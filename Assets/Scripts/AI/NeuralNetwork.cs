
using System.Collections.Generic;
using UnityEngine;


public class NeuralNetwork : MonoBehaviour, ICharacterMovement {

    private AIInputDataCreator _inputDataCreator;
    private INeuralNetworkBrain _brain;
    private int _noMoveCount;
    private int _onlyRotateCount;
    private ICharacter _controledCharacter;

    public int MoveDirection {
        get;
        set;
    }

    public int RotationDirection {
        get;
        set;
    }

    public ICharacter ControledCharacter {
        get {
            return _controledCharacter;
        }

        set {
            _controledCharacter = value;
        }
    }

    public INeuralNetworkBrain Brain {
        get {
            return _brain;
        }

        set {
            _brain = value;
            _brain.Init();
        }
    }

    public AIInputDataCreator InputDataCreator {
        get {
            return _inputDataCreator;
        }

        set {
            _inputDataCreator = value;
        }
    }

    private void OnEnable() {
        _noMoveCount = 0;
        _onlyRotateCount = 0;
    }


    private void MapOutputToPlayerInput(List<float> output) {

        MoveDirection = 0;
        RotationDirection = 0;

        if (output[0] >= 1) {
            MoveDirection = MoveDirection + 1;
        }
        if (output[1] >= 1) {
            MoveDirection = MoveDirection - 1;
        }
        if (output[2] >= 1) {
            RotationDirection = RotationDirection + 1;
        }
        if (output[3] >= 1) {
            RotationDirection = RotationDirection - 1;
        }



        if (RotationDirection == 0 && MoveDirection == 0) {
            _noMoveCount = _noMoveCount + 1;
            if (_noMoveCount >= SettingsObject.Instance.AIMaxNoMoveFrames) {
                gameObject.SetActive(false);
            }
        }
        else if (RotationDirection != 0 && MoveDirection == 0) {
            _noMoveCount = 0;
            _onlyRotateCount = _onlyRotateCount + 1;
            if (_onlyRotateCount >= SettingsObject.Instance.AIMaxOnlyRotationFrames) {
                gameObject.SetActive(false);
            }
        }
        else {
            _noMoveCount = 0;
            _onlyRotateCount = 0;
        }

    }

    private void FillInInputData(float[] input) {
        float[] inputList = ScaleInput(input);
        _brain.SetInputLayer(inputList);
    }

    private float[] ScaleInput(float[] input) {
        for (int i = 0; i < input.Length; i++) {
            input[i] = input[i] / SettingsObject.Instance.AiInputScale;
        }
        return input;
    }
    private void Update() {

        if (_brain == null) {
            return;
        }
        float[] input = _inputDataCreator.AiInputData;

        FillInInputData(input);

        List<float> output = _brain.CalculateOutputData();

        MapOutputToPlayerInput(output);

    }
    private void OnDisable() {
        SaveAgenScore();
        this.enabled = false;
    }
    public void SaveAgenScore() {

        if (SettingsObject.Instance.AIDeactivateSaveAIScore == false) {
            CharacterScore character = gameObject.GetComponent<CharacterScore>();
            _brain.SetScore(character.CurrentScore);
            //AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();
        }

        _controledCharacter.ReleaseControl(this);
        _controledCharacter = null;
    }
}

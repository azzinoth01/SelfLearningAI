//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:26:40
//  GitUser: azzinoth01
//===================================================
using UnityEngine;

public class AIPlayerControler : MonoBehaviour {
    [SerializeField] private Transform _startPosition;
    [SerializeField] private NeuralNetworkBrainObject _brain;
    [SerializeField] bool _takeControl;
    [ShowOnly, SerializeField] bool _hasControl;
    private NeuralNetwork _neuralNetwork;

    private void Update() {
        if (_takeControl == true && _hasControl == false) {
            if (CharacterContainer.Instance.ControlAbleCharacterQueue.Count != 0) {
                ICharacter controledCharacter = CharacterContainer.Instance.ControlAbleCharacterQueue.Pop();
                GameObject obj = controledCharacter.GetGameObject();
                _neuralNetwork = obj.GetComponent<NeuralNetwork>();
                if (controledCharacter.TakeControl(_neuralNetwork) == false) {
                    controledCharacter = null;
                    return;
                }
                _neuralNetwork.InputDataCreator = obj.GetComponent<AIInputDataCreator>();
                _neuralNetwork.InputDataCreator.enabled = true;
                _neuralNetwork.Brain = _brain.Brain;
                _neuralNetwork.enabled = true;
                obj.transform.position = _startPosition.position;
                obj.transform.rotation = Quaternion.identity;
                _neuralNetwork.ControledCharacter = controledCharacter;


                obj.SetActive(true);

                _hasControl = true;
            }
        }
        else if (_takeControl == false && _hasControl == true) {
            if (_neuralNetwork.ControledCharacter.ReleaseControl(_neuralNetwork)) {
                _hasControl = false;
                _neuralNetwork = null;
            }
        }
    }
}

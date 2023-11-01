using System.Diagnostics;
using UnityEditor;
using UnityEngine;

using Debug = UnityEngine.Debug;

public class NeuralNetwork : MonoBehaviour {
    public Player playerScore;
    public PlayerMovement player;
    public AIInputDataCreator inputDataCreator;
    public NeuralNetworkBrainObject brainobject;
    public NeuralNetworkBrain brain;


    public float InputScale;
    public bool deactivateSavePowerlevel;
    public int noMoveCount;
    public int onlyRotateCount;

    private void OnEnable() {
        noMoveCount = 0;
        onlyRotateCount = 0;
        if (brainobject != null) {
            brain = brainobject.brain;
            brain.CreateDictionaries();
        }
    }

    // Update is called once per frame
    void Update() {

        FillInInputData();
        GenerateOutputData();

        MapOutputToPlayerInput();
    }

    private void OnDisable() {

        if (deactivateSavePowerlevel == false) {
            brain.powerValue = playerScore.currentScore;
            AssetDatabase.SaveAssets();

        }


    }

    public void MapOutputToPlayerInput() {
        player.rotate = 0;
        player.move = 0;

        if (brain.layeredNodeList[brain.maxLayer][0].value != 0) {
            player.move = player.move + 1;
        }
        if (brain.layeredNodeList[brain.maxLayer][1].value != 0) {
            player.move = player.move - 1;
        }

        if (brain.layeredNodeList[brain.maxLayer][2].value != 0) {
            player.rotate = player.rotate + 1;
        }
        if (brain.layeredNodeList[brain.maxLayer][3].value != 0) {
            player.rotate = player.rotate - 1;
        }

        if (player.rotate == 0 && player.move == 0) {
            noMoveCount = noMoveCount + 1;
            if (noMoveCount >= 10) {
                gameObject.SetActive(false);
            }
        }
        else {
            noMoveCount = 0;
        }
        if (player.move == 0) {
            onlyRotateCount = onlyRotateCount + 1;
            if (onlyRotateCount >= 500) {
                gameObject.SetActive(false);
            }
        }
        else {
            onlyRotateCount = 0;
        }
    }


    [ContextMenu("Test")]
    public void Test() {
        Stopwatch watch = new Stopwatch();

        watch.Start();


        FillInInputData();
        GenerateOutputData();

        Debug.Log(watch.ElapsedMilliseconds);
    }

    public void FillInInputData() {
        float[] inputList = ScaleInput();
        for (int i = 0; i < 8;) {
            brain.layeredNodeList[0][i].value = inputList[i];
            i = i + 1;
        }
        //foreach (float value in inputList) {

        //}

        //brain.layeredNodeList[0][0].value = ScaleInput(inputDataCreator.frontDistance);
        //brain.layeredNodeList[0][1].value = ScaleInput(inputDataCreator.leftFrontDistance);
        //brain.layeredNodeList[0][2].value = ScaleInput(inputDataCreator.rightFrontDistance);
        //brain.layeredNodeList[0][3].value = ScaleInput(inputDataCreator.leftDistance);
        //brain.layeredNodeList[0][4].value = ScaleInput(inputDataCreator.rightDistance);
        //brain.layeredNodeList[0][5].value = ScaleInput(inputDataCreator.leftBackDistance);
        //brain.layeredNodeList[0][6].value = ScaleInput(inputDataCreator.rightBackDistance);
        //brain.layeredNodeList[0][7].value = ScaleInput(inputDataCreator.backDistance);

    }

    public float[] ScaleInput() {

        float[] inputList = new float[8];


        for (int i = 0; i < 8;) {
            inputList[i] = inputDataCreator.aiInputData[i] / InputScale;
            i = i + 1;
        }

        return inputList;
    }


    public void GenerateOutputData() {
        int layer = 1;
        //foreach (Node node in brain.layeredNodeList[0]) {
        //    Debug.Log("input Value:" + node.value);
        //}
        while (brain.maxLayer >= layer) {
            foreach (Node node in brain.layeredNodeList[layer]) {
                node.CalculateValue();

            }

            layer = layer + 1;
        }

        //foreach (Node node in brain.layeredNodeList[brain.maxLayer]) {
        //    Debug.Log("value: " + node.value);
        //}
    }

}

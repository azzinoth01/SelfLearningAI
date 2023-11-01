using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BrainCreator : MonoBehaviour {

    public int brainsToCreate;
    public List<NeuralNetworkBrain> brainList;
    public int _currentBrainId;

    public NeuralNetworkBrainObject currentParentBrain;



    public int increaseMutationAmount;




    public void CreateBrainFromParentBrain(NeuralNetworkBrain parentBrain, int rangeIncrease = 0) {


        NeuralNetworkBrain brain = new NeuralNetworkBrain(parentBrain, 1 + rangeIncrease);
        brainList.Add(brain);




    }

    public void CreateNewBrainGen() {
        bool brainChange = false;
        //NeuralNetworkBrain parentBrain = brainList[0];
        NeuralNetworkBrain bestBrainThisGen;
        if (brainList.Count == 0) {
            NeuralNetworkBrain baseBrain = new NeuralNetworkBrain(currentParentBrain.brain, 1, false);
            brainList.Add(baseBrain);
        }

        bestBrainThisGen = brainList[0];

        foreach (NeuralNetworkBrain brain in brainList) {
            //if (brain.powerValue > currentParentBrain.brain.powerValue) {
            //    currentParentBrain = brain;
            //    brainChange = true;

            //    increaseMutationAmount = 0;
            //}
            if (bestBrainThisGen.powerValue < brain.powerValue) {
                bestBrainThisGen = brain;
            }
        }

        if (bestBrainThisGen.powerValue > currentParentBrain.brain.powerValue) {
            currentParentBrain = null;

            currentParentBrain = new NeuralNetworkBrainObject();
            currentParentBrain.brain = bestBrainThisGen;
            brainChange = true;
            increaseMutationAmount = 0;
        }

        if (brainChange == false) {

            increaseMutationAmount = increaseMutationAmount + 1;

        }

        brainList.Clear();

        _currentBrainId = _currentBrainId + 1;
        if (brainChange == true) {
            AssetDatabase.CreateAsset(currentParentBrain, "Assets/ScriptableObjects/brainGen" + _currentBrainId + ".asset");
            AssetDatabase.SaveAssets();
        }

        NeuralNetworkBrain newBrain = new NeuralNetworkBrain(currentParentBrain.brain, 1, false);

        brainList.Add(newBrain);



        while (brainList.Count < brainsToCreate / 2) {

            CreateBrainFromParentBrain(newBrain, increaseMutationAmount);

        }
        newBrain = new NeuralNetworkBrain(bestBrainThisGen, 1, false);
        brainList.Add(newBrain);
        while (brainList.Count < brainsToCreate) {

            CreateBrainFromParentBrain(newBrain, 0);

        }
    }
}

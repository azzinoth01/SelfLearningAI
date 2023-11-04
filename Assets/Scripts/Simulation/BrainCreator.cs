using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BrainCreator : MonoBehaviour {
    [SerializeField] private string _brainStoragePath;
    private List<INeuralNetworkBrain> _brainList;
    [SerializeField] private int _currentBrainId;
    [SerializeField] private NeuralNetworkBrainObject _currentParentBrain;
    [SerializeField] private int _increaseMutationAmount;

    public List<INeuralNetworkBrain> BrainList {
        get {
            return _brainList;
        }
    }

    private void Awake() {
        _brainList = new List<INeuralNetworkBrain>();
    }

    private void CreateBrainFromParentBrain(NeuralNetworkBrain parentBrain, int MutationIncrease = 0, bool mutate = true) {

        NeuralNetworkBrain brain = new NeuralNetworkBrain(parentBrain, 1 + MutationIncrease, mutate);
        _brainList.Add(brain);
    }

    public void CreateNewBrainGen() {
        if (_currentParentBrain == null) {
            return;
        }
        //List<NeuralNetworkBrain> bestBrainsThisBatch = GetBestBrainsOfThisBatch();

        CheckBrainGeneration();

        _brainList = new List<INeuralNetworkBrain>();
        CreateBrainFromParentBrain(_currentParentBrain.Brain, 0, false);
        while (_brainList.Count < SettingsObject.Instance.AIBrainsToCreate / 2) {

            CreateBrainFromParentBrain(_currentParentBrain.Brain, _increaseMutationAmount);
        }
        while (_brainList.Count < SettingsObject.Instance.AIBrainsToCreate) {

            CreateBrainFromParentBrain(_currentParentBrain.Brain, 0);
        }


    }

    private void CheckBrainGeneration() {
        NeuralNetworkBrain bestBrainThisGen;
        if (_brainList.Count == 0) {
            CreateBrainFromParentBrain(_currentParentBrain.Brain, 0, false);
        }
        bestBrainThisGen = FindBestBrain(_brainList);
        _currentBrainId = _currentBrainId + 1;
        if (bestBrainThisGen.PowerValue > _currentParentBrain.Brain.PowerValue) {
            ReplaceParentBrain(bestBrainThisGen);
            _increaseMutationAmount = 0;
        }
        else {
            _increaseMutationAmount = _increaseMutationAmount + 1;
        }


    }

    private NeuralNetworkBrain FindBestBrain(List<INeuralNetworkBrain> brainList) {
        if (brainList == null || brainList.Count == 0) {
            return null;
        }
        NeuralNetworkBrain bestBrainThisGen = (NeuralNetworkBrain)brainList[0];
        foreach (NeuralNetworkBrain brain in brainList) {

            if (bestBrainThisGen.PowerValue < brain.PowerValue) {
                bestBrainThisGen = brain;
            }
        }
        return bestBrainThisGen;
    }
    private List<NeuralNetworkBrain> GetBestBrainsOfThisBatch() {
        if (_brainList == null || _brainList.Count == 0) {
            return null;
        }
        List<NeuralNetworkBrain> sortedList = new List<NeuralNetworkBrain>();
        foreach (NeuralNetworkBrain brain in _brainList) {
            sortedList.Add(brain);
        }
        sortedList = sortedList.OrderByDescending(x => x.PowerValue).ToList();

        sortedList.RemoveRange((sortedList.Count / 2) - 1, sortedList.Count / 2);

        return sortedList;

    }


    private void ReplaceParentBrain(NeuralNetworkBrain brain) {
        // set null to lose referenz so it does not overwrite the old scriptable object
        _currentParentBrain = null;
        _currentParentBrain = new NeuralNetworkBrainObject();
        _currentParentBrain.Brain = brain;

        // "Assets/ScriptableObjects/brainGen"
        AssetDatabase.CreateAsset(_currentParentBrain, _brainStoragePath + _currentBrainId + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:27:54
//  GitUser: azzinoth01
//===================================================
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BrainCreator : MonoBehaviour {
    [SerializeField] private string _brainStoragePath;
    private List<INeuralNetworkBrain> _brainList;
    [SerializeField] private int _currentBrainId;
    [SerializeField] private NeuralNetworkBrainObject _currentParentBrain;
    private int _bestGeneration;
    [SerializeField] private int _increaseMutationAmount;
    [SerializeField] private int _currentTestCycle;

    public List<INeuralNetworkBrain> BrainList {
        get {
            return _brainList;
        }
    }

    public int CurrentTestCycle {
        get {
            return _currentTestCycle;
        }
    }

    public int BestGeneration {
        get {
            return _bestGeneration;
        }

    }

    public int CurrentBrainId {
        get {
            return _currentBrainId;
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
        if (_brainList == null || _brainList.Count == 0) {
            _brainList = new List<INeuralNetworkBrain>();
            for (int i = 0; i < SettingsObject.Instance.AIBrainsToCreate; i++) {
                CreateBrainFromParentBrain(_currentParentBrain.Brain, 0, false);
            }
            _bestGeneration = _currentBrainId;
            return;
        }
        if (_currentTestCycle < SettingsObject.Instance.AITestCycle) {

            _currentTestCycle = _currentTestCycle + 1;
        }
        else {
            _currentTestCycle = 1;
            CheckBrainGeneration();
            List<NeuralNetworkBrain> bestBrainsThisBatch = GetBestBrainsOfThisBatch();

            _brainList = new List<INeuralNetworkBrain>();

            foreach (INeuralNetworkBrain brain in bestBrainsThisBatch) {
                CreateBrainFromParentBrain((NeuralNetworkBrain)brain, 0, false);
            }
            foreach (INeuralNetworkBrain brain in bestBrainsThisBatch) {
                for (int i = 0; i < 9; i++) {
                    CreateBrainFromParentBrain((NeuralNetworkBrain)brain, _increaseMutationAmount);
                }
            }
        }

    }

    private void CheckBrainGeneration() {
        NeuralNetworkBrain bestBrainThisGen;
        if (_brainList.Count == 0) {
            CreateBrainFromParentBrain(_currentParentBrain.Brain, 0, false);
        }
        bestBrainThisGen = FindBestBrain(_brainList);
        _currentBrainId = _currentBrainId + 1;
        if (bestBrainThisGen.AverageValue > _currentParentBrain.Brain.AverageValue) {
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

            if (bestBrainThisGen.AverageValue < brain.AverageValue) {
                bestBrainThisGen = brain;
            }
        }
        return bestBrainThisGen;
    }
    private List<NeuralNetworkBrain> GetBestBrainsOfThisBatch() {
        if (_brainList == null || _brainList.Count == 0) {
            return new List<NeuralNetworkBrain>();
        }
        List<NeuralNetworkBrain> sortedList = new List<NeuralNetworkBrain>();
        foreach (NeuralNetworkBrain brain in _brainList) {
            sortedList.Add(brain);
        }
        sortedList = sortedList.OrderByDescending(x => x.AverageValue).ToList();
        List<NeuralNetworkBrain> test = sortedList.Take(SettingsObject.Instance.AIBrainsToCreate / 10).ToList();
        return test;
    }


    private void ReplaceParentBrain(NeuralNetworkBrain brain) {
        // set null to lose referenz so it does not overwrite the old scriptable object
        _currentParentBrain = null;
        _currentParentBrain = new NeuralNetworkBrainObject();
        _currentParentBrain.Brain = brain;
        _bestGeneration = _currentBrainId;
        // "Assets/ScriptableObjects/brainGen"
        AssetDatabase.CreateAsset(_currentParentBrain, _brainStoragePath + CurrentBrainId + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class NeuralNetworkManager : MonoBehaviour {

    private static NeuralNetworkManager _instance;

    private List<INeuralNetworkAgent> _agenList;
    private Dictionary<int, INeuralNetworkAgent> _agentDictonary;
    private Stack<int> _freeIDs;
    private int _iDCounter;

    public static NeuralNetworkManager Instance {
        get {
            if (_instance == null) {
                GameObject obj = new GameObject();
                _instance = obj.AddComponent<NeuralNetworkManager>();

                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    //public List<INeuralNetworkAgent> AgenList {
    //    get {
    //        return _agenList;
    //    }
    //}
    public void AddAgent(INeuralNetworkAgent agent) {
        if (_freeIDs.Count != 0) {
            _agentDictonary.Add(_freeIDs.Pop(), agent);
        }
        else {
            _agentDictonary.Add(_iDCounter, agent);
            _iDCounter = _iDCounter + 1;
        }
    }

    public int AgentCount() {
        return _agentDictonary.Count;
    }

    // Update is called once per frame
    void Update() {

        Stack<int> removeStack = new Stack<int>();
        foreach (var pair in _agentDictonary) {
            if (pair.Value.Update() == false) {
                removeStack.Push(pair.Key);

            }
        }
        while (removeStack.Count != 0) {
            int value = removeStack.Pop();
            _agentDictonary.Remove(value);
            _freeIDs.Push(value);
        }

    }


    private NeuralNetworkManager() {
        _agenList = new List<INeuralNetworkAgent>();
        _agentDictonary = new Dictionary<int, INeuralNetworkAgent>();
        _freeIDs = new Stack<int>();
        _iDCounter = 0;
        for (int i = 0; i < 1000; i++) {
            _freeIDs.Push(_iDCounter);
            _iDCounter = _iDCounter + 1;
        }
    }
}

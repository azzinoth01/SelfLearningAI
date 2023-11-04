using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/NeuralNetworkBrain", order = 1)]
public class NeuralNetworkBrainObject : ScriptableObject {

    [SerializeField] private NeuralNetworkBrain _brain;

    public NeuralNetworkBrain Brain {
        get {
            return _brain;
        }
        set {
            _brain = value;
        }
    }

    [ContextMenu("Create Base Brains")]
    public void CreateBrain() {

        List<int> hiddenLayerNodes = new List<int>() { 10, 10, 10, 10, 4 };
        _brain = new NeuralNetworkBrain(hiddenLayerNodes);

        AssetDatabase.SaveAssets();
    }
}



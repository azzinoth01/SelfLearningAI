﻿//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:28:53
//  GitUser: azzinoth01
//===================================================
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseBrain", menuName = "ScriptableObjects/NeuralNetworkBrain", order = 1)]
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



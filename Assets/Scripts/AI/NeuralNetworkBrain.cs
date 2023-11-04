using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class NeuralNetworkBrain : INeuralNetworkBrain {
    [SerializeField] private List<Node> _nodeList;
    [SerializeField] private int _maxLayer;

    private Dictionary<int, List<Node>> _layeredNodeList;
    private Dictionary<int, Node> _nodeDictonary;

    [SerializeField] private float _powerValue;

    public float PowerValue {
        get {
            return _powerValue;
        }

        set {
            _powerValue = value;
        }
    }

    public int MaxLayer {
        get {
            return _maxLayer;
        }
    }

    public Dictionary<int, List<Node>> LayeredNodeList {
        get {
            return _layeredNodeList;
        }
    }

    public void CreateDictionaries() {

        _layeredNodeList = new Dictionary<int, List<Node>>();
        _nodeDictonary = new Dictionary<int, Node>();
        foreach (Node node in _nodeList) {
            _nodeDictonary.Add(node.Id, node);
            if (_layeredNodeList.TryGetValue(node.Layer, out List<Node> layerNodeList)) {
                layerNodeList.Add(node);
            }
            else {
                layerNodeList = new List<Node>();
                layerNodeList.Add(node);
                _layeredNodeList.Add(node.Layer, layerNodeList);
            }

        }
        SetNodeConnections();
    }

    private void SetNodeConnections() {
        foreach (Node node in _nodeList) {
            foreach (Connection con in node.ConnectedNodes) {
                con.Node = _nodeDictonary[con.NodeId];
            }
        }
    }

    public NeuralNetworkBrain(List<int> layerNodes) {

        _nodeList = new List<Node>();
        _layeredNodeList = new Dictionary<int, List<Node>>();
        _nodeDictonary = new Dictionary<int, Node>();
        int nodeId = 0;
        int layer = 0;
        foreach (int nodesInLayer in layerNodes) {
            nodeId = CreateNodeLayer(nodeId, nodesInLayer, layer);
            _maxLayer = layer;
            layer = layer + 1;
        }
    }

    public NeuralNetworkBrain(NeuralNetworkBrain parentBrain, int maxMutateCount = 1, bool mutate = true) {
        _nodeList = new List<Node>();
        _layeredNodeList = new Dictionary<int, List<Node>>();
        _nodeDictonary = new Dictionary<int, Node>();
        _nodeList = parentBrain._nodeList.ConvertAll(node => new Node(node)).ToList();
        _maxLayer = parentBrain._maxLayer;
        _powerValue = 0;

        CreateDictionaries();
        if (mutate == true) {
            int mutateAmout = Random.Range(1, maxMutateCount);
            for (int i = 0; i < mutateAmout; i++) {
                Mutate();
            }
        }

    }

    private int CreateNodeLayer(int currentNodeId, int nodes, int layer) {
        for (int i = 0; i < nodes; i++) {
            Node node = new Node(currentNodeId, Random.Range(-1f, 1f), layer);
            _nodeDictonary.Add(currentNodeId, node);
            currentNodeId = currentNodeId + 1;

            _nodeList.Add(node);

            if (_layeredNodeList.TryGetValue(layer, out List<Node> layerNodeList)) {
                layerNodeList.Add(node);
            }
            else {
                layerNodeList = new List<Node>();
                layerNodeList.Add(node);
                _layeredNodeList.Add(layer, layerNodeList);
            }
            ConnectPreviousLayerNodes(node, layer);
        }
        return currentNodeId;
    }
    private void ConnectPreviousLayerNodes(Node node, int layer) {
        if (_layeredNodeList.TryGetValue(layer - 1, out List<Node> previousNodesList)) {

            foreach (Node previewsNode in previousNodesList) {
                Connection con = new Connection();

                con.NodeId = previewsNode.Id;
                con.Weight = Random.Range(-1f, 1f);
                node.ConnectedNodes.Add(con);
                con.Node = _nodeDictonary[con.NodeId];
            }

        }
    }



    public void SetInputLayer(float[] input) {
        if (_nodeList == null || _nodeList.Count == 0) {
            return;
        }


        for (int i = 0; i < _layeredNodeList[0].Count; i++) {
            if (i < input.Length) {
                _layeredNodeList[0][i].Value = input[i];
            }
            else {
                _layeredNodeList[0][i].Value = 0;
            }
        }
    }

    public void SetInputLayer(List<float> input) {
        if (_nodeList == null || _nodeList.Count == 0) {
            return;
        }

        for (int i = 0; i < _layeredNodeList[0].Count; i++) {
            if (i < input.Count) {
                _layeredNodeList[0][i].Value = input[i];
            }
            else {
                _layeredNodeList[0][i].Value = 0;
            }
        }
    }

    public List<float> CalculateOutputData() {
        List<float> outputLayerValues = new List<float>();
        for (int layer = 1; layer < _maxLayer; layer++) {
            foreach (Node node in _layeredNodeList[layer]) {
                node.CalculateValue();
            }
        }
        foreach (Node node in _layeredNodeList[_maxLayer]) {
            node.CalculateValue();
            outputLayerValues.Add(node.Value);
        }
        return outputLayerValues;
    }

    public void Mutate() {

        int changeNode = Random.Range(_layeredNodeList[0].Count, _nodeList.Count);

        int changeWeight = Random.Range(0, 2);

        if (changeWeight == 0) {
            _nodeList[changeNode].Bias = _nodeList[changeNode].Bias + Random.Range(-1f, 1);
        }
        else {
            int connections = _nodeList[changeNode].ConnectedNodes.Count;
            if (connections != 0) {
                int changeConnection = Random.Range(0, connections);

                _nodeList[changeNode].ConnectedNodes[changeConnection].Weight = _nodeList[changeNode].ConnectedNodes[changeConnection].Weight + Random.Range(-1f, 1f);
            }

        }
    }

    public void Init() {
        CreateDictionaries();
    }

    public void SetScore(float score) {
        _powerValue = score;
    }

}

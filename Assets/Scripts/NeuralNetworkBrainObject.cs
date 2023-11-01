using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/NeuralNetworkBrain", order = 1)]
public class NeuralNetworkBrainObject : ScriptableObject {

    public NeuralNetworkBrain brain;

    //public List<Node> nodeList;
    //public int maxLayer;

    //public Dictionary<int, List<Node>> layeredNodeList;
    //public Dictionary<int, Node> nodeDictonary;

    //public float powerValue;

    //public void CreateDictionaries() {

    //    layeredNodeList = new Dictionary<int, List<Node>>();
    //    nodeDictonary = new Dictionary<int, Node>();
    //    foreach (Node node in nodeList) {
    //        nodeDictonary.Add(node.id, node);
    //        if (layeredNodeList.TryGetValue(node.layer, out List<Node> layerNodeList)) {
    //            layerNodeList.Add(node);
    //        }
    //        else {
    //            layerNodeList = new List<Node>();
    //            layerNodeList.Add(node);
    //            layeredNodeList.Add(node.layer, layerNodeList);
    //        }

    //    }
    //    SetNodeConnections();
    //}

    //public void SetNodeConnections() {
    //    foreach (Node node in nodeList) {
    //        foreach (Connection con in node.connectedNodes) {
    //            con.Node = nodeDictonary[con.nodeId];
    //        }
    //    }
    //}

    //public NeuralNetworkBrainObject() {

    //}
    //public NeuralNetworkBrainObject(NeuralNetworkBrainObject parentBrain, int maxMutateCount = 1, bool mutate = true) {

    //    nodeList = parentBrain.nodeList.ConvertAll(node => new Node(node)).ToList();
    //    maxLayer = parentBrain.maxLayer;
    //    powerValue = 0;


    //    if (mutate == true) {
    //        int mutateAmout = Random.Range(1, maxMutateCount);
    //        for (int i = 0; i < mutateAmout;) {
    //            Mutate();

    //            i = i + 1;
    //        }

    //    }


    //    CreateDictionaries();
    //}

    //public void Mutate() {
    //    int changeNode = Random.Range(0, nodeList.Count);

    //    int changeWeight = Random.Range(0, 2);

    //    if (changeWeight == 0) {
    //        nodeList[changeNode].bias = nodeList[changeNode].bias + Random.Range(-1f, 1);
    //    }
    //    else {
    //        int connections = nodeList[changeNode].connectedNodes.Count;
    //        if (connections != 0) {
    //            int changeConnection = Random.Range(0, connections);

    //            nodeList[changeNode].connectedNodes[changeConnection].weight = nodeList[changeNode].connectedNodes[changeConnection].weight + Random.Range(-1f, 1f);
    //        }

    //    }
    //}
    //[ContextMenu("Create Brain")]
    //public void CreateBrain() {

    //    brain = new NeuralNetworkBrain();

    //    brain.nodeList = nodeList.ConvertAll(node => new Node(node)).ToList();
    //    brain.maxLayer = maxLayer;
    //    brain.powerValue = powerValue;
    //}


    [ContextMenu("Create Base Brains")]
    public void CreateBrain() {
        brain = new NeuralNetworkBrain();


        brain.nodeList = new List<Node>();
        int currentLayer = 0;
        int nodeId = 0;

        Dictionary<int, List<Node>> layeredNodeList = new Dictionary<int, List<Node>>();

        for (int i = 0; i < 10;) {
            Node node = new Node();
            node.id = nodeId;
            nodeId = nodeId + 1;
            node.bias = Random.Range(-1f, 1f);
            node.connectedNodes = new List<Connection>();
            node.layer = currentLayer;
            brain.nodeList.Add(node);

            if (layeredNodeList.TryGetValue(currentLayer, out List<Node> layerNodeList)) {
                layerNodeList.Add(node);
            }
            else {

                layerNodeList = new List<Node>();
                layerNodeList.Add(node);
                layeredNodeList.Add(currentLayer, layerNodeList);
            }

            i = i + 1;
        }

        currentLayer = currentLayer + 1;

        for (int i = 0; i < 10;) {
            Node node = new Node();
            node.id = nodeId;
            nodeId = nodeId + 1;
            node.bias = Random.Range(-1f, 1f);
            node.connectedNodes = new List<Connection>();
            node.layer = currentLayer;
            brain.nodeList.Add(node);
            if (layeredNodeList.TryGetValue(currentLayer, out List<Node> layerNodeList)) {
                layerNodeList.Add(node);
            }
            else {

                layerNodeList = new List<Node>();
                layerNodeList.Add(node);
                layeredNodeList.Add(currentLayer, layerNodeList);
            }

            foreach (Node previewsNode in layeredNodeList[currentLayer - 1]) {
                Connection con = new Connection();

                con.nodeId = previewsNode.id;
                con.weight = Random.Range(-1f, 1f);
                node.connectedNodes.Add(con);
            }
            i = i + 1;
        }
        currentLayer = currentLayer + 1;

        for (int i = 0; i < 10;) {
            Node node = new Node();
            node.id = nodeId;
            nodeId = nodeId + 1;
            node.bias = Random.Range(-1f, 1f);
            node.connectedNodes = new List<Connection>();
            node.layer = currentLayer;
            brain.nodeList.Add(node);
            if (layeredNodeList.TryGetValue(currentLayer, out List<Node> layerNodeList)) {
                layerNodeList.Add(node);
            }
            else {

                layerNodeList = new List<Node>();
                layerNodeList.Add(node);
                layeredNodeList.Add(currentLayer, layerNodeList);
            }

            foreach (Node previewsNode in layeredNodeList[currentLayer - 1]) {
                Connection con = new Connection();

                con.nodeId = previewsNode.id;
                con.weight = Random.Range(-1f, 1f);
                node.connectedNodes.Add(con);
            }
            i = i + 1;
        }
        currentLayer = currentLayer + 1;

        for (int i = 0; i < 10;) {
            Node node = new Node();
            node.id = nodeId;
            nodeId = nodeId + 1;
            node.bias = Random.Range(-1f, 1f);
            node.connectedNodes = new List<Connection>();
            node.layer = currentLayer;
            brain.nodeList.Add(node);
            if (layeredNodeList.TryGetValue(currentLayer, out List<Node> layerNodeList)) {
                layerNodeList.Add(node);
            }
            else {

                layerNodeList = new List<Node>();
                layerNodeList.Add(node);
                layeredNodeList.Add(currentLayer, layerNodeList);
            }

            foreach (Node previewsNode in layeredNodeList[currentLayer - 1]) {
                Connection con = new Connection();

                con.nodeId = previewsNode.id;
                con.weight = Random.Range(-1f, 1f);
                node.connectedNodes.Add(con);
            }
            i = i + 1;
        }

        currentLayer = currentLayer + 1;
        for (int i = 0; i < 4;) {
            Node node = new Node();
            node.id = nodeId;
            nodeId = nodeId + 1;
            node.bias = Random.Range(-1f, 1f);
            node.connectedNodes = new List<Connection>();
            node.layer = currentLayer;
            brain.nodeList.Add(node);
            if (layeredNodeList.TryGetValue(currentLayer, out List<Node> layerNodeList)) {
                layerNodeList.Add(node);
            }
            else {

                layerNodeList = new List<Node>();
                layerNodeList.Add(node);
                layeredNodeList.Add(currentLayer, layerNodeList);
            }

            foreach (Node previewsNode in layeredNodeList[currentLayer - 1]) {
                Connection con = new Connection();

                con.nodeId = previewsNode.id;
                con.weight = Random.Range(-1f, 1f);
                node.connectedNodes.Add(con);
            }
            i = i + 1;
        }

        brain.maxLayer = currentLayer;
        brain.powerValue = 0f;

        brain.CreateDictionaries();
        AssetDatabase.SaveAssets();
    }
}



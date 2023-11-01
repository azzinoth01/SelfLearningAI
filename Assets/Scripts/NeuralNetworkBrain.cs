using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

[Serializable]
public class NeuralNetworkBrain {
    public List<Node> nodeList;
    public int maxLayer;

    public Dictionary<int, List<Node>> layeredNodeList;
    public Dictionary<int, Node> nodeDictonary;

    public float powerValue;

    public void CreateDictionaries() {

        layeredNodeList = new Dictionary<int, List<Node>>();
        nodeDictonary = new Dictionary<int, Node>();
        foreach (Node node in nodeList) {
            nodeDictonary.Add(node.id, node);
            if (layeredNodeList.TryGetValue(node.layer, out List<Node> layerNodeList)) {
                layerNodeList.Add(node);
            }
            else {
                layerNodeList = new List<Node>();
                layerNodeList.Add(node);
                layeredNodeList.Add(node.layer, layerNodeList);
            }

        }
        SetNodeConnections();
    }

    public void SetNodeConnections() {
        foreach (Node node in nodeList) {
            foreach (Connection con in node.connectedNodes) {
                con.Node = nodeDictonary[con.nodeId];
            }
        }
    }

    public NeuralNetworkBrain() {

    }
    public NeuralNetworkBrain(NeuralNetworkBrain parentBrain, int maxMutateCount = 1, bool mutate = true) {
        nodeList = new List<Node>();
        layeredNodeList = new Dictionary<int, List<Node>>();
        nodeDictonary = new Dictionary<int, Node>();
        nodeList = parentBrain.nodeList.ConvertAll(node => new Node(node)).ToList();
        maxLayer = parentBrain.maxLayer;
        powerValue = 0;


        if (mutate == true) {
            int mutateAmout = Random.Range(1, maxMutateCount);
            for (int i = 0; i < mutateAmout;) {
                Mutate();

                i = i + 1;
            }

        }


        CreateDictionaries();
    }

    public void Mutate() {
        int changeNode = Random.Range(0, nodeList.Count);

        int changeWeight = Random.Range(0, 2);

        if (changeWeight == 0) {
            nodeList[changeNode].bias = nodeList[changeNode].bias + Random.Range(-1f, 1);
        }
        else {
            int connections = nodeList[changeNode].connectedNodes.Count;
            if (connections != 0) {
                int changeConnection = Random.Range(0, connections);

                nodeList[changeNode].connectedNodes[changeConnection].weight = nodeList[changeNode].connectedNodes[changeConnection].weight + Random.Range(-1f, 1f);
            }

        }
    }
}

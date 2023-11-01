using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

[Serializable]
public class NeatBrain {

    public List<NeatNode> inputNodes;
    public List<NeatNode> outputNodes;

    public List<NeatNode> nodeList;
    public List<NeatConnection> connectionList;

    public int maxNodeId;
    public float powerlevel;

    public Dictionary<int, NeatConnection> connectionDictonary;
    public Dictionary<int, NeatNode> nodeDictonary;
    public Dictionary<int, List<NeatNode>> layerdNodeListDictonary;


    private void CreateInputListForNodes() {
        // create calculating order

        foreach (NeatNode node in inputNodes) {
            node.InputList.Clear();
            node.OutputList.Clear();
            node.Layer = -1;
        }

        foreach (NeatNode node in nodeList) {
            node.InputList.Clear();
            node.OutputList.Clear();
            node.Layer = 0;
        }
        foreach (NeatNode node in outputNodes) {
            node.InputList.Clear();
            node.OutputList.Clear();
            node.Layer = 1;
        }

        foreach (NeatConnection con in connectionList) {
            nodeDictonary[con.OutputNodeId].InputList.Add(con.Id);
            nodeDictonary[con.InputNodeId].OutputList.Add(nodeDictonary[con.OutputNodeId]);
        }
        foreach (NeatNode node in inputNodes) {
            node.CalculateLayer(-1);
        }

    }

    private void InitDictonaries() {
        connectionDictonary = new Dictionary<int, NeatConnection>();
        foreach (NeatConnection con in connectionList) {
            connectionDictonary.Add(con.Id, con);
        }

        nodeDictonary = new Dictionary<int, NeatNode>();
        layerdNodeListDictonary = new Dictionary<int, List<NeatNode>>();
        foreach (NeatNode node in inputNodes) {
            nodeDictonary.Add(node.Id, node);
        }
        foreach (NeatNode node in nodeList) {
            nodeDictonary.Add(node.Id, node);

            if (layerdNodeListDictonary.TryGetValue(node.Layer, out List<NeatNode> layerList)) {
                layerList.Add(node);
            }
            else {
                layerList = new List<NeatNode>();
                layerList.Add(node);
                layerdNodeListDictonary.Add(node.Layer, layerList);
            }

        }
        foreach (NeatNode node in outputNodes) {
            nodeDictonary.Add(node.Id, node);
        }


    }

    public void InitBrain() {
        InitDictonaries();
        CreateInputListForNodes();
    }

    public void CalculatedNetwork(List<float> input) {

        CalculateInput(input);
        CalculateHiddeLayer();
        CalculateOutput();
    }

    private void CalculateInput(List<float> input) {
        int count;
        if (input.Count > inputNodes.Count) {
            count = inputNodes.Count;
        }
        else {
            count = input.Count;
        }
        for (int i = 0; i < count;) {
            inputNodes[i].Value = input[i];

            i = i + 1;
        }
    }
    private void CalculateHiddeLayer() {
        bool nextLayerExits = true;
        int i = 0;
        while (nextLayerExits) {

            if (layerdNodeListDictonary.TryGetValue(i, out List<NeatNode> list)) {
                foreach (NeatNode node in list) {
                    node.CalculateNode(this);
                }
            }
            else {
                nextLayerExits = false;
            }

            i = i + 1;
        }
    }
    private void CalculateOutput() {
        foreach (NeatNode node in outputNodes) {
            node.CalculateNode(this);
        }
    }

    public NeatBrain() {

    }
    public NeatBrain(NeatBrain copy) {
        powerlevel = 0;
        maxNodeId = copy.maxNodeId;
        inputNodes = copy.inputNodes.ConvertAll(x => new NeatNode(x)).ToList();
        outputNodes = copy.outputNodes.ConvertAll(x => new NeatNode(x)).ToList();
        nodeList = copy.nodeList.ConvertAll(x => new NeatNode(x)).ToList();
        connectionList = copy.connectionList.ConvertAll(x => new NeatConnection(x)).ToList();


        InitBrain();
    }

    public void Mutate() {

        int x = Random.Range(0, 100);
        bool mutated = false;
        while (mutated == false) {
            if (x < 80) {
                // mutate weight
                x = Random.Range(0, 100);
                int y = Random.Range(0, connectionList.Count);
                if (x < 90) {
                    connectionList[y].Weight = connectionList[y].Weight + Random.Range(-1f, 1f);
                }
                else {
                    connectionList[y].Weight = Random.Range(-1f, 1f);
                }
            }
            else if (x < 95) {
                // mutate add connection
                bool nodeIsNoOutputNode = false;

                while (nodeIsNoOutputNode == false) {
                    List<int> keyList = nodeDictonary.Keys.ToList();
                    int y = Random.Range(0, keyList.Count);
                    int nodeId = keyList[y];
                    if (nodeDictonary[nodeId].Type != NeatNodeType.output) {
                        nodeIsNoOutputNode = true;
                        List<NeatNode> posibleConnectionNodes = new List<NeatNode>();
                        foreach (NeatNode node in nodeList) {
                            if (node.Layer >= nodeDictonary[nodeId].Layer && node.Id != nodeId) {
                                posibleConnectionNodes.Add(node);
                            }
                        }
                        foreach (NeatNode node in outputNodes) {
                            posibleConnectionNodes.Add(node);
                        }
                        y = Random.Range(0, posibleConnectionNodes.Count);
                        if (NeatHistoryInstance.Instance.History.connectionDictonary.TryGetValue((nodeId, posibleConnectionNodes[y].Id), out NeatConnection con)) {
                            NeatConnection addCon = new NeatConnection(con);
                            addCon.Enabled = true;
                            connectionList.Add(addCon);

                        }
                        else {
                            con = new NeatConnection(nodeId, posibleConnectionNodes[y].Id);
                            connectionList.Add(con);
                        }



                    }

                }

            }
            else {
                // mutate add node
                bool conIsEnabled = false;
                while (conIsEnabled == false) {
                    int y = Random.Range(0, connectionList.Count);
                    if (connectionList[y].Enabled) {
                        conIsEnabled = true;
                        NeatNode newNode = new NeatNode();
                        nodeList.Add(newNode);

                        NeatConnection con = new NeatConnection(connectionList[y].InputNodeId, newNode.Id);
                        connectionList.Add(con);

                        con = new NeatConnection(newNode.Id, connectionList[y].OutputNodeId, connectionList[y].Weight);
                        connectionList.Add(con);

                        connectionList[y].Enabled = false;

                    }
                }



            }
        }


    }

}

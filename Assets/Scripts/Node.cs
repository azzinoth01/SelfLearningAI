
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Node {
    public int id;
    public float value;
    public float bias;
    public List<Connection> connectedNodes;
    public int layer;


    public void CalculateValue() {

        float currentvalue = 0;

        foreach (Connection con in connectedNodes) {
            currentvalue = currentvalue + (con.Node.value * con.weight);
        }
        currentvalue = currentvalue + bias;


        value = Mathf.Max(0, currentvalue);


    }

    public Node() {

    }

    public Node(Node copy) {
        connectedNodes = new List<Connection>();
        id = copy.id;
        value = 0;
        bias = copy.bias;
        layer = copy.layer;

        connectedNodes = copy.connectedNodes.ConvertAll(con => new Connection(con)).ToList();
    }
}

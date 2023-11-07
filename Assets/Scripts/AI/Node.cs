//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:28:58
//  GitUser: azzinoth01
//===================================================

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Node {
    [SerializeField] private int _id;
    [SerializeField] private float _value;
    [SerializeField] private float _bias;
    [SerializeField] private List<Connection> _connectedNodes;
    [SerializeField] private int _layer;

    public List<Connection> ConnectedNodes {
        get {
            return _connectedNodes;
        }

        set {
            _connectedNodes = value;
        }
    }

    public int Id {
        get {
            return _id;
        }
    }

    public int Layer {
        get {
            return _layer;
        }
    }

    public float Bias {
        get {
            return _bias;
        }

        set {
            _bias = value;
        }
    }

    public float Value {
        get {
            return _value;
        }

        set {
            _value = value;
        }
    }

    public void CalculateValue() {
        float currentvalue = 0;

        foreach (Connection con in _connectedNodes) {
            currentvalue = currentvalue + (con.Node._value * con.Weight);
        }
        currentvalue = currentvalue + _bias;
        _value = Mathf.Max(0, currentvalue);
    }

    public Node(int id, float bias, int layer) {
        _id = id;
        _bias = bias;
        _connectedNodes = new List<Connection>();
        _layer = layer;

    }

    public Node(Node copy) {
        _connectedNodes = new List<Connection>();
        _id = copy._id;
        _value = 0;
        _bias = copy._bias;
        _layer = copy._layer;

        _connectedNodes = copy._connectedNodes.ConvertAll(con => new Connection(con)).ToList();
    }
}

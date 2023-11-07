//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:28:40
//  GitUser: azzinoth01
//===================================================
using System;
using UnityEngine;

[Serializable]
public class Connection {
    [SerializeField] private int _nodeId;
    [SerializeField] private float _weight;
    private Node _node;

    public Node Node {
        get {
            return _node;
        }

        set {
            _node = value;
        }
    }

    public float Weight {
        get {
            return _weight;
        }

        set {
            _weight = value;
        }
    }

    public int NodeId {
        get {
            return _nodeId;
        }

        set {
            _nodeId = value;
        }
    }

    public Connection() {
    }
    public Connection(Connection copy) {
        _nodeId = copy._nodeId;
        _weight = copy._weight;
        _node = null;
    }
}

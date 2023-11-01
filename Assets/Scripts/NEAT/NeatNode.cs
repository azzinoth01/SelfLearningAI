using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class NeatNode {
    private int _id;
    private float _value;
    private NeatNodeType _type;
    private List<int> _inputList;
    private List<NeatNode> _outputNodeList;
    private int _layer;

    public int Id {
        get {
            return _id;
        }
    }

    public float Value {
        get {
            return _value;
        }
        set {
            if (_type == NeatNodeType.input) {
                _value = value;
            }

        }

    }

    public NeatNodeType Type {
        get {
            return _type;
        }


    }

    public List<int> InputList {
        get {
            return _inputList;
        }

    }

    public int Layer {
        get {
            return _layer;
        }

        set {
            _layer = value;
        }
    }

    public List<NeatNode> OutputList {
        get {
            return _outputNodeList;
        }

    }

    public void CalculateNode(NeatBrain brain) {
        float currentValue = 0;
        foreach (int conId in _inputList) {
            NeatConnection con = brain.connectionDictonary[conId];
            if (con.Enabled) {
                currentValue = currentValue + (con.Weight * brain.nodeDictonary[con.InputNodeId]._value);
            }
        }
    }

    public NeatNode(int id = -1, NeatNodeType type = NeatNodeType.hidden) {
        _inputList = new List<int>();
        _outputNodeList = new List<NeatNode>();
        _layer = -1;

        if (id == -1) {
            _id = NeatHistoryInstance.Instance.History.CurrentNodeId;
        }
        _type = type;

    }
    public NeatNode(NeatNode copy) {
        _id = copy._id;
        _value = 0;
        _type = copy._type;
        _layer = copy._layer;

        _inputList = copy._inputList.ToList();
        _outputNodeList = copy._outputNodeList.ConvertAll(x => new NeatNode(x));
    }

    public void CalculateLayer(int layer) {

        if (_layer <= layer) {
            _layer = layer + 1;
        }
        foreach (NeatNode node in _outputNodeList) {
            node.CalculateLayer(_layer);
        }

    }
}

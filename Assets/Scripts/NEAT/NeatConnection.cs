using System;

[Serializable]
public class NeatConnection {
    private int _id;
    private int _inputNodeId;
    private int _outputNodeId;
    private float _weight;
    private bool _enabled;

    public int Id {
        get {
            return _id;
        }

    }

    public int InputNodeId {
        get {
            return _inputNodeId;
        }

    }

    public int OutputNodeId {
        get {
            return _outputNodeId;
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

    public bool Enabled {
        get {
            return _enabled;
        }

        set {
            _enabled = value;
        }
    }


    public NeatConnection(int inputNodeId, int outputNodeId, float weight = 1f) {
        _id = NeatHistoryInstance.Instance.History.CurrentConnectionId;
        _inputNodeId = inputNodeId;
        _outputNodeId = outputNodeId;
        _weight = weight;
        _enabled = true;

        NeatHistoryInstance.Instance.History.AddConnection(this);
    }

    public NeatConnection(NeatConnection copy) {
        _id = copy._id;
        _inputNodeId = copy._inputNodeId;
        _outputNodeId = copy._outputNodeId;
        _weight = copy._weight;
        _enabled = copy._enabled;

    }
}

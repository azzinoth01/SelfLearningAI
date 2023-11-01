using System;

[Serializable]
public class Connection {
    public int nodeId;
    public float weight;
    private Node _node;

    public Node Node {
        get {
            return _node;
        }

        set {
            _node = value;
        }
    }
    public Connection() {
    }
    public Connection(Connection copy) {
        nodeId = copy.nodeId;
        weight = copy.weight;
        _node = null;
    }
}

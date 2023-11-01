using System.Collections.Generic;

public class NeatHistory {
    public List<NeatConnection> connectionList;
    public Dictionary<(int, int), NeatConnection> connectionDictonary;

    private int _currentConnectionId;
    private int _currentNodeId;

    public NeatHistory() {
        connectionList = new List<NeatConnection>();

        _currentConnectionId = 0;
        _currentNodeId = 0;
    }

    public int CurrentConnectionId {
        get {
            _currentConnectionId = _currentConnectionId + 1;
            return _currentConnectionId;
        }


    }

    public int CurrentNodeId {
        get {
            _currentNodeId = _currentNodeId + 1;
            return _currentNodeId;
        }
    }

    public void AddConnection(NeatConnection con) {

        connectionDictonary.Add((con.InputNodeId, con.OutputNodeId), con);

    }

    public void CreateDictonary() {
        connectionDictonary = new Dictionary<(int, int), NeatConnection>();

        foreach (NeatConnection con in connectionList) {
            connectionDictonary.Add((con.InputNodeId, con.OutputNodeId), con);
        }

    }
}

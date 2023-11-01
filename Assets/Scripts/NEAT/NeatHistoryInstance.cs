public class NeatHistoryInstance {
    private static NeatHistoryInstance _instance;
    private NeatHistory _history;
    public static NeatHistoryInstance Instance {
        get {
            if (_instance == null) {
                _instance = new NeatHistoryInstance();
            }
            return _instance;
        }

    }

    public NeatHistory History {
        get {
            return _history;
        }

        set {
            _history = value;
            _history.CreateDictonary();
        }
    }

    private NeatHistoryInstance() {

    }

}

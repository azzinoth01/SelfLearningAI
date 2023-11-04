using UnityEngine;

public class CheckPoint : MonoBehaviour {

    [SerializeField] private CheckPoint _nextCheckpoint;
    private CheckPoint _previousCheckpoint;

    public CheckPoint NextCheckpoint {
        get {
            return _nextCheckpoint;
        }
    }

    public CheckPoint PreviousCheckpoint {
        get {
            return _previousCheckpoint;
        }
    }

    private void Awake() {
        NextCheckpoint._previousCheckpoint = this;
    }
    private void OnDrawGizmos() {
        if (Application.isPlaying) {
            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(transform.position, _nextCheckpoint.transform.position);
        }
    }
}

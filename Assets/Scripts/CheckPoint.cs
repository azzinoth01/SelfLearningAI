using UnityEngine;

public class CheckPoint : MonoBehaviour {
    public CheckPoint nextCheckpoint;
    public CheckPoint previousCheckpoint;

    private void Awake() {
        nextCheckpoint.previousCheckpoint = this;
    }

    private void Update() {
        Debug.DrawLine(transform.position, nextCheckpoint.transform.position, Color.yellow);
    }
}

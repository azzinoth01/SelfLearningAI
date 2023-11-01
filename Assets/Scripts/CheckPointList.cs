using System.Collections.Generic;
using UnityEngine;

public class CheckPointList : MonoBehaviour {

    public List<CheckPoint> checkpoints;
    private List<Vector3> _checkpointPositionList;
    // Start is called before the first frame update
    void Start() {
        GenerateCheckpointVectors();
    }

    public CheckPoint GetClosestCheckPoint(Vector3 position) {
        if (_checkpointPositionList == null || _checkpointPositionList.Count == 0) {
            GenerateCheckpointVectors();
        }

        int index = 0;
        float currentDistance = 0;
        float distance = (position - _checkpointPositionList[index]).magnitude;


        for (int i = 1; i < _checkpointPositionList.Count;) {
            currentDistance = (position - _checkpointPositionList[i]).magnitude;
            if (currentDistance < distance) {
                distance = currentDistance;
                index = i;
            }


            i = i + 1;
        }



        return checkpoints[index];
    }
    public void GenerateCheckpointVectors() {
        _checkpointPositionList = new List<Vector3>();
        foreach (CheckPoint game in checkpoints) {
            _checkpointPositionList.Add(game.transform.position);
        }
    }
}

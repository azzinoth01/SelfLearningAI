//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:30:29
//  GitUser: azzinoth01
//===================================================
using System.Collections.Generic;
using UnityEngine;

public class CheckPointList : MonoBehaviour {

    [SerializeField] private List<CheckPoint> _checkpoints;
    private List<Vector3> _checkpointPositionList;
    // Start is called before the first frame update
    void Start() {
        GenerateCheckpointVectors();
    }

    public CheckPoint GetClosestCheckPoint(Vector3 position) {
        if (_checkpointPositionList == null || _checkpointPositionList.Count == 0) {
            GenerateCheckpointVectors();
        }
        int index = LineCalculations.GetIndexOfClosesPositionWithinList(_checkpointPositionList, position);
        return _checkpoints[index];
    }
    private void GenerateCheckpointVectors() {
        _checkpointPositionList = new List<Vector3>();
        foreach (CheckPoint game in _checkpoints) {
            _checkpointPositionList.Add(game.transform.position);
        }
    }
}

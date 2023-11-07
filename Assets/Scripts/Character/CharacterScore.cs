//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:28:10
//  GitUser: azzinoth01
//===================================================
using UnityEngine;

public class CharacterScore : MonoBehaviour {



    private CheckPointList _checkpointList;
    private Vector3 _lastPointOnTrack;
    [SerializeField] private float _currentScore;
    private CheckPoint _closestPoint;
    private float _increaseScoreMultiplier;


    public CheckPointList CheckpointList {
        get {
            return _checkpointList;
        }

        set {
            _checkpointList = value;
        }
    }

    public float CurrentScore {
        get {
            return _currentScore;
        }
    }
    private void OnEnable() {
        ResetValues();
    }

    public void ResetValues() {

        if (_checkpointList != null) {
            _closestPoint = _checkpointList.GetClosestCheckPoint(transform.position);

            _lastPointOnTrack = _closestPoint.transform.position;
        }
        _currentScore = 1;
        _increaseScoreMultiplier = 0;

    }

    // Update is called once per frame
    void Update() {
        if (_checkpointList == null) {
            return;
        }

        CheckPoint currentClosesPoint = _checkpointList.GetClosestCheckPoint(transform.position);

        //Debug.DrawLine(transform.position, currentClosesPoint.transform.position, Color.green);

        if (currentClosesPoint != _closestPoint) {
            _closestPoint = currentClosesPoint;
            _increaseScoreMultiplier = _increaseScoreMultiplier + 1f;
        }

        Vector3 nextDotPos = LineCalculations.GetClosestPointOnLine(_closestPoint.transform.position, _closestPoint.NextCheckpoint.transform.position, transform.position);
        float distanceNextLine = Vector3.Distance(transform.position, nextDotPos);

        Vector3 previousDotPos = LineCalculations.GetClosestPointOnLine(_closestPoint.transform.position, _closestPoint.PreviousCheckpoint.transform.position, transform.position);
        float distancePreviousLine = Vector3.Distance(transform.position, previousDotPos);

        Vector3 nextLineDir = (_closestPoint.NextCheckpoint.transform.position - _closestPoint.transform.position).normalized;
        Vector3 previousLineDir = (_closestPoint.PreviousCheckpoint.transform.position - _closestPoint.transform.position).normalized;

        float distance;
        if (distanceNextLine <= distancePreviousLine) {
            distance = LineCalculations.GetSignedDistance(nextDotPos, _lastPointOnTrack, nextLineDir);
            _lastPointOnTrack = nextDotPos;
        }
        else {
            distance = LineCalculations.GetSignedDistance(_lastPointOnTrack, previousDotPos, previousLineDir);
            _lastPointOnTrack = previousDotPos;
        }
        int negativeMultiplier = 1;
        // when distance is negative it means the character moves back along the track and this should negativly apply to score
        if (distance < 0) {
            negativeMultiplier = 2;
        }
        _currentScore = _currentScore + (distance * negativeMultiplier * (1 + _increaseScoreMultiplier));
        if (_currentScore < 0) {
            // when score gets less than 0 through moving backwards it gets an additional negative points
            // in order to ensure this Ai does not continue to populate 
            _currentScore = _currentScore - 1000;
            gameObject.SetActive(false);


        }
    }

    private void OnCollisionEnter(Collision collision) {
        // wall hit
        if (collision.gameObject.layer == 6) {
            gameObject.SetActive(false);
        }
    }

}

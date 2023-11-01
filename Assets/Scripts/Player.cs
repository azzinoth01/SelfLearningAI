using UnityEngine;

public class Player : MonoBehaviour {



    public CheckPointList checkpointList;

    public float distanceNextLine;
    public float distancePreviousLine;

    public Vector3 lastPointOnTrack;
    //public TMPro.TMP_Text score;
    public float currentScore;
    public CheckPoint closestPoint;
    public float increaseScoreMultiplier;



    // Start is called before the first frame update
    void Start() {
        ResetValues();

    }


    public void ResetValues() {
        currentScore = 1;
        closestPoint = checkpointList.GetClosestCheckPoint(transform.position);

        lastPointOnTrack = closestPoint.transform.position;
        increaseScoreMultiplier = 0;

    }

    // Update is called once per frame
    void Update() {
        //currentScore = currentScore - (1f * Time.deltaTime);

        CheckPoint currentClosesPoint = checkpointList.GetClosestCheckPoint(transform.position);

        if (currentClosesPoint != closestPoint) {
            closestPoint = currentClosesPoint;
            increaseScoreMultiplier = increaseScoreMultiplier + 1f;
        }


        Vector3 nextDotPos = GetClosestPointOnLine(closestPoint.transform.position, closestPoint.nextCheckpoint.transform.position, transform.position);
        //Debug.DrawLine(transform.position, nextDotPos, Color.magenta);
        distanceNextLine = Vector3.Distance(transform.position, nextDotPos);

        Vector3 previousDotPos = GetClosestPointOnLine(closestPoint.transform.position, closestPoint.previousCheckpoint.transform.position, transform.position);
        //Debug.DrawLine(transform.position, previousDotPos, Color.green);
        distancePreviousLine = Vector3.Distance(transform.position, previousDotPos);


        Vector3 nextLineDir = (closestPoint.nextCheckpoint.transform.position - closestPoint.transform.position).normalized;


        //Debug.DrawLine(transform.position, transform.position + nextLineDir * 5, Color.red);

        Vector3 prevoisLineDir = (closestPoint.previousCheckpoint.transform.position - closestPoint.transform.position).normalized;

        //Debug.DrawLine(transform.position, transform.position + prevoisLineDir * 5, Color.blue);


        float distance;
        if (distanceNextLine <= distancePreviousLine) {

            float dot = Vector3.Dot((nextDotPos - lastPointOnTrack).normalized, nextLineDir);
            //Debug.Log("Dot: " + dot);
            distance = Vector3.Distance(lastPointOnTrack, nextDotPos) * dot;
            if (distance < 0) {
                distance = distance * 2;
            }
            lastPointOnTrack = nextDotPos;


        }
        else {
            float dot = Vector3.Dot((lastPointOnTrack - previousDotPos).normalized, prevoisLineDir);
            //Debug.Log("Dot: " + dot);
            distance = Vector3.Distance(lastPointOnTrack, previousDotPos) * dot;
            lastPointOnTrack = previousDotPos;
            if (distance < 0) {
                distance = distance * 2;
            }
        }

        currentScore = currentScore + (distance * (1 + increaseScoreMultiplier));

        //score.text = "Score: " + currentScore;

        if (currentScore < 0) {
            currentScore = currentScore - 1000;
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public Vector3 GetClosestPointOnLine(Vector3 start, Vector3 end, Vector3 point) {
        Vector3 line = end - start;
        Vector3 pointLine = point - start;
        float dot = Mathf.Clamp(Vector3.Dot(pointLine, line.normalized), 0, line.magnitude);
        Vector3 dotline = line.normalized * dot;
        Vector3 dotPos = start + dotline;

        return dotPos;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == 6) {
            //Debug.Log("wall hit");

            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

}

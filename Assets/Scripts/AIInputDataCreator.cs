using UnityEngine;

public class AIInputDataCreator : MonoBehaviour {

    public float size;

    public float frontDistance;
    public float leftFrontDistance;
    public float rightFrontDistance;
    public float leftDistance;
    public float rightDistance;
    public float backDistance;
    public float leftBackDistance;
    public float rightBackDistance;

    public LayerMask layerMask;

    public float[] aiInputData;
    public float maxinputDataValue;


    // Start is called before the first frame update
    void Start() {
        aiInputData = new float[10];
    }
    public void ResetValues() {
        aiInputData = new float[10];
        maxinputDataValue = 0;
    }


    // Update is called once per frame
    void Update() {
        RaycastHit hit;

        Physics.Raycast(transform.position, transform.rotation * Vector3.right, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore);
        frontDistance = hit.distance - (size / 2);
        aiInputData[0] = frontDistance;
        maxinputDataValue = frontDistance;




        Physics.Raycast(transform.position, transform.rotation * (Vector3.right + Vector3.forward), out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore);
        leftFrontDistance = hit.distance - (size / 2);
        aiInputData[1] = leftFrontDistance;
        if (maxinputDataValue < leftFrontDistance) {
            maxinputDataValue = leftFrontDistance;
        }


        Physics.Raycast(transform.position, transform.rotation * (Vector3.right + Vector3.back), out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore);
        rightFrontDistance = hit.distance - (size / 2);
        aiInputData[2] = rightFrontDistance;
        if (maxinputDataValue < rightFrontDistance) {
            maxinputDataValue = rightFrontDistance;
        }

        Physics.Raycast(transform.position, transform.rotation * Vector3.forward, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore);
        leftDistance = hit.distance - (size / 2);
        aiInputData[3] = leftDistance;
        if (maxinputDataValue < leftDistance) {
            maxinputDataValue = leftDistance;
        }

        Physics.Raycast(transform.position, transform.rotation * Vector3.back, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore);
        rightDistance = hit.distance - (size / 2);
        aiInputData[4] = rightDistance;
        if (maxinputDataValue < rightDistance) {
            maxinputDataValue = rightDistance;
        }

        Physics.Raycast(transform.position, transform.rotation * Vector3.left, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore);
        backDistance = hit.distance - (size / 2);
        aiInputData[5] = backDistance;
        if (maxinputDataValue < backDistance) {
            maxinputDataValue = backDistance;
        }

        Physics.Raycast(transform.position, transform.rotation * (Vector3.left + Vector3.forward), out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore);
        leftBackDistance = hit.distance - (size / 2);
        aiInputData[6] = leftBackDistance;
        if (maxinputDataValue < leftBackDistance) {
            maxinputDataValue = leftBackDistance;
        }

        Physics.Raycast(transform.position, transform.rotation * (Vector3.left + Vector3.back), out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore);
        rightBackDistance = hit.distance - (size / 2);
        aiInputData[7] = rightBackDistance;
        if (maxinputDataValue < rightBackDistance) {
            maxinputDataValue = rightBackDistance;
        }


        Vector3 displacement = new Vector3(0, 0, 1.5f);
        Physics.Raycast(transform.position + displacement, transform.rotation * Vector3.right, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore);

        aiInputData[8] = hit.distance - (size / 2);
        if (maxinputDataValue < aiInputData[8]) {
            maxinputDataValue = aiInputData[8];
        }

        displacement = new Vector3(0, 0, -1.5f);
        Physics.Raycast(transform.position + displacement, transform.rotation * Vector3.right, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore);

        aiInputData[9] = hit.distance - (size / 2);
        if (maxinputDataValue < aiInputData[9]) {
            maxinputDataValue = aiInputData[9];
        }


    }

    private void OnDisable() {
        ResetValues();
    }
}

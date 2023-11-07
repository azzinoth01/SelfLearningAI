//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:29:50
//  GitUser: azzinoth01
//===================================================
using UnityEngine;

public class AIInputDataCreator : MonoBehaviour {

    private float[] _aiInputData;




    public float[] AiInputData {
        get {
            return _aiInputData;
        }
    }
    private void Awake() {
        _aiInputData = new float[10];
    }
    private void OnEnable() {
        for (int i = 0; i < 10; i++) {
            _aiInputData[i] = 0;
        }
    }

    private void Update() {
        RaycastHit hit;
        LayerMask mask = SettingsObject.Instance.AILayerMask;
        int size = SettingsObject.Instance.AIObjectSize / 2;
        int length = SettingsObject.Instance.AIRaycastMaxLength;

        Physics.Raycast(transform.position, transform.rotation * Vector3.right, out hit, length, mask, QueryTriggerInteraction.Ignore);
        _aiInputData[0] = hit.distance - size;

        Physics.Raycast(transform.position, transform.rotation * (Vector3.right + Vector3.forward), out hit, length, mask, QueryTriggerInteraction.Ignore);
        _aiInputData[1] = hit.distance - size;

        Physics.Raycast(transform.position, transform.rotation * (Vector3.right + Vector3.back), out hit, length, mask, QueryTriggerInteraction.Ignore);
        _aiInputData[2] = hit.distance - size;

        Physics.Raycast(transform.position, transform.rotation * Vector3.forward, out hit, length, mask, QueryTriggerInteraction.Ignore);
        _aiInputData[3] = hit.distance - size;

        Physics.Raycast(transform.position, transform.rotation * Vector3.back, out hit, length, mask, QueryTriggerInteraction.Ignore);
        _aiInputData[4] = hit.distance - size;

        Physics.Raycast(transform.position, transform.rotation * Vector3.left, out hit, length, mask, QueryTriggerInteraction.Ignore);
        _aiInputData[5] = hit.distance - size;

        Physics.Raycast(transform.position, transform.rotation * (Vector3.left + Vector3.forward), out hit, length, mask, QueryTriggerInteraction.Ignore);
        _aiInputData[6] = hit.distance - size;

        Physics.Raycast(transform.position, transform.rotation * (Vector3.left + Vector3.back), out hit, length, mask, QueryTriggerInteraction.Ignore);
        _aiInputData[7] = hit.distance - size;

        Vector3 displacement = new Vector3(0, 0, 1.5f);
        Physics.Raycast(transform.position + displacement, transform.rotation * Vector3.right, out hit, length, mask, QueryTriggerInteraction.Ignore);
        _aiInputData[8] = hit.distance - size;

        displacement = -displacement;
        Physics.Raycast(transform.position + displacement, transform.rotation * Vector3.right, out hit, length, mask, QueryTriggerInteraction.Ignore);
        _aiInputData[9] = hit.distance - size;
    }

    private void OnDisable() {
        this.enabled = false;
    }

}

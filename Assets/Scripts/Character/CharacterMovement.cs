//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:29:11
//  GitUser: azzinoth01
//===================================================
using UnityEngine;


public class CharacterMovement : MonoBehaviour, ICharacter {

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private Rigidbody _body;
    private ICharacterMovement _controlObject;

    private void Awake() {
        _body = GetComponent<Rigidbody>();
        RegisterToCharacterContainer();
    }

    // Update is called once per frame
    void Update() {
        if (_controlObject == null) {
            return;
        }
        if (_controlObject.RotationDirection != 0) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation * Quaternion.Euler(0, 90 * _controlObject.RotationDirection, 0), _rotateSpeed * Time.deltaTime);

        }
        Vector3 rotatedMove = new Vector3(_controlObject.MoveDirection * _moveSpeed, 0, 0);
        rotatedMove = transform.rotation * rotatedMove;
        _body.velocity = rotatedMove;

    }

    public void RegisterToCharacterContainer() {
        CharacterContainer.Instance.ControlAbleCharacterQueue.Push(this);
    }

    public bool TakeControl(ICharacterMovement controler) {
        if (_controlObject != null) {
            return false;
        }
        _controlObject = controler;
        return true;
    }

    public bool ReleaseControl(ICharacterMovement controler) {
        if (_controlObject != controler) {
            return false;
        }
        _controlObject = null;
        gameObject.SetActive(false);
        RegisterToCharacterContainer();
        return true;
    }

    public GameObject GetGameObject() {
        return gameObject;
    }

    public bool CanTakeControl() {
        return _controlObject == null;
    }
}

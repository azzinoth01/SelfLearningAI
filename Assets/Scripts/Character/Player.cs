using UnityEngine;

public class Player : MonoBehaviour, PlayerInput.IMovePlayerActions, ICharacterMovement {

    [SerializeField] private Transform _startPosition;
    [SerializeField] private CheckPointList _checkPointList;
    [SerializeField] private bool _takeControl;
    [ShowOnly, SerializeField] private bool _hasControl;
    private ICharacter _controledCharacter;


    private PlayerInput _inputs;
    public int MoveDirection {
        get;
        set;
    }

    public int RotationDirection {
        get;
        set;
    }

    public void OnMoveBackwards(UnityEngine.InputSystem.InputAction.CallbackContext context) {

        if (context.started) {
            MoveDirection = MoveDirection - 1;
        }
        else if (context.canceled) {
            MoveDirection = MoveDirection + 1;
        }
    }

    public void OnMoveForward(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        if (context.started) {
            MoveDirection = MoveDirection + 1;
        }
        else if (context.canceled) {
            MoveDirection = MoveDirection - 1;
        }
    }

    public void OnRotateLeft(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        if (context.started) {
            RotationDirection = RotationDirection - 1;
        }
        else if (context.canceled) {
            RotationDirection = RotationDirection + 1;
        }
    }

    public void OnRotateRigth(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        if (context.started) {
            RotationDirection = RotationDirection + 1;
        }
        else if (context.canceled) {
            RotationDirection = RotationDirection - 1;
        }
    }

    private void OnEnable() {
        if (_inputs == null) {
            _inputs = new PlayerInput();
            _inputs.MovePlayer.SetCallbacks(this);
            _inputs.Enable();

        }
    }

    private void Update() {
        if (_takeControl == true && _hasControl == false) {
            if (CharacterContainer.Instance.ControlAbleCharacterQueue.Count != 0) {
                _controledCharacter = CharacterContainer.Instance.ControlAbleCharacterQueue.Pop();


                if (_controledCharacter.TakeControl(this) == false) {
                    _controledCharacter = null;
                    return;
                }
                GameObject obj = _controledCharacter.GetGameObject();
                obj.transform.position = _startPosition.position;
                obj.transform.rotation = Quaternion.identity;
                obj.GetComponent<CharacterScore>().CheckpointList = _checkPointList;

                obj.SetActive(true);

                _hasControl = true;
            }
        }
        else if (_takeControl == false && _hasControl == true) {
            if (_controledCharacter.ReleaseControl(this)) {
                _hasControl = false;
                _controledCharacter = null;
            }
        }
    }
    private void OnDisable() {
        _inputs.Dispose();
        _inputs = null;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour, NewInputSystem.IMovePlayerActions {

    private NewInputSystem _controller;

    public int move;
    public int rotate;


    public float moveSpeed;
    public float rotateSpeed;

    public Rigidbody body;

    public float aliveTime;

    public void OnMoveBackwards(InputAction.CallbackContext context) {

        if (context.started) {
            move = move - 1;
        }
        else if (context.canceled) {
            move = move + 1;
        }
    }

    public void OnMoveForward(InputAction.CallbackContext context) {
        if (context.started) {
            move = move + 1;
        }
        else if (context.canceled) {
            move = move - 1;
        }
    }

    public void OnRotateLeft(InputAction.CallbackContext context) {
        if (context.started) {
            rotate = rotate - 1;
        }
        else if (context.canceled) {
            rotate = rotate + 1;
        }
    }

    public void OnRotateRigth(InputAction.CallbackContext context) {
        if (context.started) {
            rotate = rotate + 1;
        }
        else if (context.canceled) {
            rotate = rotate - 1;
        }
    }





    // Start is called before the first frame update
    void Start() {

        if (_controller == null) {
            _controller = new NewInputSystem();
            _controller.MovePlayer.SetCallbacks(this);
            _controller.MovePlayer.Enable();

        }

    }

    // Update is called once per frame
    void Update() {
        aliveTime = aliveTime + Time.deltaTime;


        if (rotate != 0) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation * Quaternion.Euler(0, 90 * rotate, 0), rotateSpeed * Time.deltaTime);

        }

        Vector3 rotatedMove = new Vector3(move * moveSpeed, 0, 0);

        rotatedMove = transform.rotation * rotatedMove;



        body.velocity = rotatedMove;

    }

    private void OnDisable() {
        rotate = 0;
        move = 0;
    }

}

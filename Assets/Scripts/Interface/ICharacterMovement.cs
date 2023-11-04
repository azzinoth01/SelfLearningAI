using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterMovement {

    public int MoveDirection {
        get;
        set;
    }
    public int RotationDirection {
        get;
        set;
    }
}

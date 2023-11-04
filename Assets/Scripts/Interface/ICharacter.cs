using UnityEngine;

public interface ICharacter {
    public void RegisterToCharacterContainer();
    public bool TakeControl(ICharacterMovement controler);
    public bool ReleaseControl(ICharacterMovement controler);
    public GameObject GetGameObject();
    public bool CanTakeControl();

}

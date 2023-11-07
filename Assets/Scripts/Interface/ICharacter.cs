//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:29:31
//  GitUser: azzinoth01
//===================================================
using UnityEngine;

public interface ICharacter {
    public void RegisterToCharacterContainer();
    public bool TakeControl(ICharacterMovement controler);
    public bool ReleaseControl(ICharacterMovement controler);
    public GameObject GetGameObject();
    public bool CanTakeControl();

}

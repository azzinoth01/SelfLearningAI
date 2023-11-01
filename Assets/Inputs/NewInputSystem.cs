//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Inputs/NewInputSystem.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @NewInputSystem : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @NewInputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""NewInputSystem"",
    ""maps"": [
        {
            ""name"": ""MovePlayer"",
            ""id"": ""9094d99f-e096-41ce-b004-d5973ecf4efa"",
            ""actions"": [
                {
                    ""name"": ""MoveForward"",
                    ""type"": ""Button"",
                    ""id"": ""db187dbf-4268-4fc1-83c3-313553584626"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateLeft"",
                    ""type"": ""Button"",
                    ""id"": ""777d1cbb-a8c2-44f1-8271-c1822b3083f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateRigth"",
                    ""type"": ""Button"",
                    ""id"": ""451ceeb7-8c57-438e-b50a-b85054b798a6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveBackwards"",
                    ""type"": ""Button"",
                    ""id"": ""f8c4755c-d78c-475b-9f4c-122d79095685"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""454363f4-2ab6-4dd8-b6d7-ba6bfaf9361e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a834344-19b3-4d4b-b416-ee44058f46b6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a9f0ce6-f521-4d32-a2bf-a49325be13b9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateRigth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1bf0bb88-7132-4ae8-8f44-bf2c3f721b96"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBackwards"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MovePlayer
        m_MovePlayer = asset.FindActionMap("MovePlayer", throwIfNotFound: true);
        m_MovePlayer_MoveForward = m_MovePlayer.FindAction("MoveForward", throwIfNotFound: true);
        m_MovePlayer_RotateLeft = m_MovePlayer.FindAction("RotateLeft", throwIfNotFound: true);
        m_MovePlayer_RotateRigth = m_MovePlayer.FindAction("RotateRigth", throwIfNotFound: true);
        m_MovePlayer_MoveBackwards = m_MovePlayer.FindAction("MoveBackwards", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // MovePlayer
    private readonly InputActionMap m_MovePlayer;
    private IMovePlayerActions m_MovePlayerActionsCallbackInterface;
    private readonly InputAction m_MovePlayer_MoveForward;
    private readonly InputAction m_MovePlayer_RotateLeft;
    private readonly InputAction m_MovePlayer_RotateRigth;
    private readonly InputAction m_MovePlayer_MoveBackwards;
    public struct MovePlayerActions
    {
        private @NewInputSystem m_Wrapper;
        public MovePlayerActions(@NewInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveForward => m_Wrapper.m_MovePlayer_MoveForward;
        public InputAction @RotateLeft => m_Wrapper.m_MovePlayer_RotateLeft;
        public InputAction @RotateRigth => m_Wrapper.m_MovePlayer_RotateRigth;
        public InputAction @MoveBackwards => m_Wrapper.m_MovePlayer_MoveBackwards;
        public InputActionMap Get() { return m_Wrapper.m_MovePlayer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovePlayerActions set) { return set.Get(); }
        public void SetCallbacks(IMovePlayerActions instance)
        {
            if (m_Wrapper.m_MovePlayerActionsCallbackInterface != null)
            {
                @MoveForward.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnMoveForward;
                @MoveForward.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnMoveForward;
                @MoveForward.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnMoveForward;
                @RotateLeft.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnRotateLeft;
                @RotateRigth.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnRotateRigth;
                @RotateRigth.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnRotateRigth;
                @RotateRigth.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnRotateRigth;
                @MoveBackwards.started -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnMoveBackwards;
                @MoveBackwards.performed -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnMoveBackwards;
                @MoveBackwards.canceled -= m_Wrapper.m_MovePlayerActionsCallbackInterface.OnMoveBackwards;
            }
            m_Wrapper.m_MovePlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveForward.started += instance.OnMoveForward;
                @MoveForward.performed += instance.OnMoveForward;
                @MoveForward.canceled += instance.OnMoveForward;
                @RotateLeft.started += instance.OnRotateLeft;
                @RotateLeft.performed += instance.OnRotateLeft;
                @RotateLeft.canceled += instance.OnRotateLeft;
                @RotateRigth.started += instance.OnRotateRigth;
                @RotateRigth.performed += instance.OnRotateRigth;
                @RotateRigth.canceled += instance.OnRotateRigth;
                @MoveBackwards.started += instance.OnMoveBackwards;
                @MoveBackwards.performed += instance.OnMoveBackwards;
                @MoveBackwards.canceled += instance.OnMoveBackwards;
            }
        }
    }
    public MovePlayerActions @MovePlayer => new MovePlayerActions(this);
    public interface IMovePlayerActions
    {
        void OnMoveForward(InputAction.CallbackContext context);
        void OnRotateLeft(InputAction.CallbackContext context);
        void OnRotateRigth(InputAction.CallbackContext context);
        void OnMoveBackwards(InputAction.CallbackContext context);
    }
}
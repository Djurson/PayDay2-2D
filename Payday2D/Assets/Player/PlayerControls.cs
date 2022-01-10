// GENERATED AUTOMATICALLY FROM 'Assets/Player/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""KeyboardInputs"",
            ""id"": ""412f06fa-979f-4374-857f-c3f5054d73f8"",
            ""actions"": [
                {
                    ""name"": ""MovementVertical"",
                    ""type"": ""Value"",
                    ""id"": ""81d250ab-8e8b-4ff3-89f9-79d424263efc"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MovementHorizontal"",
                    ""type"": ""Button"",
                    ""id"": ""bc9d8eda-450e-4ded-8987-e0f9824d68ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shift"",
                    ""type"": ""Button"",
                    ""id"": ""43fd6c82-7625-4358-b3b2-cb7177f96550"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""b3fc7e57-7b03-4009-b531-18567aa912d8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""8f8781a5-1520-4b62-8d70-fe5f48e573f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ThrowBags"",
                    ""type"": ""Button"",
                    ""id"": ""2e887e2d-64c5-45b3-8457-1103c06753fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""128eb2cb-4311-4d0a-97e8-dd67027240da"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""MovementVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5de5fcc0-47a4-4b13-8d09-45513da04dfa"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=-1)"",
                    ""groups"": """",
                    ""action"": ""MovementVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f413703e-4ff4-42e6-9a45-4a7dc26c24de"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""MovementHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa488c7b-aaef-4148-8933-a7c2ca95acce"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=-1)"",
                    ""groups"": """",
                    ""action"": ""MovementHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""360858ba-cda3-43a3-8693-5b390196c523"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c507c905-05ed-463a-87eb-e9d22a95f1df"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bffc697f-f9a8-45eb-ab53-9675016230ce"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7cdd7de9-62af-400d-9261-f944e23843c8"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrowBags"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // KeyboardInputs
        m_KeyboardInputs = asset.FindActionMap("KeyboardInputs", throwIfNotFound: true);
        m_KeyboardInputs_MovementVertical = m_KeyboardInputs.FindAction("MovementVertical", throwIfNotFound: true);
        m_KeyboardInputs_MovementHorizontal = m_KeyboardInputs.FindAction("MovementHorizontal", throwIfNotFound: true);
        m_KeyboardInputs_Shift = m_KeyboardInputs.FindAction("Shift", throwIfNotFound: true);
        m_KeyboardInputs_MousePosition = m_KeyboardInputs.FindAction("MousePosition", throwIfNotFound: true);
        m_KeyboardInputs_Interact = m_KeyboardInputs.FindAction("Interact", throwIfNotFound: true);
        m_KeyboardInputs_ThrowBags = m_KeyboardInputs.FindAction("ThrowBags", throwIfNotFound: true);
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

    // KeyboardInputs
    private readonly InputActionMap m_KeyboardInputs;
    private IKeyboardInputsActions m_KeyboardInputsActionsCallbackInterface;
    private readonly InputAction m_KeyboardInputs_MovementVertical;
    private readonly InputAction m_KeyboardInputs_MovementHorizontal;
    private readonly InputAction m_KeyboardInputs_Shift;
    private readonly InputAction m_KeyboardInputs_MousePosition;
    private readonly InputAction m_KeyboardInputs_Interact;
    private readonly InputAction m_KeyboardInputs_ThrowBags;
    public struct KeyboardInputsActions
    {
        private @PlayerControls m_Wrapper;
        public KeyboardInputsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovementVertical => m_Wrapper.m_KeyboardInputs_MovementVertical;
        public InputAction @MovementHorizontal => m_Wrapper.m_KeyboardInputs_MovementHorizontal;
        public InputAction @Shift => m_Wrapper.m_KeyboardInputs_Shift;
        public InputAction @MousePosition => m_Wrapper.m_KeyboardInputs_MousePosition;
        public InputAction @Interact => m_Wrapper.m_KeyboardInputs_Interact;
        public InputAction @ThrowBags => m_Wrapper.m_KeyboardInputs_ThrowBags;
        public InputActionMap Get() { return m_Wrapper.m_KeyboardInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardInputsActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardInputsActions instance)
        {
            if (m_Wrapper.m_KeyboardInputsActionsCallbackInterface != null)
            {
                @MovementVertical.started -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMovementVertical;
                @MovementVertical.performed -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMovementVertical;
                @MovementVertical.canceled -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMovementVertical;
                @MovementHorizontal.started -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMovementHorizontal;
                @MovementHorizontal.performed -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMovementHorizontal;
                @MovementHorizontal.canceled -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMovementHorizontal;
                @Shift.started -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnShift;
                @Shift.performed -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnShift;
                @Shift.canceled -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnShift;
                @MousePosition.started -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMousePosition;
                @Interact.started -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnInteract;
                @ThrowBags.started -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnThrowBags;
                @ThrowBags.performed -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnThrowBags;
                @ThrowBags.canceled -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnThrowBags;
            }
            m_Wrapper.m_KeyboardInputsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MovementVertical.started += instance.OnMovementVertical;
                @MovementVertical.performed += instance.OnMovementVertical;
                @MovementVertical.canceled += instance.OnMovementVertical;
                @MovementHorizontal.started += instance.OnMovementHorizontal;
                @MovementHorizontal.performed += instance.OnMovementHorizontal;
                @MovementHorizontal.canceled += instance.OnMovementHorizontal;
                @Shift.started += instance.OnShift;
                @Shift.performed += instance.OnShift;
                @Shift.canceled += instance.OnShift;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @ThrowBags.started += instance.OnThrowBags;
                @ThrowBags.performed += instance.OnThrowBags;
                @ThrowBags.canceled += instance.OnThrowBags;
            }
        }
    }
    public KeyboardInputsActions @KeyboardInputs => new KeyboardInputsActions(this);
    public interface IKeyboardInputsActions
    {
        void OnMovementVertical(InputAction.CallbackContext context);
        void OnMovementHorizontal(InputAction.CallbackContext context);
        void OnShift(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnThrowBags(InputAction.CallbackContext context);
    }
}

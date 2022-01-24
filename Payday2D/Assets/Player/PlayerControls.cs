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
                },
                {
                    ""name"": ""MouseFire"",
                    ""type"": ""Button"",
                    ""id"": ""97e6dd22-385f-4c37-b75c-6d2ccafbc77e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Value"",
                    ""id"": ""25dcac19-f29c-486a-b402-474758914196"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""f7c1cca1-f900-4c7b-97b9-d126fdfd0203"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeFireMode"",
                    ""type"": ""Button"",
                    ""id"": ""82369aaa-c500-47d3-9305-914f5706df55"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchPrimaryWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""cf694e08-6092-4d7e-be93-37b0a266d218"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchSecondary"",
                    ""type"": ""Button"",
                    ""id"": ""f8dc5c65-731f-45ee-8d25-35994a51e993"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseSwitchWeapon"",
                    ""type"": ""Value"",
                    ""id"": ""2d6a053f-5d02-4aea-9073-1d9d034221c7"",
                    ""expectedControlType"": ""Axis"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""9c7cf1a7-7174-4c23-9bc2-72737cd255e1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42e454de-549f-4371-bda7-54e8e0eab882"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e2abb6c-3639-4fd5-9d5d-544981209592"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f216bee7-d86f-4928-aa9c-6df864db6e79"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeFireMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0922670-f1ec-4feb-b69e-994fac33398d"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchPrimaryWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd371244-563c-4bdb-ae42-197a3f61df9b"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchSecondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""933275b9-7d32-4cbc-800e-e9d2eb3a45cd"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseSwitchWeapon"",
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
        m_KeyboardInputs_MouseFire = m_KeyboardInputs.FindAction("MouseFire", throwIfNotFound: true);
        m_KeyboardInputs_Reload = m_KeyboardInputs.FindAction("Reload", throwIfNotFound: true);
        m_KeyboardInputs_Aim = m_KeyboardInputs.FindAction("Aim", throwIfNotFound: true);
        m_KeyboardInputs_ChangeFireMode = m_KeyboardInputs.FindAction("ChangeFireMode", throwIfNotFound: true);
        m_KeyboardInputs_SwitchPrimaryWeapon = m_KeyboardInputs.FindAction("SwitchPrimaryWeapon", throwIfNotFound: true);
        m_KeyboardInputs_SwitchSecondary = m_KeyboardInputs.FindAction("SwitchSecondary", throwIfNotFound: true);
        m_KeyboardInputs_MouseSwitchWeapon = m_KeyboardInputs.FindAction("MouseSwitchWeapon", throwIfNotFound: true);
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
    private readonly InputAction m_KeyboardInputs_MouseFire;
    private readonly InputAction m_KeyboardInputs_Reload;
    private readonly InputAction m_KeyboardInputs_Aim;
    private readonly InputAction m_KeyboardInputs_ChangeFireMode;
    private readonly InputAction m_KeyboardInputs_SwitchPrimaryWeapon;
    private readonly InputAction m_KeyboardInputs_SwitchSecondary;
    private readonly InputAction m_KeyboardInputs_MouseSwitchWeapon;
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
        public InputAction @MouseFire => m_Wrapper.m_KeyboardInputs_MouseFire;
        public InputAction @Reload => m_Wrapper.m_KeyboardInputs_Reload;
        public InputAction @Aim => m_Wrapper.m_KeyboardInputs_Aim;
        public InputAction @ChangeFireMode => m_Wrapper.m_KeyboardInputs_ChangeFireMode;
        public InputAction @SwitchPrimaryWeapon => m_Wrapper.m_KeyboardInputs_SwitchPrimaryWeapon;
        public InputAction @SwitchSecondary => m_Wrapper.m_KeyboardInputs_SwitchSecondary;
        public InputAction @MouseSwitchWeapon => m_Wrapper.m_KeyboardInputs_MouseSwitchWeapon;
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
                @MouseFire.started -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMouseFire;
                @MouseFire.performed -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMouseFire;
                @MouseFire.canceled -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMouseFire;
                @Reload.started -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnReload;
                @Aim.started -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnAim;
                @ChangeFireMode.started -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnChangeFireMode;
                @ChangeFireMode.performed -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnChangeFireMode;
                @ChangeFireMode.canceled -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnChangeFireMode;
                @SwitchPrimaryWeapon.started -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnSwitchPrimaryWeapon;
                @SwitchPrimaryWeapon.performed -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnSwitchPrimaryWeapon;
                @SwitchPrimaryWeapon.canceled -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnSwitchPrimaryWeapon;
                @SwitchSecondary.started -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnSwitchSecondary;
                @SwitchSecondary.performed -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnSwitchSecondary;
                @SwitchSecondary.canceled -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnSwitchSecondary;
                @MouseSwitchWeapon.started -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMouseSwitchWeapon;
                @MouseSwitchWeapon.performed -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMouseSwitchWeapon;
                @MouseSwitchWeapon.canceled -= m_Wrapper.m_KeyboardInputsActionsCallbackInterface.OnMouseSwitchWeapon;
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
                @MouseFire.started += instance.OnMouseFire;
                @MouseFire.performed += instance.OnMouseFire;
                @MouseFire.canceled += instance.OnMouseFire;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @ChangeFireMode.started += instance.OnChangeFireMode;
                @ChangeFireMode.performed += instance.OnChangeFireMode;
                @ChangeFireMode.canceled += instance.OnChangeFireMode;
                @SwitchPrimaryWeapon.started += instance.OnSwitchPrimaryWeapon;
                @SwitchPrimaryWeapon.performed += instance.OnSwitchPrimaryWeapon;
                @SwitchPrimaryWeapon.canceled += instance.OnSwitchPrimaryWeapon;
                @SwitchSecondary.started += instance.OnSwitchSecondary;
                @SwitchSecondary.performed += instance.OnSwitchSecondary;
                @SwitchSecondary.canceled += instance.OnSwitchSecondary;
                @MouseSwitchWeapon.started += instance.OnMouseSwitchWeapon;
                @MouseSwitchWeapon.performed += instance.OnMouseSwitchWeapon;
                @MouseSwitchWeapon.canceled += instance.OnMouseSwitchWeapon;
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
        void OnMouseFire(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnChangeFireMode(InputAction.CallbackContext context);
        void OnSwitchPrimaryWeapon(InputAction.CallbackContext context);
        void OnSwitchSecondary(InputAction.CallbackContext context);
        void OnMouseSwitchWeapon(InputAction.CallbackContext context);
    }
}

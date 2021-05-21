// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/MobileInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MobileInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MobileInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MobileInput"",
    ""maps"": [
        {
            ""name"": ""Touch"",
            ""id"": ""71e56769-eaf3-49ff-b107-9c02ae620479"",
            ""actions"": [
                {
                    ""name"": ""Slide"",
                    ""type"": ""Value"",
                    ""id"": ""169bcf57-6198-48c3-b693-f372fc9c3864"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""PassThrough"",
                    ""id"": ""45625a37-8f15-4aa9-aeb2-e56713bdb2eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""e9fad910-f3e4-48cf-9097-39930944261e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slide"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""cf57dd4e-394a-4715-a2bb-2bde271990f2"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Android"",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a5c7c0c5-9a4e-47e8-b989-e24669281caa"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Android"",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c5fb44c2-c74d-4d90-a17d-1ef31e53814c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Android"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Android"",
            ""bindingGroup"": ""Android"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Touch
        m_Touch = asset.FindActionMap("Touch", throwIfNotFound: true);
        m_Touch_Slide = m_Touch.FindAction("Slide", throwIfNotFound: true);
        m_Touch_Jump = m_Touch.FindAction("Jump", throwIfNotFound: true);
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

    // Touch
    private readonly InputActionMap m_Touch;
    private ITouchActions m_TouchActionsCallbackInterface;
    private readonly InputAction m_Touch_Slide;
    private readonly InputAction m_Touch_Jump;
    public struct TouchActions
    {
        private @MobileInput m_Wrapper;
        public TouchActions(@MobileInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Slide => m_Wrapper.m_Touch_Slide;
        public InputAction @Jump => m_Wrapper.m_Touch_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Touch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
        public void SetCallbacks(ITouchActions instance)
        {
            if (m_Wrapper.m_TouchActionsCallbackInterface != null)
            {
                @Slide.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnSlide;
                @Slide.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnSlide;
                @Slide.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnSlide;
                @Jump.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_TouchActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Slide.started += instance.OnSlide;
                @Slide.performed += instance.OnSlide;
                @Slide.canceled += instance.OnSlide;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public TouchActions @Touch => new TouchActions(this);
    private int m_AndroidSchemeIndex = -1;
    public InputControlScheme AndroidScheme
    {
        get
        {
            if (m_AndroidSchemeIndex == -1) m_AndroidSchemeIndex = asset.FindControlSchemeIndex("Android");
            return asset.controlSchemes[m_AndroidSchemeIndex];
        }
    }
    public interface ITouchActions
    {
        void OnSlide(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}

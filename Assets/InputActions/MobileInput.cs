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
            ""name"": ""Game"",
            ""id"": ""71e56769-eaf3-49ff-b107-9c02ae620479"",
            ""actions"": [
                {
                    ""name"": ""Slide"",
                    ""type"": ""Button"",
                    ""id"": ""169bcf57-6198-48c3-b693-f372fc9c3864"",
                    ""expectedControlType"": ""Button"",
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
                },
                {
                    ""name"": ""ShowHelp"",
                    ""type"": ""Button"",
                    ""id"": ""189b01d0-bbf9-4cbc-95eb-1c01fad1eb31"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""e1b710da-88bf-4779-b963-5dc040cbd134"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""088c2a11-4d72-4f10-849a-1ef2a041cb4e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ee24e02-5749-4c6c-a754-2ac253bbfc40"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad374095-574f-43a4-8c38-af33a6ed8607"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
                },
                {
                    ""name"": """",
                    ""id"": ""3084a3ca-03e1-4fff-9a3d-30a08911cb01"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4e41c48-b531-47af-bce8-6eb053c1c16e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fad6512a-fe17-4747-acb0-73609d742627"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff61b21e-3901-4b46-9b1e-c8983052f1e6"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowHelp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a34bbced-40c7-4424-b3a1-2a67df1b41a0"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowHelp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7ef5ce1-8ff5-473e-a491-745006ef0bb6"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38f37e6e-3c24-4aba-9906-3cf60a8d5aa5"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Paused"",
            ""id"": ""3f62ee0b-b6da-4a7a-b738-7207aa26fd05"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""64f29079-2fe4-4449-93c1-c3be37a37756"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6e745bb8-ff9e-4c37-823b-0075d0c508da"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
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
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_Slide = m_Game.FindAction("Slide", throwIfNotFound: true);
        m_Game_Jump = m_Game.FindAction("Jump", throwIfNotFound: true);
        m_Game_ShowHelp = m_Game.FindAction("ShowHelp", throwIfNotFound: true);
        m_Game_PauseGame = m_Game.FindAction("PauseGame", throwIfNotFound: true);
        // Paused
        m_Paused = asset.FindActionMap("Paused", throwIfNotFound: true);
        m_Paused_Newaction = m_Paused.FindAction("New action", throwIfNotFound: true);
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

    // Game
    private readonly InputActionMap m_Game;
    private IGameActions m_GameActionsCallbackInterface;
    private readonly InputAction m_Game_Slide;
    private readonly InputAction m_Game_Jump;
    private readonly InputAction m_Game_ShowHelp;
    private readonly InputAction m_Game_PauseGame;
    public struct GameActions
    {
        private @MobileInput m_Wrapper;
        public GameActions(@MobileInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Slide => m_Wrapper.m_Game_Slide;
        public InputAction @Jump => m_Wrapper.m_Game_Jump;
        public InputAction @ShowHelp => m_Wrapper.m_Game_ShowHelp;
        public InputAction @PauseGame => m_Wrapper.m_Game_PauseGame;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void SetCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterface != null)
            {
                @Slide.started -= m_Wrapper.m_GameActionsCallbackInterface.OnSlide;
                @Slide.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnSlide;
                @Slide.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnSlide;
                @Jump.started -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                @ShowHelp.started -= m_Wrapper.m_GameActionsCallbackInterface.OnShowHelp;
                @ShowHelp.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnShowHelp;
                @ShowHelp.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnShowHelp;
                @PauseGame.started -= m_Wrapper.m_GameActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnPauseGame;
            }
            m_Wrapper.m_GameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Slide.started += instance.OnSlide;
                @Slide.performed += instance.OnSlide;
                @Slide.canceled += instance.OnSlide;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @ShowHelp.started += instance.OnShowHelp;
                @ShowHelp.performed += instance.OnShowHelp;
                @ShowHelp.canceled += instance.OnShowHelp;
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
            }
        }
    }
    public GameActions @Game => new GameActions(this);

    // Paused
    private readonly InputActionMap m_Paused;
    private IPausedActions m_PausedActionsCallbackInterface;
    private readonly InputAction m_Paused_Newaction;
    public struct PausedActions
    {
        private @MobileInput m_Wrapper;
        public PausedActions(@MobileInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_Paused_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_Paused; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PausedActions set) { return set.Get(); }
        public void SetCallbacks(IPausedActions instance)
        {
            if (m_Wrapper.m_PausedActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_PausedActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_PausedActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_PausedActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_PausedActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public PausedActions @Paused => new PausedActions(this);
    private int m_AndroidSchemeIndex = -1;
    public InputControlScheme AndroidScheme
    {
        get
        {
            if (m_AndroidSchemeIndex == -1) m_AndroidSchemeIndex = asset.FindControlSchemeIndex("Android");
            return asset.controlSchemes[m_AndroidSchemeIndex];
        }
    }
    public interface IGameActions
    {
        void OnSlide(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnShowHelp(InputAction.CallbackContext context);
        void OnPauseGame(InputAction.CallbackContext context);
    }
    public interface IPausedActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}

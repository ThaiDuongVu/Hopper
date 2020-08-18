// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/InputManager.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputManager : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputManager()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputManager"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""85a6fda7-cb58-4385-972e-09b8bce6ec09"",
            ""actions"": [
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""5bf7c586-7e15-4545-901a-415145cd31fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fe5326f3-b6f9-4769-9b8e-481d45b07d50"",
                    ""path"": ""<Pointer>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Letter"",
            ""id"": ""3dc31639-ffa4-464c-85f6-25a79dea104c"",
            ""actions"": [
                {
                    ""name"": ""ChangeDirection"",
                    ""type"": ""Button"",
                    ""id"": ""5a4fa520-1fca-45bf-9f70-97d2028a5058"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""65185f60-94a8-44f0-8589-73d345d723c6"",
                    ""path"": ""<Pointer>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""ChangeDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_Start = m_Game.FindAction("Start", throwIfNotFound: true);
        // Letter
        m_Letter = asset.FindActionMap("Letter", throwIfNotFound: true);
        m_Letter_ChangeDirection = m_Letter.FindAction("ChangeDirection", throwIfNotFound: true);
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
    private readonly InputAction m_Game_Start;
    public struct GameActions
    {
        private @InputManager m_Wrapper;
        public GameActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Start => m_Wrapper.m_Game_Start;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void SetCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterface != null)
            {
                @Start.started -= m_Wrapper.m_GameActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnStart;
            }
            m_Wrapper.m_GameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
            }
        }
    }
    public GameActions @Game => new GameActions(this);

    // Letter
    private readonly InputActionMap m_Letter;
    private ILetterActions m_LetterActionsCallbackInterface;
    private readonly InputAction m_Letter_ChangeDirection;
    public struct LetterActions
    {
        private @InputManager m_Wrapper;
        public LetterActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @ChangeDirection => m_Wrapper.m_Letter_ChangeDirection;
        public InputActionMap Get() { return m_Wrapper.m_Letter; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LetterActions set) { return set.Get(); }
        public void SetCallbacks(ILetterActions instance)
        {
            if (m_Wrapper.m_LetterActionsCallbackInterface != null)
            {
                @ChangeDirection.started -= m_Wrapper.m_LetterActionsCallbackInterface.OnChangeDirection;
                @ChangeDirection.performed -= m_Wrapper.m_LetterActionsCallbackInterface.OnChangeDirection;
                @ChangeDirection.canceled -= m_Wrapper.m_LetterActionsCallbackInterface.OnChangeDirection;
            }
            m_Wrapper.m_LetterActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ChangeDirection.started += instance.OnChangeDirection;
                @ChangeDirection.performed += instance.OnChangeDirection;
                @ChangeDirection.canceled += instance.OnChangeDirection;
            }
        }
    }
    public LetterActions @Letter => new LetterActions(this);
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    public interface IGameActions
    {
        void OnStart(InputAction.CallbackContext context);
    }
    public interface ILetterActions
    {
        void OnChangeDirection(InputAction.CallbackContext context);
    }
}

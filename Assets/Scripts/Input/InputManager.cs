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
            ""name"": ""Platform"",
            ""id"": ""c63d7d1c-482a-4745-81d6-f0fe0a2ef0f4"",
            ""actions"": [
                {
                    ""name"": ""Grow"",
                    ""type"": ""Button"",
                    ""id"": ""6c36d7d6-a8ca-485a-a676-29f66a7ee1d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c0341347-69dd-42a8-89ff-f212f9065469"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Grow"",
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
        }
    ]
}");
        // Platform
        m_Platform = asset.FindActionMap("Platform", throwIfNotFound: true);
        m_Platform_Grow = m_Platform.FindAction("Grow", throwIfNotFound: true);
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

    // Platform
    private readonly InputActionMap m_Platform;
    private IPlatformActions m_PlatformActionsCallbackInterface;
    private readonly InputAction m_Platform_Grow;
    public struct PlatformActions
    {
        private @InputManager m_Wrapper;
        public PlatformActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Grow => m_Wrapper.m_Platform_Grow;
        public InputActionMap Get() { return m_Wrapper.m_Platform; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlatformActions set) { return set.Get(); }
        public void SetCallbacks(IPlatformActions instance)
        {
            if (m_Wrapper.m_PlatformActionsCallbackInterface != null)
            {
                @Grow.started -= m_Wrapper.m_PlatformActionsCallbackInterface.OnGrow;
                @Grow.performed -= m_Wrapper.m_PlatformActionsCallbackInterface.OnGrow;
                @Grow.canceled -= m_Wrapper.m_PlatformActionsCallbackInterface.OnGrow;
            }
            m_Wrapper.m_PlatformActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Grow.started += instance.OnGrow;
                @Grow.performed += instance.OnGrow;
                @Grow.canceled += instance.OnGrow;
            }
        }
    }
    public PlatformActions @Platform => new PlatformActions(this);
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    public interface IPlatformActions
    {
        void OnGrow(InputAction.CallbackContext context);
    }
}

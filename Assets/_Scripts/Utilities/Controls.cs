// GENERATED AUTOMATICALLY FROM 'Assets/_Scripts/Utilities/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Mapping"",
            ""id"": ""41084352-bcdd-4027-a937-11d60b114bd8"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""50694c83-012b-4a7e-afe5-afebf6bdf8a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""551be09b-b30c-4a5a-8d00-2bf3ab781040"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GoBack"",
                    ""type"": ""Button"",
                    ""id"": ""3c6ae52b-e32c-4b83-b2d9-1a48a72a9baf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""659a240a-631c-499c-b8c2-9bbc552e77f2"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9507a5a5-bf52-42bf-b580-aa7603ff918c"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94fb278a-7ba1-4e05-913f-320113a39742"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GoBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Mapping
        m_Mapping = asset.FindActionMap("Mapping", throwIfNotFound: true);
        m_Mapping_Pause = m_Mapping.FindAction("Pause", throwIfNotFound: true);
        m_Mapping_Confirm = m_Mapping.FindAction("Confirm", throwIfNotFound: true);
        m_Mapping_GoBack = m_Mapping.FindAction("GoBack", throwIfNotFound: true);
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

    // Mapping
    private readonly InputActionMap m_Mapping;
    private IMappingActions m_MappingActionsCallbackInterface;
    private readonly InputAction m_Mapping_Pause;
    private readonly InputAction m_Mapping_Confirm;
    private readonly InputAction m_Mapping_GoBack;
    public struct MappingActions
    {
        private @Controls m_Wrapper;
        public MappingActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Mapping_Pause;
        public InputAction @Confirm => m_Wrapper.m_Mapping_Confirm;
        public InputAction @GoBack => m_Wrapper.m_Mapping_GoBack;
        public InputActionMap Get() { return m_Wrapper.m_Mapping; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MappingActions set) { return set.Get(); }
        public void SetCallbacks(IMappingActions instance)
        {
            if (m_Wrapper.m_MappingActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_MappingActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MappingActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MappingActionsCallbackInterface.OnPause;
                @Confirm.started -= m_Wrapper.m_MappingActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_MappingActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_MappingActionsCallbackInterface.OnConfirm;
                @GoBack.started -= m_Wrapper.m_MappingActionsCallbackInterface.OnGoBack;
                @GoBack.performed -= m_Wrapper.m_MappingActionsCallbackInterface.OnGoBack;
                @GoBack.canceled -= m_Wrapper.m_MappingActionsCallbackInterface.OnGoBack;
            }
            m_Wrapper.m_MappingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
                @GoBack.started += instance.OnGoBack;
                @GoBack.performed += instance.OnGoBack;
                @GoBack.canceled += instance.OnGoBack;
            }
        }
    }
    public MappingActions @Mapping => new MappingActions(this);
    public interface IMappingActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnConfirm(InputAction.CallbackContext context);
        void OnGoBack(InputAction.CallbackContext context);
    }
}

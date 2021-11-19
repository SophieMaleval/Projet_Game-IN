// GENERATED AUTOMATICALLY FROM 'Assets/Script/Player/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerActionControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActionControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerInLand"",
            ""id"": ""6a5e9aaf-1ff8-46fd-b71f-7e945e100ef1"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""05a8f0cf-6c7c-43a3-bb93-cfbbed2be43a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2062a430-8cce-47a9-8bdf-cbafe8451567"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Arrow Movement"",
                    ""id"": ""92bceaa5-2eaf-449c-b5d3-f4a627658be4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""26fecb69-f17e-4b3f-bbb9-b26ae045ff69"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""b3f41fec-4b50-4ec9-b08b-ab13c69d7feb"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""b4ca3cfc-0d60-46c5-8d13-7433bf9786ef"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""e8f44e34-13bd-45fa-8401-95c444d39cb3"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ZQSD Movement"",
                    ""id"": ""e36ea280-6af1-45e7-b0a2-7b1f4a3a94f8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""22edaf5b-6138-401b-bf3f-e2ba6d4c0c42"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""91b06036-7748-4579-9979-903375aeac17"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""31ab6f0b-5677-44b9-9de5-ddd32d6d8e06"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""5c94a7ec-db38-49b8-a4e1-523a0d6b6241"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""70a68799-6341-47b2-998d-8176d61f6bbd"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""660c879b-cc45-4a28-8c58-b75817e497a9"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInLand
        m_PlayerInLand = asset.FindActionMap("PlayerInLand", throwIfNotFound: true);
        m_PlayerInLand_Move = m_PlayerInLand.FindAction("Move", throwIfNotFound: true);
        m_PlayerInLand_Interact = m_PlayerInLand.FindAction("Interact", throwIfNotFound: true);
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

    // PlayerInLand
    private readonly InputActionMap m_PlayerInLand;
    private IPlayerInLandActions m_PlayerInLandActionsCallbackInterface;
    private readonly InputAction m_PlayerInLand_Move;
    private readonly InputAction m_PlayerInLand_Interact;
    public struct PlayerInLandActions
    {
        private @PlayerActionControls m_Wrapper;
        public PlayerInLandActions(@PlayerActionControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerInLand_Move;
        public InputAction @Interact => m_Wrapper.m_PlayerInLand_Interact;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInLand; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInLandActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInLandActions instance)
        {
            if (m_Wrapper.m_PlayerInLandActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerInLandActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerInLandActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerInLandActionsCallbackInterface.OnMove;
                @Interact.started -= m_Wrapper.m_PlayerInLandActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerInLandActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerInLandActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_PlayerInLandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public PlayerInLandActions @PlayerInLand => new PlayerInLandActions(this);
    public interface IPlayerInLandActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
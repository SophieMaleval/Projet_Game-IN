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
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a1061594-9a4e-4316-bc32-f10a15c667e1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""EnterScoot"",
                    ""type"": ""PassThrough"",
                    ""id"": ""be329a52-9498-462b-afa7-8663d4dd658a"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""bc44e985-ab23-48c2-a580-5910509d939a"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""903b834d-bc49-40d5-8931-1b26993ef307"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0edcd3aa-f678-49d2-8c21-33a4db41a254"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnterScoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5fe7beb6-959f-4c74-8453-3a7dfe08638a"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnterScoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerInScoot"",
            ""id"": ""c0f67742-586d-42d1-8f5b-feb1d3d494e2"",
            ""actions"": [
                {
                    ""name"": ""MoveScoot"",
                    ""type"": ""Value"",
                    ""id"": ""cda207b8-0716-4f04-9341-6e481769620b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5d725e0f-4876-4dc7-aa9b-15c45be588f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ExitScoot"",
                    ""type"": ""PassThrough"",
                    ""id"": ""22a5776a-40dc-4401-aab1-1dc82c2c6459"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""569f1eea-738b-43af-80aa-f4744a1d8871"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16a2c1a5-5104-41dc-9aa9-c895af998d7e"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Arrow Movement"",
                    ""id"": ""676f72a6-eb5b-483b-b9d8-5463ca28e282"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveScoot"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""91906650-aac9-4e86-9092-9431ca62db77"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveScoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""2098f18f-d9e0-4950-990f-17ccd148ebe2"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveScoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""095818f7-eaf9-4a6e-8c02-30c46a837c55"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveScoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""230aa871-ee9b-4097-a094-8fb4f5fede14"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveScoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ZQSD Movement"",
                    ""id"": ""4289e848-ca68-4bad-a8ee-1133b1453f2b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveScoot"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""b5538a83-b6da-46c8-8d33-c54a59567d1a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveScoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""d891ce95-eee1-49e4-8529-9560e715d65a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveScoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""7b51f758-8614-4d40-9d13-9efb2268c1c8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveScoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""3e66a239-fde5-489f-a24f-8b4f8e466a3e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveScoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b908c86c-b3d7-4968-a961-1a8de596c781"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitScoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""469505b4-890c-482e-b47f-1303b8f3efbb"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitScoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerInDialogue"",
            ""id"": ""4faf5640-0082-4b47-a126-1dba7a87b5ab"",
            ""actions"": [
                {
                    ""name"": ""SelectPreviousQuestion"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6a146fd9-c00f-4c19-82bb-5858c5fbc87a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""SelectNextQuestion"",
                    ""type"": ""PassThrough"",
                    ""id"": ""14ccbef2-2643-44de-86c6-82734d46f1da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ValidateChoice"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e11225cc-0e73-4ff5-95c2-c3c9409aa5ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""aec3e49c-64d0-4f1e-9d11-4a75bfb5f82c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectPreviousQuestion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3858ee51-9a54-45fa-b4e8-5a2ea80ff479"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectPreviousQuestion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08fd5c2b-a387-461d-8879-b63307cf4a77"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectNextQuestion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9cbd5ab-0ab7-4c0f-905d-0c60ca2b4637"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectNextQuestion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76106e1a-ad2d-4ee5-b105-27e322f8fa55"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ValidateChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af06a12b-2d24-444b-a2f3-8144d3407bea"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ValidateChoice"",
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
        m_PlayerInLand_Inventory = m_PlayerInLand.FindAction("Inventory", throwIfNotFound: true);
        m_PlayerInLand_EnterScoot = m_PlayerInLand.FindAction("EnterScoot", throwIfNotFound: true);
        // PlayerInScoot
        m_PlayerInScoot = asset.FindActionMap("PlayerInScoot", throwIfNotFound: true);
        m_PlayerInScoot_MoveScoot = m_PlayerInScoot.FindAction("MoveScoot", throwIfNotFound: true);
        m_PlayerInScoot_Inventory = m_PlayerInScoot.FindAction("Inventory", throwIfNotFound: true);
        m_PlayerInScoot_ExitScoot = m_PlayerInScoot.FindAction("ExitScoot", throwIfNotFound: true);
        // PlayerInDialogue
        m_PlayerInDialogue = asset.FindActionMap("PlayerInDialogue", throwIfNotFound: true);
        m_PlayerInDialogue_SelectPreviousQuestion = m_PlayerInDialogue.FindAction("SelectPreviousQuestion", throwIfNotFound: true);
        m_PlayerInDialogue_SelectNextQuestion = m_PlayerInDialogue.FindAction("SelectNextQuestion", throwIfNotFound: true);
        m_PlayerInDialogue_ValidateChoice = m_PlayerInDialogue.FindAction("ValidateChoice", throwIfNotFound: true);
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
    private readonly InputAction m_PlayerInLand_Inventory;
    private readonly InputAction m_PlayerInLand_EnterScoot;
    public struct PlayerInLandActions
    {
        private @PlayerActionControls m_Wrapper;
        public PlayerInLandActions(@PlayerActionControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerInLand_Move;
        public InputAction @Interact => m_Wrapper.m_PlayerInLand_Interact;
        public InputAction @Inventory => m_Wrapper.m_PlayerInLand_Inventory;
        public InputAction @EnterScoot => m_Wrapper.m_PlayerInLand_EnterScoot;
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
                @Inventory.started -= m_Wrapper.m_PlayerInLandActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_PlayerInLandActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_PlayerInLandActionsCallbackInterface.OnInventory;
                @EnterScoot.started -= m_Wrapper.m_PlayerInLandActionsCallbackInterface.OnEnterScoot;
                @EnterScoot.performed -= m_Wrapper.m_PlayerInLandActionsCallbackInterface.OnEnterScoot;
                @EnterScoot.canceled -= m_Wrapper.m_PlayerInLandActionsCallbackInterface.OnEnterScoot;
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
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @EnterScoot.started += instance.OnEnterScoot;
                @EnterScoot.performed += instance.OnEnterScoot;
                @EnterScoot.canceled += instance.OnEnterScoot;
            }
        }
    }
    public PlayerInLandActions @PlayerInLand => new PlayerInLandActions(this);

    // PlayerInScoot
    private readonly InputActionMap m_PlayerInScoot;
    private IPlayerInScootActions m_PlayerInScootActionsCallbackInterface;
    private readonly InputAction m_PlayerInScoot_MoveScoot;
    private readonly InputAction m_PlayerInScoot_Inventory;
    private readonly InputAction m_PlayerInScoot_ExitScoot;
    public struct PlayerInScootActions
    {
        private @PlayerActionControls m_Wrapper;
        public PlayerInScootActions(@PlayerActionControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveScoot => m_Wrapper.m_PlayerInScoot_MoveScoot;
        public InputAction @Inventory => m_Wrapper.m_PlayerInScoot_Inventory;
        public InputAction @ExitScoot => m_Wrapper.m_PlayerInScoot_ExitScoot;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInScoot; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInScootActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInScootActions instance)
        {
            if (m_Wrapper.m_PlayerInScootActionsCallbackInterface != null)
            {
                @MoveScoot.started -= m_Wrapper.m_PlayerInScootActionsCallbackInterface.OnMoveScoot;
                @MoveScoot.performed -= m_Wrapper.m_PlayerInScootActionsCallbackInterface.OnMoveScoot;
                @MoveScoot.canceled -= m_Wrapper.m_PlayerInScootActionsCallbackInterface.OnMoveScoot;
                @Inventory.started -= m_Wrapper.m_PlayerInScootActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_PlayerInScootActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_PlayerInScootActionsCallbackInterface.OnInventory;
                @ExitScoot.started -= m_Wrapper.m_PlayerInScootActionsCallbackInterface.OnExitScoot;
                @ExitScoot.performed -= m_Wrapper.m_PlayerInScootActionsCallbackInterface.OnExitScoot;
                @ExitScoot.canceled -= m_Wrapper.m_PlayerInScootActionsCallbackInterface.OnExitScoot;
            }
            m_Wrapper.m_PlayerInScootActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveScoot.started += instance.OnMoveScoot;
                @MoveScoot.performed += instance.OnMoveScoot;
                @MoveScoot.canceled += instance.OnMoveScoot;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @ExitScoot.started += instance.OnExitScoot;
                @ExitScoot.performed += instance.OnExitScoot;
                @ExitScoot.canceled += instance.OnExitScoot;
            }
        }
    }
    public PlayerInScootActions @PlayerInScoot => new PlayerInScootActions(this);

    // PlayerInDialogue
    private readonly InputActionMap m_PlayerInDialogue;
    private IPlayerInDialogueActions m_PlayerInDialogueActionsCallbackInterface;
    private readonly InputAction m_PlayerInDialogue_SelectPreviousQuestion;
    private readonly InputAction m_PlayerInDialogue_SelectNextQuestion;
    private readonly InputAction m_PlayerInDialogue_ValidateChoice;
    public struct PlayerInDialogueActions
    {
        private @PlayerActionControls m_Wrapper;
        public PlayerInDialogueActions(@PlayerActionControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SelectPreviousQuestion => m_Wrapper.m_PlayerInDialogue_SelectPreviousQuestion;
        public InputAction @SelectNextQuestion => m_Wrapper.m_PlayerInDialogue_SelectNextQuestion;
        public InputAction @ValidateChoice => m_Wrapper.m_PlayerInDialogue_ValidateChoice;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInDialogue; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInDialogueActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInDialogueActions instance)
        {
            if (m_Wrapper.m_PlayerInDialogueActionsCallbackInterface != null)
            {
                @SelectPreviousQuestion.started -= m_Wrapper.m_PlayerInDialogueActionsCallbackInterface.OnSelectPreviousQuestion;
                @SelectPreviousQuestion.performed -= m_Wrapper.m_PlayerInDialogueActionsCallbackInterface.OnSelectPreviousQuestion;
                @SelectPreviousQuestion.canceled -= m_Wrapper.m_PlayerInDialogueActionsCallbackInterface.OnSelectPreviousQuestion;
                @SelectNextQuestion.started -= m_Wrapper.m_PlayerInDialogueActionsCallbackInterface.OnSelectNextQuestion;
                @SelectNextQuestion.performed -= m_Wrapper.m_PlayerInDialogueActionsCallbackInterface.OnSelectNextQuestion;
                @SelectNextQuestion.canceled -= m_Wrapper.m_PlayerInDialogueActionsCallbackInterface.OnSelectNextQuestion;
                @ValidateChoice.started -= m_Wrapper.m_PlayerInDialogueActionsCallbackInterface.OnValidateChoice;
                @ValidateChoice.performed -= m_Wrapper.m_PlayerInDialogueActionsCallbackInterface.OnValidateChoice;
                @ValidateChoice.canceled -= m_Wrapper.m_PlayerInDialogueActionsCallbackInterface.OnValidateChoice;
            }
            m_Wrapper.m_PlayerInDialogueActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SelectPreviousQuestion.started += instance.OnSelectPreviousQuestion;
                @SelectPreviousQuestion.performed += instance.OnSelectPreviousQuestion;
                @SelectPreviousQuestion.canceled += instance.OnSelectPreviousQuestion;
                @SelectNextQuestion.started += instance.OnSelectNextQuestion;
                @SelectNextQuestion.performed += instance.OnSelectNextQuestion;
                @SelectNextQuestion.canceled += instance.OnSelectNextQuestion;
                @ValidateChoice.started += instance.OnValidateChoice;
                @ValidateChoice.performed += instance.OnValidateChoice;
                @ValidateChoice.canceled += instance.OnValidateChoice;
            }
        }
    }
    public PlayerInDialogueActions @PlayerInDialogue => new PlayerInDialogueActions(this);
    public interface IPlayerInLandActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnEnterScoot(InputAction.CallbackContext context);
    }
    public interface IPlayerInScootActions
    {
        void OnMoveScoot(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnExitScoot(InputAction.CallbackContext context);
    }
    public interface IPlayerInDialogueActions
    {
        void OnSelectPreviousQuestion(InputAction.CallbackContext context);
        void OnSelectNextQuestion(InputAction.CallbackContext context);
        void OnValidateChoice(InputAction.CallbackContext context);
    }
}

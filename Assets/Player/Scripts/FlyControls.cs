// GENERATED AUTOMATICALLY FROM 'Assets/Player/Scripts/FlyControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace PlayerInputControls
{
    public class @FlyControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @FlyControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""FlyControls"",
    ""maps"": [
        {
            ""name"": ""FlyModel"",
            ""id"": ""fe97e9a9-7e68-4479-be7f-b733c928597c"",
            ""actions"": [
                {
                    ""name"": ""FirstItemFunction"",
                    ""type"": ""Button"",
                    ""id"": ""e0a49c30-8c7f-4f28-a069-1b6992913c82"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveToPoint"",
                    ""type"": ""Button"",
                    ""id"": ""f41eb743-f792-4975-9f72-acf421fef297"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondtemFunction"",
                    ""type"": ""Button"",
                    ""id"": ""2afa2bd6-d795-4078-b2eb-dcaab6bc956b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""f226a251-7612-46f9-9164-957db6265c00"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookRotation"",
                    ""type"": ""Value"",
                    ""id"": ""13923fc0-0830-42c9-9976-a678ca686855"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeAltitude"",
                    ""type"": ""Value"",
                    ""id"": ""ef1addd1-2046-4a16-a4d9-aa3c98cbe767"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e0172ed8-fd09-44ff-9f39-fb23bb4c8d48"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstItemFunction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94657346-adeb-4f28-bf12-f13d6ecbbd29"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondtemFunction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""17d7f5b7-afab-4d5b-8a0c-0391d696f163"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""03b17156-7c7f-45ae-acf5-cdc8714a34b3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cbb8019a-d23d-4dc1-8d5d-cf54d421046b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""910c7ec9-6526-4c39-850b-222899a5cf28"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""16fc116d-4456-47e5-8575-c55524884e18"",
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
                    ""id"": ""4bca703a-b9f7-4071-a92f-5f7441e90fbe"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""2750746b-01e6-43e8-90f3-4caf711df307"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeAltitude"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0dec5c66-baf9-416b-a5f4-bee3b41172c7"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeAltitude"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""65e4884e-f00c-4388-baf4-cae9e4ba5cf0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeAltitude"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7917a134-d6e8-453f-ba0c-18a3f6a67a1b"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveToPoint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // FlyModel
            m_FlyModel = asset.FindActionMap("FlyModel", throwIfNotFound: true);
            m_FlyModel_FirstItemFunction = m_FlyModel.FindAction("FirstItemFunction", throwIfNotFound: true);
            m_FlyModel_MoveToPoint = m_FlyModel.FindAction("MoveToPoint", throwIfNotFound: true);
            m_FlyModel_SecondtemFunction = m_FlyModel.FindAction("SecondtemFunction", throwIfNotFound: true);
            m_FlyModel_Move = m_FlyModel.FindAction("Move", throwIfNotFound: true);
            m_FlyModel_LookRotation = m_FlyModel.FindAction("LookRotation", throwIfNotFound: true);
            m_FlyModel_ChangeAltitude = m_FlyModel.FindAction("ChangeAltitude", throwIfNotFound: true);
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

        // FlyModel
        private readonly InputActionMap m_FlyModel;
        private IFlyModelActions m_FlyModelActionsCallbackInterface;
        private readonly InputAction m_FlyModel_FirstItemFunction;
        private readonly InputAction m_FlyModel_MoveToPoint;
        private readonly InputAction m_FlyModel_SecondtemFunction;
        private readonly InputAction m_FlyModel_Move;
        private readonly InputAction m_FlyModel_LookRotation;
        private readonly InputAction m_FlyModel_ChangeAltitude;
        public struct FlyModelActions
        {
            private @FlyControls m_Wrapper;
            public FlyModelActions(@FlyControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @FirstItemFunction => m_Wrapper.m_FlyModel_FirstItemFunction;
            public InputAction @MoveToPoint => m_Wrapper.m_FlyModel_MoveToPoint;
            public InputAction @SecondtemFunction => m_Wrapper.m_FlyModel_SecondtemFunction;
            public InputAction @Move => m_Wrapper.m_FlyModel_Move;
            public InputAction @LookRotation => m_Wrapper.m_FlyModel_LookRotation;
            public InputAction @ChangeAltitude => m_Wrapper.m_FlyModel_ChangeAltitude;
            public InputActionMap Get() { return m_Wrapper.m_FlyModel; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(FlyModelActions set) { return set.Get(); }
            public void SetCallbacks(IFlyModelActions instance)
            {
                if (m_Wrapper.m_FlyModelActionsCallbackInterface != null)
                {
                    @FirstItemFunction.started -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnFirstItemFunction;
                    @FirstItemFunction.performed -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnFirstItemFunction;
                    @FirstItemFunction.canceled -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnFirstItemFunction;
                    @MoveToPoint.started -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnMoveToPoint;
                    @MoveToPoint.performed -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnMoveToPoint;
                    @MoveToPoint.canceled -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnMoveToPoint;
                    @SecondtemFunction.started -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnSecondtemFunction;
                    @SecondtemFunction.performed -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnSecondtemFunction;
                    @SecondtemFunction.canceled -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnSecondtemFunction;
                    @Move.started -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnMove;
                    @LookRotation.started -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnLookRotation;
                    @LookRotation.performed -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnLookRotation;
                    @LookRotation.canceled -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnLookRotation;
                    @ChangeAltitude.started -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnChangeAltitude;
                    @ChangeAltitude.performed -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnChangeAltitude;
                    @ChangeAltitude.canceled -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnChangeAltitude;
                }
                m_Wrapper.m_FlyModelActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @FirstItemFunction.started += instance.OnFirstItemFunction;
                    @FirstItemFunction.performed += instance.OnFirstItemFunction;
                    @FirstItemFunction.canceled += instance.OnFirstItemFunction;
                    @MoveToPoint.started += instance.OnMoveToPoint;
                    @MoveToPoint.performed += instance.OnMoveToPoint;
                    @MoveToPoint.canceled += instance.OnMoveToPoint;
                    @SecondtemFunction.started += instance.OnSecondtemFunction;
                    @SecondtemFunction.performed += instance.OnSecondtemFunction;
                    @SecondtemFunction.canceled += instance.OnSecondtemFunction;
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @LookRotation.started += instance.OnLookRotation;
                    @LookRotation.performed += instance.OnLookRotation;
                    @LookRotation.canceled += instance.OnLookRotation;
                    @ChangeAltitude.started += instance.OnChangeAltitude;
                    @ChangeAltitude.performed += instance.OnChangeAltitude;
                    @ChangeAltitude.canceled += instance.OnChangeAltitude;
                }
            }
        }
        public FlyModelActions @FlyModel => new FlyModelActions(this);
        public interface IFlyModelActions
        {
            void OnFirstItemFunction(InputAction.CallbackContext context);
            void OnMoveToPoint(InputAction.CallbackContext context);
            void OnSecondtemFunction(InputAction.CallbackContext context);
            void OnMove(InputAction.CallbackContext context);
            void OnLookRotation(InputAction.CallbackContext context);
            void OnChangeAltitude(InputAction.CallbackContext context);
        }
    }
}

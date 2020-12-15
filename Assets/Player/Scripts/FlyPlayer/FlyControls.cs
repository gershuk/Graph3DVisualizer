// This file is part of Grpah3DVisualizer.
// Copyright © Gershuk Vladislav 2020.
//
// Grpah3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Grpah3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Grpah3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

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
        public @FlyControls ()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""FlyControls"",
    ""maps"": [
        {
            ""name"": ""FlyModel"",
            ""id"": ""fe97e9a9-7e68-4479-be7f-b733c928597c"",
            ""actions"": [
                {
                    ""name"": ""MoveToPoint"",
                    ""type"": ""Button"",
                    ""id"": ""f41eb743-f792-4975-9f72-acf421fef297"",
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
                },
                {
                    ""name"": ""ScrollItemList"",
                    ""type"": ""Value"",
                    ""id"": ""9cc439e9-4a1d-4eb1-9bc8-3ce18dbae4cd"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectItem"",
                    ""type"": ""Button"",
                    ""id"": ""4b3ee49a-5ad7-4301-856f-f1a5e4a6f701"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
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
                },
                {
                    ""name"": """",
                    ""id"": ""bf532e83-8400-4e06-9018-4e85431dc937"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollItemList"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8c74da1-fe3a-45b9-a5a7-d9289b668f1d"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03035fa0-0a83-45ff-8185-07d2e041434f"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0fa8b02-5532-4fb5-b465-9a8f078b154e"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6b34e39-f557-45fc-9832-2063fe3103ca"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91b2edd0-29bb-4ebd-b238-b4d37eecff1c"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aedafc96-238d-4795-9ad0-d9eb00513846"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae4a336e-e3c7-4215-97b7-e84c8d1f5f36"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea9d7d44-7e4c-4518-b48d-6024ae079e2d"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""538f3aeb-ab58-49c9-bdfa-f951a1ea9e25"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectItem"",
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
            m_FlyModel_MoveToPoint = m_FlyModel.FindAction("MoveToPoint", throwIfNotFound: true);
            m_FlyModel_Move = m_FlyModel.FindAction("Move", throwIfNotFound: true);
            m_FlyModel_LookRotation = m_FlyModel.FindAction("LookRotation", throwIfNotFound: true);
            m_FlyModel_ChangeAltitude = m_FlyModel.FindAction("ChangeAltitude", throwIfNotFound: true);
            m_FlyModel_ScrollItemList = m_FlyModel.FindAction("ScrollItemList", throwIfNotFound: true);
            m_FlyModel_SelectItem = m_FlyModel.FindAction("SelectItem", throwIfNotFound: true);
        }

        public void Dispose () => UnityEngine.Object.Destroy(asset);

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

        public bool Contains (InputAction action) => asset.Contains(action);

        public IEnumerator<InputAction> GetEnumerator () => asset.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator () => GetEnumerator();

        public void Enable () => asset.Enable();

        public void Disable () => asset.Disable();

        // FlyModel
        private readonly InputActionMap m_FlyModel;
        private IFlyModelActions m_FlyModelActionsCallbackInterface;
        private readonly InputAction m_FlyModel_MoveToPoint;
        private readonly InputAction m_FlyModel_Move;
        private readonly InputAction m_FlyModel_LookRotation;
        private readonly InputAction m_FlyModel_ChangeAltitude;
        private readonly InputAction m_FlyModel_ScrollItemList;
        private readonly InputAction m_FlyModel_SelectItem;
        public struct FlyModelActions
        {
            private readonly @FlyControls m_Wrapper;
            public FlyModelActions (@FlyControls wrapper) => m_Wrapper = wrapper;
            public InputAction @MoveToPoint => m_Wrapper.m_FlyModel_MoveToPoint;
            public InputAction @Move => m_Wrapper.m_FlyModel_Move;
            public InputAction @LookRotation => m_Wrapper.m_FlyModel_LookRotation;
            public InputAction @ChangeAltitude => m_Wrapper.m_FlyModel_ChangeAltitude;
            public InputAction @ScrollItemList => m_Wrapper.m_FlyModel_ScrollItemList;
            public InputAction @SelectItem => m_Wrapper.m_FlyModel_SelectItem;
            public InputActionMap Get () => m_Wrapper.m_FlyModel;
            public void Enable () => Get().Enable();
            public void Disable () => Get().Disable();
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap (FlyModelActions set) => set.Get();
            public void SetCallbacks (IFlyModelActions instance)
            {
                if (m_Wrapper.m_FlyModelActionsCallbackInterface != null)
                {
                    @MoveToPoint.started -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnMoveToPoint;
                    @MoveToPoint.performed -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnMoveToPoint;
                    @MoveToPoint.canceled -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnMoveToPoint;
                    @Move.started -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnMove;
                    @LookRotation.started -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnLookRotation;
                    @LookRotation.performed -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnLookRotation;
                    @LookRotation.canceled -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnLookRotation;
                    @ChangeAltitude.started -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnChangeAltitude;
                    @ChangeAltitude.performed -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnChangeAltitude;
                    @ChangeAltitude.canceled -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnChangeAltitude;
                    @ScrollItemList.started -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnScrollItemList;
                    @ScrollItemList.performed -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnScrollItemList;
                    @ScrollItemList.canceled -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnScrollItemList;
                    @SelectItem.started -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnSelectItem;
                    @SelectItem.performed -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnSelectItem;
                    @SelectItem.canceled -= m_Wrapper.m_FlyModelActionsCallbackInterface.OnSelectItem;
                }
                m_Wrapper.m_FlyModelActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MoveToPoint.started += instance.OnMoveToPoint;
                    @MoveToPoint.performed += instance.OnMoveToPoint;
                    @MoveToPoint.canceled += instance.OnMoveToPoint;
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @LookRotation.started += instance.OnLookRotation;
                    @LookRotation.performed += instance.OnLookRotation;
                    @LookRotation.canceled += instance.OnLookRotation;
                    @ChangeAltitude.started += instance.OnChangeAltitude;
                    @ChangeAltitude.performed += instance.OnChangeAltitude;
                    @ChangeAltitude.canceled += instance.OnChangeAltitude;
                    @ScrollItemList.started += instance.OnScrollItemList;
                    @ScrollItemList.performed += instance.OnScrollItemList;
                    @ScrollItemList.canceled += instance.OnScrollItemList;
                    @SelectItem.started += instance.OnSelectItem;
                    @SelectItem.performed += instance.OnSelectItem;
                    @SelectItem.canceled += instance.OnSelectItem;
                }
            }
        }
        public FlyModelActions @FlyModel => new FlyModelActions(this);
        public interface IFlyModelActions
        {
            void OnMoveToPoint (InputAction.CallbackContext context);
            void OnMove (InputAction.CallbackContext context);
            void OnLookRotation (InputAction.CallbackContext context);
            void OnChangeAltitude (InputAction.CallbackContext context);
            void OnScrollItemList (InputAction.CallbackContext context);
            void OnSelectItem (InputAction.CallbackContext context);
        }
    }
}

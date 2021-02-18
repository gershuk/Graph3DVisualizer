// This file is part of Graph3DVisualizer.
// Copyright © Gershuk Vladislav 2021.
//
// Graph3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Graph3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Graph3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

using UnityEngine;
using UnityEngine.InputSystem;

using static Graph3DVisualizer.GUI.GUIFactory;

namespace Graph3DVisualizer.Scene
{
    /// <summary>
    /// Component that creates a menu for working with <see cref="SceneController"/>.
    /// </summary>
    [RequireComponent(typeof(SceneController))]
    public class Menu : MonoBehaviour
    {
        private enum State
        {
            StartMenu,
            TaskScene,
        }

        private const string _menuAction = "Show/HideMenuAction";

        [SerializeField]
        private GameObject _content;

        [SerializeField]
        private Font _font;

        private InputActionMap _inputActions;

        private bool _isActive = true;

        [SerializeField]
        private GameObject _menu;

        private SceneController _sceneControler;
        private State _state = State.StartMenu;

        private void Awake ()
        {
            _inputActions = new InputActionMap("SceneControlletActionMap");
            var selectItemAction = _inputActions.AddAction(_menuAction, InputActionType.Button, "<Keyboard>/Escape");
            selectItemAction.performed += CallChangeMenuState;
            _inputActions.Enable();
            _sceneControler = GetComponent<SceneController>();
            _sceneControler.LoadMods();
            _sceneControler.FindAllTasks();

            var taskList = _sceneControler.TaskList;

            var buttonHeight = _content.GetComponent<RectTransform>().sizeDelta.y / 8;
            var buttonWidth = _content.GetComponent<RectTransform>().sizeDelta.x;
            var i = 0;
            for (i = 0; i < taskList.Count; i++)
            {
                var task = taskList[i];
                {
                    var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
                        new Vector2(buttonWidth / 2, -buttonHeight * (i + 0.5f)));
                    var index = i;
                    var buttonParameters = new ButtonParameters(task.Name, buttonRectParameters, null,
                        () =>
                        {
                            _state = State.TaskScene;
                            ChangeMenuState(false);
                            if (_sceneControler.ActiveTask)
                                _sceneControler.StopTask();
                            _sceneControler.StartTask(index);
                        });
                    var newButton = CreateButton(buttonParameters);
                    var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
                    var textParameters = new TextParameters(task.Name, Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
                    var newText = CreateText(textParameters);
                }
            }

            {
                var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
                            new Vector2(buttonWidth / 2, -buttonHeight * (i + 0.5f)));
                var buttonParameters = new ButtonParameters("StartMenu", buttonRectParameters, null,
                    () =>
                    {
                        _state = State.StartMenu;
                        if (_sceneControler.ActiveTask)
                            _sceneControler.StopTask();
                        ChangeMenuState(true);
                    });
                var newButton = CreateButton(buttonParameters);
                var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
                var textParameters = new TextParameters("StartMenu", Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
                var newText = CreateText(textParameters);
                i++;
            }

            {
                var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
                            new Vector2(buttonWidth / 2, -buttonHeight * (i + 0.5f)));
                var buttonParameters = new ButtonParameters("Check", buttonRectParameters, null,
                    () =>
                    {
                        if (_state == State.TaskScene)
                        {
                            Debug.LogError($"{_sceneControler.ActiveTask.name} result:");
                            foreach (var verdict in _sceneControler.ActiveTask.GetResult())
                                Debug.LogError(verdict);
                            Debug.LogError("=================================");
                        }
                    });
                var newButton = CreateButton(buttonParameters);
                var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
                var textParameters = new TextParameters("Check", Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
                var newText = CreateText(textParameters);
                i++;
            }

            {
                var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
                            new Vector2(buttonWidth / 2, -buttonHeight * (i + 0.5f)));
                var buttonParameters = new ButtonParameters("Save", buttonRectParameters, null, () => _sceneControler.SaveBinary("saveFile.binary"));
                var newButton = CreateButton(buttonParameters);
                var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
                var textParameters = new TextParameters("Save", Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
                var newText = CreateText(textParameters);
                i++;
            }

            {
                var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
                            new Vector2(buttonWidth / 2, -buttonHeight * (i + 0.5f)));
                var buttonParameters = new ButtonParameters("Load", buttonRectParameters, null, () =>
                {
                    _state = State.TaskScene;
                    if (_sceneControler.ActiveTask)
                        _sceneControler.StopTask();
                    _sceneControler.LoadBinary("saveFile.binary");
                    ChangeMenuState(false);
                });
                var newButton = CreateButton(buttonParameters);
                var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
                var textParameters = new TextParameters("Load", Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
                var newText = CreateText(textParameters);
            }
        }

        private void CallChangeMenuState (InputAction.CallbackContext obj)
        {
            if (_state != State.StartMenu)
                ChangeMenuState(!_isActive);
        }

        private void ChangeMenuState (bool state)
        {
            _isActive = state;
            _menu.SetActive(_isActive);
            Cursor.visible = state;

            if (_sceneControler.ActiveTask != null)
            {
                foreach (var player in _sceneControler.ActiveTask.Players)
                    player.gameObject.SetActive(!state);
            }
        }
    }
}
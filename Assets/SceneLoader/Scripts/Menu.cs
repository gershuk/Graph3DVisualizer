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

#nullable enable

using Graph3DVisualizer.SceneController;

using UnityEngine;
using UnityEngine.InputSystem;

using static Graph3DVisualizer.Gui.GUIFactory;

namespace Graph3DVisualizer.SceneLoader
{
    /// <summary>
    /// Component that creates a menu for working with <see cref="SceneLoader"/>.
    /// </summary>
    [RequireComponent(typeof(SceneLoader))]
    public class Menu : MonoBehaviour
    {
        private enum State
        {
            StartMenu,
            TaskScene,
        }

        [SerializeField]
        private GameObject _content;

        [SerializeField]
        private Font _font;

        private bool _isActive = true;

        [SerializeField]
        private GameObject _menu;

        private SceneLoader _sceneLoader;

        private void Awake ()
        {
            _sceneLoader = GetComponent<SceneLoader>();
            _sceneLoader.LoadMods();
            _sceneLoader.FindAllTasks();
            Cursor.visible = false;

            var taskList = _sceneLoader.TaskList;

            var buttonHeight = _content.GetComponent<RectTransform>().sizeDelta.y / 15;
            var buttonWidth = _content.GetComponent<RectTransform>().sizeDelta.x;
            var i = 0;
            for (i = 0; i < taskList.Count; i++)
            {
                var task = taskList[i];
                {
                    var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
                        new Vector2(buttonWidth / 2, -buttonHeight * (i + 0.5f)));
                    var index = i;
                    var buttonParameters = new ButtonParameters(
                        () =>
                        {
                            ChangeMenuState(false);
                            _sceneLoader.StartTask(index);
                        }, task.Name, buttonRectParameters, null);
                    var newButton = CreateButton(buttonParameters);
                    var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
                    var textParameters = new TextParameters(task.Name, Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
                    var newText = CreateText(textParameters);
                }
            }

            {
                var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
                            new Vector2(buttonWidth / 2, -buttonHeight * (i + 0.5f)));
                var buttonParameters = new ButtonParameters(
                    () =>
                    {
                        _sceneLoader.StopTask();
                        ChangeMenuState(true);
                    }, "StartMenu", buttonRectParameters, null);
                var newButton = CreateButton(buttonParameters);
                var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
                var textParameters = new TextParameters("StartMenu", Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
                var newText = CreateText(textParameters);
                i++;
            }

            //{
            //    var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
            //                new Vector2(buttonWidth / 2, -buttonHeight * (i + 0.5f)));
            //    var buttonParameters = new ButtonParameters(
            //        () =>
            //        {
            //            if (_state == State.TaskScene)
            //            {
            //                Debug.LogError($"{_sceneControler.ActiveTask.name} result:");
            //                foreach (var verdict in _sceneControler.ActiveTask.GetResult())
            //                    Debug.LogError(verdict);
            //                Debug.LogError("=================================");
            //            }
            //        }, "Check", buttonRectParameters, null);
            //    var newButton = CreateButton(buttonParameters);
            //    var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            //    var textParameters = new TextParameters("Check", Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
            //    var newText = CreateText(textParameters);
            //    i++;
            //}

            {
                var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
                            new Vector2(buttonWidth / 2, -buttonHeight * (i + 0.5f)));
                var buttonParameters = new ButtonParameters(() => _sceneLoader.SaveBinary("saveFile.binary"), "SaveBinary", buttonRectParameters, null);
                var newButton = CreateButton(buttonParameters);
                var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
                var textParameters = new TextParameters("SaveBinary", Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
                var newText = CreateText(textParameters);
                i++;
            }

            {
                var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
                            new Vector2(buttonWidth / 2, -buttonHeight * (i + 0.5f)));
                var buttonParameters = new ButtonParameters(() =>
                {
                    _sceneLoader.LoadBinary("saveFile.binary");
                    ChangeMenuState(false);
                }, "LoadBinary", buttonRectParameters, null);
                var newButton = CreateButton(buttonParameters);
                var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
                var textParameters = new TextParameters("LoadBinary", Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
                var newText = CreateText(textParameters);
                i++;
            }

            {
                var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
                            new Vector2(buttonWidth / 2, -buttonHeight * (i + 0.5f)));
                var buttonParameters = new ButtonParameters(() => _sceneLoader.SaveJson("saveFile.json"), "SaveJson", buttonRectParameters, null);
                var newButton = CreateButton(buttonParameters);
                var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
                var textParameters = new TextParameters("SaveJson", Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
                var newText = CreateText(textParameters);
                i++;
            }

            {
                var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
                            new Vector2(buttonWidth / 2, -buttonHeight * (i + 0.5f)));
                var buttonParameters = new ButtonParameters(() =>
                {
                    _sceneLoader.LoadJson("saveFile.json");
                    ChangeMenuState(false);
                }, "LoadJson", buttonRectParameters, null);
                var newButton = CreateButton(buttonParameters);
                var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
                var textParameters = new TextParameters("LoadJson", Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
                var newText = CreateText(textParameters);
                i++;
            }

            _sceneLoader.StartTask(_sceneLoader.TaskList.FindIndex(0, (t) => t == typeof(HubScene)));
        }

        private void CallChangeMenuState (InputAction.CallbackContext obj) => ChangeMenuState(!_isActive);

        private void ChangeMenuState (bool state)
        {
            _isActive = state;
            _menu.SetActive(_isActive);
            //Cursor.visible = state;

            //if (_sceneLoader.ActiveTask != null)
            //{
            //    foreach (var player in _sceneLoader.ActiveTask.Players)
            //        player.gameObject.SetActive(!state);
            //}
        }
    }
}
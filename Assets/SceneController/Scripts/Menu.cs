using Grpah3DVisualser;

using UnityEngine;
using UnityEngine.InputSystem;

using static Grpah3DVisualser.GUIFactory;

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
    private GameObject _menu;
    [SerializeField]
    private Font _font;

    private State _state = State.StartMenu;
    private InputActionMap _inputActions;
    private bool _isActive = true;
    private SceneController _sceneControler;

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

        var buttonHeight = _content.GetComponent<RectTransform>().sizeDelta.y / 5;
        var buttonWidth = _content.GetComponent<RectTransform>().sizeDelta.x;
        for (var i = 0; i < taskList.Count; i++)
        {
            var task = taskList[i];
            {
                var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
                    new Vector2(buttonWidth / 2, -buttonHeight * (i + 0.5f)));
                var buttonParameters = new ButtonParameters(task.Name, buttonRectParameters,
                    () =>
                    {
                        _state = State.TaskScene;
                        _sceneControler.CurrentTaskType = task;
                        ChangeMenuState(false);
                        if (_sceneControler.VisualTask != null)
                            _sceneControler.VisualTask.DestroyTask();
                        CreateTask();
                    });
                var newButton = CreateButton(buttonParameters);
                var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
                var textParameters = new TextParameters(task.Name, Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
                var newText = CreateText(textParameters);
            }
        }

        {
            var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
                        new Vector2(buttonWidth / 2, -buttonHeight * (taskList.Count + 0.5f)));
            var buttonParameters = new ButtonParameters("StartMenu", buttonRectParameters,
                () =>
                {
                    _state = State.StartMenu;
                    _sceneControler.CurrentTaskType = null;
                    ChangeMenuState(true);
                    if (_sceneControler.VisualTask != null)
                        _sceneControler.VisualTask.DestroyTask();
                });
            var newButton = CreateButton(buttonParameters);
            var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            var textParameters = new TextParameters("StartMenu", Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
            var newText = CreateText(textParameters);
        }

        {
            var buttonRectParameters = new RectTransformParameters(_content.transform, Vector2.up, Vector2.up, new Vector2(buttonWidth, buttonHeight),
                        new Vector2(buttonWidth / 2, -buttonHeight * (taskList.Count + 1 + 0.5f)));
            var buttonParameters = new ButtonParameters("Check", buttonRectParameters,
                () => { if (_state == State.TaskScene) foreach (var verdict in _sceneControler.VisualTask.GetResult()) Debug.Log(verdict); });
            var newButton = CreateButton(buttonParameters);
            var textRectParameters = new RectTransformParameters(newButton.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            var textParameters = new TextParameters("Check", Color.black, _font, TextAnchor.MiddleCenter, 24, textRectParameters);
            var newText = CreateText(textParameters);
        }
    }

    private void CreateTask ()
    {
        if (_state == State.TaskScene)
            _sceneControler.CreateTask();
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

        if (_sceneControler.VisualTask != null)
        {
            foreach (var player in _sceneControler.VisualTask.Players)
                player.gameObject.SetActive(!state);
        }
    }
}

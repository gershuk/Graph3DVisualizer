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

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerTech : MonoBehaviour
{
    private readonly string[] _characters = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u",
                                   "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P",
                                   "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "_",
                                   "-"};

    private readonly string _delDecryptedPass;

    private readonly String[] _delLines;

    private readonly string _loadSceneName;

    private string _confPassword;

    private string _decryptedPass;

    private string _delPasswordString;

    private string _delUsernameString;

    private string _form;

    private String[] _lines;

    private string _logPasswordString;

    private string _logUsernameString;

    private string _password;

    private string _path;

    private string[] _qualityNames;

    private Resolution[] _resolutions;

    [Header("List Indexing")]
    private int _speakersIndex = 0;

    private int _subtitleLanguageIndex = 0;

    private Transform _tempParent;

    private int _tempQualityLevel;

    private string _username;

    private float _xScale = 0f;

    private float _yScale = 0f;

    public bool advancedMenu = true;

    // store it for start up text update
    [Tooltip("The volume slider UI element in the Settings Screen")]
    public Slider audioSlider;

    public Image[] blurs;

    public TMP_InputField confPassword;

    [Tooltip("The UI Panel holding the credits")]
    public GameObject creditsScreen;

    [Tooltip("The UI Panel holding account and load menu")]
    public GameObject databaseScreen;

    [Tooltip("The date and time display text at the bottom of the screen")]
    public TMP_Text dateDisplay;

    [Tooltip("The UI Panel holding the Delete Account Screen elements")]
    public GameObject deleteAccountScreen;

    public string deletedMessageDisplay = "ACCOUNT DELETED";

    public TMP_InputField delPassword;

    [Header("Delete Account")]
    public TMP_InputField delUsername;

    // the input in delUsername
    // the input in delPassword
    public TMP_Text error_Delete;

    // the input in logUsername
    // the input in logPassword
    public TMP_Text error_LogIn;

    public TMP_Text error_NewAccount;

    [Tooltip("If a message is displaying indiciating FAILURE, this is the color of that error text")]
    public Color errorColor;

    [Header("Simple Panels")]
    [Tooltip("The UI Panel holding the Home Screen elements")]
    public GameObject homeScreen;

    [Tooltip("The UI Panel holding the YES or NO Options for Load Game")]
    public GameObject loadGameScreen;

    [Tooltip("The loading bar Slider UI element in the Loading Screen")]
    public Slider loadingBar;

    [Tooltip("The Loading Screen holding loading bar")]
    public GameObject loadingScreen;

    public string loginMessageDisplay = "LOGGED IN";

    [Tooltip("The UI Panel holding Log-In Buttons")]
    public GameObject loginScreen;

    public TMP_InputField logPassword;

    [Header("Login Account")]
    public TMP_InputField logUsername;

    [Header("ADVANDED - UI Elements & User Data")]
    [Tooltip("The Main Canvas Gameobject")]
    public CanvasScaler mainCanvas;

    [Tooltip("The UI Menu Bar at the edge of the screen")]
    public GameObject menuBar;

    [Tooltip("The Arrow at the corner of the screen activating and de-activating the menu bar")]
    public GameObject menuBarButton;

    public TMP_Text messageDisplayDatabase;

    public float messageDisplayLength = 2.0f;

    public string newAccountMessageDisplay = "ACCOUNT CREATED";

    [Header("ADVANDED - Panels")]
    [Tooltip("The UI Panel holding the New Account Screen elements")]
    public GameObject newAccountScreen;

    [Tooltip("The UI Panel holding the CANCEL or ACCEPT Options for New Game")]
    public GameObject newGameScreen;

    [Header("Loading Screen Elements")]
    [Tooltip("The name of the scene loaded when a 'NEW GAME' is started")]
    public string newSceneName;

    [Header("COLORS - Tint")]
    public Image[] panelGraphics;

    public TMP_InputField password;

    public TMP_Text profileDisplay;

    [Tooltip("The icon showing the current quality selected in the Settings Panels")]
    public Animator qualityDisplay;

    [Tooltip("The text object in the Settings Panel displaying the current quality setting enabled")]
    public TMP_Text qualityText;

    [Header("Debug")]
    [Tooltip("If this is true, pressing 'R' will reload the scene.")]
    public bool reloadSceneButton = true;

    [Tooltip("The dropdown menu containing all the resolutions that your game can adapt to")]
    public TMP_Dropdown ResolutionDropDown;

    public bool showDate = true;

    [Header("Menu Bar")]
    public bool showMenuBar = true;

    public bool showTime = true;

    [Header("What Menu Is Active?")]
    public bool simpleMenu = false;

    public List<string> speakers = new List<string>();

    [Header("Starting Options Values")]
    public int speakersDefault = 0;

    public List<string> subtitleLanguage = new List<string>();

    public int subtitleLanguageDefault = 0;

    // text displaying current selected quality
    [Tooltip("If a message is displaying indiciating SUCCESS, this is the color of that success text")]
    public Color successColor;

    [Tooltip("The UI Panel holding the settings")]
    public GameObject systemScreen;

    [Header("Settings Screen")]
    public TMP_Text textSpeakers;

    public TMP_Text textSubtitleLanguage;
    public TMP_Text timeDisplay;
    public Color tint;
    public Slider uiScaleSlider;
    // scene name is defined when the load game data is retrieved

    [Header("Register Account")]
    public TMP_InputField username;

    // Load Bar synching animation
    private IEnumerator LoadAsynchronously (string sceneName)
    { // scene name is just the name of the current scene being loaded
        var operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            var progress = Mathf.Clamp01(operation.progress / .9f);

            loadingBar.value = progress;

            yield return null;
        }
    }

    private IEnumerator MessageDisplay (string message, Color col)
    { // Display and then clear
        messageDisplayDatabase.color = col;
        messageDisplayDatabase.text = message;
        yield return new WaitForSeconds(messageDisplayLength);
        messageDisplayDatabase.text = "";
    }

    // Converts the resolution into a string form that is then used in the dropdown list as the options
    private string ResToString (Resolution res)
    {
        return res.width + " x " + res.height;
    }

    private void Start ()
    {
        // By default, starts on the home screen, disables others
        homeScreen.SetActive(true);
        var Screens = new GameObject[] { creditsScreen, systemScreen,
                                        loadingScreen, loadGameScreen, newGameScreen};
        for (var i = 0; i < Screens.Length; i++)
        {
            Screens[i].SetActive(false);
        }

        if (advancedMenu)
        {
            // Set Save Path to local
            _path = Application.dataPath;
            UpdateAccountValues();
        }

        if (menuBar != null)
        {
            if (!showMenuBar)
            {
                menuBar.gameObject.SetActive(false);
                menuBarButton.gameObject.SetActive(false);
            }
        }

        // Set Colors if the user didn't before play
        for (var i = 0; i < panelGraphics.Length; i++)
        {
            panelGraphics[i].color = tint;
        }
        for (var i = 0; i < blurs.Length; i++)
        {
            blurs[i].material.SetColor("_Color", tint);
        }

        // Get quality settings names
        _qualityNames = QualitySettings.names;

        // Get screens possible resolutions
        _resolutions = Screen.resolutions;

        // Set Drop Down resolution options according to possible screen resolutions of your monitor
        if (ResolutionDropDown != null)
        {
            for (var i = 0; i < _resolutions.Length; i++)
            {
                ResolutionDropDown.options.Add(new TMP_Dropdown.OptionData(ResToString(_resolutions[i])));

                ResolutionDropDown.value = i;

                ResolutionDropDown.onValueChanged.AddListener(delegate
                {
                    Screen.SetResolution(_resolutions
                    [ResolutionDropDown.value].width, _resolutions[ResolutionDropDown.value].height, true);
                });
            }
        }

        // Check if first time so the volume can be set to MAX
        if (PlayerPrefs.GetInt("firsttime") == 0)
        {
            // it's the player's first time. Set to false now...
            PlayerPrefs.SetInt("firsttime", 1);
            PlayerPrefs.SetFloat("volume", 1);
        }

        // Check volume that was saved from last play
        if (audioSlider != null)
            audioSlider.value = PlayerPrefs.GetFloat("volume");

        // Settings screen
        _speakersIndex = speakersDefault;
        _subtitleLanguageIndex = subtitleLanguageDefault;

        textSpeakers.text = speakers[speakersDefault];
        textSubtitleLanguage.text = subtitleLanguage[subtitleLanguageDefault];
    }

    // Just for reloading the scene! You can delete this function entirely if you want to
    private void Update ()
    {
        if (reloadSceneButton)
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                SceneManager.LoadScene("Tech Demo Scene");
            }
        }

        SetTint();

        if (showMenuBar)
        {
            // Menu Bar and Clock/Date Elements
            var time = DateTime.Now;
            if (showTime)
            {
                timeDisplay.text = time.Hour + ":" + time.Minute + ":" + time.Second;
            }
            else if (!showTime)
            {
                timeDisplay.text = "";
            }
            if (showDate)
            {
                dateDisplay.text = System.DateTime.Now.ToString("yyyy/MM/dd");
            }
            else if (!showDate)
            {
                dateDisplay.text = "";
            }
        }
    }

    // Whenever a value on the audio slider in the settings panel is changed, this
    // function is called and updated the overall game volume
    public void AudioSlider ()
    {
        AudioListener.volume = audioSlider.value;
        PlayerPrefs.SetFloat("volume", audioSlider.value);
    }

    // Make sure all the settings panel text are displaying current quality settings properly and updating UI
    public void CheckSettings ()
    {
        _tempQualityLevel = QualitySettings.GetQualityLevel();
        if (_tempQualityLevel == 0)
        {
            qualityText.text = _qualityNames[0];
            qualityDisplay.SetTrigger("Low");
        }
        else if (_tempQualityLevel == 1)
        {
            qualityText.text = _qualityNames[1];
            qualityDisplay.SetTrigger("Medium");
        }
        else if (_tempQualityLevel == 2)
        {
            qualityText.text = _qualityNames[2];
            qualityDisplay.SetTrigger("High");
        }
        else if (_tempQualityLevel == 3)
        {
            qualityText.text = _qualityNames[3];
            qualityDisplay.SetTrigger("Ultra");
        }
    }

    public void ConfirmDeleteAccount ()
    {
        var UN = false;
        var PW = false;
        if (_delUsernameString != "" && profileDisplay.text != _delUsernameString)
        {
            if (System.IO.File.Exists(_path + "_" + _delUsernameString + ".txt"))
            {
                UN = true;
                _lines = System.IO.File.ReadAllLines(_path + "_" + _delUsernameString + ".txt");
            }
            else
            {
                error_Delete.color = errorColor;
                error_Delete.text = "INVALID USERNAME";
            }
        }
        else
        {
            error_Delete.color = errorColor;
            error_Delete.text = "ENTER VALID USERNAME";
        }
        if (_delPasswordString != "")
        {
            if (System.IO.File.Exists(_path + "_" + _delUsernameString + ".txt"))
            {
                var i = 1;
                foreach (var c in _lines[2])
                {
                    i++;
                    var Decrypted = (char) (c / i);
                    _decryptedPass += Decrypted.ToString();
                }
                if (_delPasswordString == _decryptedPass)
                {
                    PW = true;
                }
                else
                {
                    error_Delete.color = errorColor;
                    error_Delete.text = "PASSWORD INCORRECT";
                }
            }
            else
            {
                error_Delete.color = errorColor;
                error_Delete.text = "PASSWORD INCORRECT";
            }
        }
        else
        {
            error_Delete.color = errorColor;
            error_Delete.text = "PLEASE ENTER PASSWORD";
        }
        if (UN == true && PW == true)
        {
            System.IO.File.Delete(_path + "_" + _delUsernameString + ".txt");
            _delUsernameString = "";
            _delPasswordString = "";
            delUsername.text = "";
            delPassword.text = "";
            error_Delete.text = "";
            _decryptedPass = "";

            MessageDisplayDatabase(deletedMessageDisplay, successColor);
            print("Deletion Successful");

            deleteAccountScreen.SetActive(false);
            databaseScreen.SetActive(true);
        }
    }

    public void ConfirmNewAccount ()
    {
        var UN = false;
        var PW = false;
        var CPW = false;

        if (_username != "")
        {
            if (!System.IO.File.Exists(_path + "_" + _username + ".txt"))
            {
                UN = true;
            }
            else
            {
                error_NewAccount.color = errorColor;
                error_NewAccount.text = "USERNAME ALREADY TAKEN";
            }
        }
        else
        {
            error_NewAccount.color = errorColor;
            error_NewAccount.text = "INVALID USERNAME";
        }
        if (_password != "")
        {
            if (_password.Length > 5)
            {
                PW = true;
            }
            else
            {
                error_NewAccount.color = errorColor;
                error_NewAccount.text = "PASSWORD IS TOO SHORT";
            }
        }
        else
        {
            error_NewAccount.color = errorColor;
            error_NewAccount.text = "INVALID PASSWORD";
        }
        if (_confPassword != "")
        {
            if (_confPassword == _password)
            {
                CPW = true;
            }
            else
            {
                error_NewAccount.color = errorColor;
                error_NewAccount.text = "PASSWORDS MUST MATCH";
            }
        }
        else
        {
            error_NewAccount.color = errorColor;
            error_NewAccount.text = "INVALID PASSWORD";
        }
        if (UN == true && PW == true && CPW == true)
        {
            var Clear = true;
            var i = 1;
            foreach (var c in _password)
            {
                if (Clear)
                {
                    _password = "";
                    Clear = false;
                }
                i++;
                var Encrypted = (char) (c * i);
                _password += Encrypted.ToString();
            }
            _form = (_username + Environment.NewLine + Environment.NewLine + _password);
            System.IO.File.WriteAllText(_path + "_" + _username + ".txt", _form);
            _username = "";
            _password = "";
            username.text = "";
            password.text = "";
            confPassword.text = "";
            error_NewAccount.text = "";
            _decryptedPass = "";

            MessageDisplayDatabase(newAccountMessageDisplay, successColor);
            print("Registration Complete");

            databaseScreen.SetActive(true);
            newAccountScreen.SetActive(false);
        }
    }

    public void DecreaseIndex (int i)
    {
        switch (i)
        {
            case 0:
                if (_speakersIndex == 0)
                {
                    _speakersIndex = speakers.Count;
                }
                _speakersIndex--;
                textSpeakers.text = speakers[_speakersIndex];
                break;

            case 1:
                if (_subtitleLanguageIndex == 0)
                {
                    _subtitleLanguageIndex = subtitleLanguage.Count;
                }
                _subtitleLanguageIndex--;
                textSubtitleLanguage.text = subtitleLanguage[_subtitleLanguageIndex];
                break;

            default:
                break;
        }
    }

    public void IncreaseIndex (int i)
    {
        switch (i)
        {
            case 0:
                if (_speakersIndex != speakers.Count - 1)
                {
                    _speakersIndex++;
                }
                else
                {
                    _speakersIndex = 0;
                }
                textSpeakers.text = speakers[_speakersIndex];
                break;

            case 1:
                if (_subtitleLanguageIndex != subtitleLanguage.Count - 1)
                {
                    _subtitleLanguageIndex++;
                }
                else
                {
                    _subtitleLanguageIndex = 0;
                }
                textSubtitleLanguage.text = subtitleLanguage[_subtitleLanguageIndex];
                break;

            default:
                break;
        }
    }

    // Called when loading new game scene
    public void LoadNewLevel ()
    {
        if (newSceneName != "")
        {
            StartCoroutine(LoadAsynchronously(newSceneName));
        }
    }

    // Called when loading saved scene
    // Add the save code in this function!
    public void LoadSavedLevel ()
    {
        if (_loadSceneName != "")
        {
            StartCoroutine(LoadAsynchronously(newSceneName)); // temporarily uses New Scene Name. Change this to 'loadSceneName' when you program the save data
        }
    }

    public void LoginButton ()
    {
        var UN = false;
        var PW = false;
        if (_logUsernameString != "")
        {
            if (System.IO.File.Exists(_path + "_" + _logUsernameString + ".txt"))
            {
                UN = true;
                _lines = System.IO.File.ReadAllLines(_path + "_" + _logUsernameString + ".txt");
            }
            else
            {
                error_LogIn.color = errorColor;
                error_LogIn.text = "INVALID USERNAME";
            }
        }
        else
        {
            error_LogIn.color = errorColor;
            error_LogIn.text = "PLEASE ENTER USERNAME";
        }
        if (_logPasswordString != "")
        {
            if (System.IO.File.Exists(_path + "_" + _logUsernameString + ".txt"))
            {
                var i = 1;
                foreach (var c in _lines[2])
                {
                    i++;
                    var Decrypted = (char) (c / i);
                    _decryptedPass += Decrypted.ToString();
                }
                if (_logPasswordString == _decryptedPass)
                {
                    PW = true;
                }
                else
                {
                    error_LogIn.color = errorColor;
                    error_LogIn.text = "PASSWORD INCORRECT";
                }
            }
            else
            {
                error_LogIn.color = errorColor;
                error_LogIn.text = "PASSWORD INCORRECT";
            }
        }
        else
        {
            error_LogIn.color = errorColor;
            error_LogIn.text = "PLEASE ENTER PASSWORD";
        }
        if (UN == true && PW == true)
        {
            profileDisplay.text = _logUsernameString;
            _logUsernameString = "";
            _logPasswordString = "";
            logUsername.text = "";
            logPassword.text = "";
            error_LogIn.text = "";
            _decryptedPass = "";

            MessageDisplayDatabase(loginMessageDisplay, successColor);
            print("Login Successful");

            databaseScreen.SetActive(true);
            loginScreen.SetActive(false);
        }
    }

    // called when returned back to the database menu, confirmation message displays temporarily
    public void MessageDisplayDatabase (string message, Color col)
    {
        StartCoroutine(MessageDisplay(message, col));
    }

    public void MoveToFront (GameObject currentObj)
    {
        //tempParent = currentObj.transform.parent;
        _tempParent = currentObj.transform;
        _tempParent.SetAsLastSibling();
    }

    // Changes the current quality settings by taking the number passed in from the UI
    // element and matching it to the index of the Quality Settings
    public void QualityChange (int x)
    {
        if (x == 0)
        {
            QualitySettings.SetQualityLevel(x, true);
            qualityText.text = _qualityNames[0];
        }
        else if (x == 1)
        {
            QualitySettings.SetQualityLevel(x, true);
            qualityText.text = _qualityNames[1];
        }
        else if (x == 2)
        {
            QualitySettings.SetQualityLevel(x, true);
            qualityText.text = _qualityNames[2];
        }
        if (x == 3)
        {
            QualitySettings.SetQualityLevel(x, true);
            qualityText.text = _qualityNames[3];
        }
    }

    // When accepting the QUIT question, the application will close
    // (Only works in Executable. Disabled in Editor)
    public void Quit ()
    {
        Application.Quit();
    }

    public void SetTint ()
    {
        for (var i = 0; i < panelGraphics.Length; i++)
        {
            panelGraphics[i].color = tint;
        }
        for (var i = 0; i < blurs.Length; i++)
        {
            blurs[i].material.SetColor("_Color", tint);
        }
    }

    public void UIScaler ()
    {
        _xScale = 1920 * uiScaleSlider.value;
        _yScale = 1080 * uiScaleSlider.value;
        mainCanvas.referenceResolution = new Vector2(_xScale, _yScale);
    }

    public void UpdateAccountValues ()
    {
        // Register
        _username = username.text;
        _password = password.text;
        _confPassword = confPassword.text;
        // Log In
        _logUsernameString = logUsername.text;
        _logPasswordString = logPassword.text;
        // Delete
        _delUsernameString = delUsername.text;
        _delPasswordString = delPassword.text;
    }
}
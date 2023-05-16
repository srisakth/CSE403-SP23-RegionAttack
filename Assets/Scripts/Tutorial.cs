using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Internal game object
    Game _game;

    // Access to the game manager
    public GameManager _gameManager;
    public GameSetting _gameOption;

    // Tutorial text
    public PopUp _popUp;
    public Button _homeButton;

    // Messages
    public TextAsset _script;
    public string[] _messages;
    int _index;

    void Awake()
    {
        if (_script != null)
        {
            // Load the messages from the text file
            _messages = _script.text.Split('\n');
        }

        _popUp.SetDuration(2);
        _popUp._default = true;
    }

    public void StartTutorial()
    {
        // Set the default options
        _gameOption.SetPlayerMode(false);
        _gameOption.SetOnlineMode(false);
        _gameOption.SetHelperMode(true);
        _gameOption.SetDimension(1);

        // Start a new game
        _gameManager.StartGame();

        // Define the tutorial version of the game 
        _game = new Game(6, false);
        _game.isP1Turn = true;
        _game.p1.numberPool = new List<int> { 4, 3, 9, 12 };
        _game.p2.numberPool = new List<int> { 1, 7, 5, 8 };

        // Pass it to the game Manager
        _gameManager.SetGame(_game);

        // Pause the game to allow reading
        _gameManager.PauseGame(true);

        _index = 0;
        _popUp._container.SetActive(true);
        Continue();
    }

    public void Continue()
    {
        // The player decided to still play
        if (_index == _messages.Length)
        {
            _gameOption.SetPlayerMode(true);
            _gameOption.SetTutorial(false);
            _popUp._container.SetActive(false);

            // Instantiate a Computer player
            Player computer = new ComputerPlayer(1, _game);
            computer.numberPool = _game.p2.numberPool;
            _game.p2 = computer;
            _gameManager.SetGame(_game);
            _gameManager._gameTimer.ResetTimer();

            return;
        }

        _popUp.SetMessage(_messages[_index]);

        // Special events for some indices
        // The number corresponds to the line number of the text displayed before the specified action occurs

        if (_index == 4)
        {
            _popUp.StartDisplay(_messages[_index]);
        }

        // Hide the display until a move happens
        if (_index == 15)
        {
            _popUp._container.SetActive(false);

            foreach (Tile tile in _gameManager._p1Hand._hand)
            {
                if (tile._num != 3)
                {
                    tile._button.enabled = false;
                }
            }
        }
        if (_index == 16)
        {
            _gameManager._game.p1.numberPool = new List<int>{ 4,11,9,12 };
            _gameManager.DisplayHand(_gameManager._p1Hand, _gameManager._game.p1);
            _popUp._container.SetActive(true);
        }
        if (_index == 17)
        {
            StartCoroutine(_gameManager.MakeMove((1, 1), 7));
            _popUp.StartDisplay(_messages[_index]);
        }

        if (_index == 18)
        {
            _gameManager._game.p2.numberPool = new List<int> { 1, 9, 5, 8 };
            _gameManager.DisplayHand(_gameManager._p2Hand, _gameManager._game.p2);
            _gameManager._p2Hand.SetEnable(false);
        }

        // Hide the display until a move happens
        if (_index == 20)
        {
            _popUp._container.SetActive(false);
            foreach(Tile tile in _gameManager._p1Hand._hand)
            {
                if (tile._num != 12)
                {
                    tile._button.enabled = false;
                }
            }
        }
        if (_index == 21)
        {
            _gameManager._game.p1.numberPool = new List<int> { 4, 11, 9, 8 };
            _gameManager.DisplayHand(_gameManager._p1Hand, _gameManager._game.p1);
            _popUp._container.SetActive(true);
        }

        if (_index == 24)
        {
            StartCoroutine(_gameManager.MakeMove((3, 1), 5));
            _popUp.StartDisplay(_messages[_index]);
        }
        if (_index == 25)
        {
            _gameManager._game.p2.numberPool = new List<int> { 1, 9, 3, 8 };
            _gameManager.DisplayHand(_gameManager._p2Hand, _gameManager._game.p2);
            _gameManager._p2Hand.SetEnable(false);

        }

        if (_index == _messages.Length - 1)
        {
            // display the Home button
            _homeButton.gameObject.SetActive(true);
        }

        _index++;
    }
}

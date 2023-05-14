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
    public GameOption _gameOption;

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
        _popUp.gameObject.SetActive(true);
        Continue();
    }

    public void Continue()
    {
        if (_index == _messages.Length)
        {
            // The player decided to still play
            _gameOption.SetPlayerMode(true);
            _popUp.gameObject.SetActive(false);

            // Instantiate a Computer player
            Player computer = new ComputerPlayer(1, _game);
            computer.numberPool = _game.p2.numberPool;
            _game.p2 = computer;
            _gameManager.SetGame(_game);
            _gameManager._gameTimer.ResetTimer();

            return;
        }

        _popUp.SetMessage(_messages[_index++]);

        if (_index == _messages.Length - 1)
        {
            // display the Home button
            _homeButton.gameObject.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


/*
 * Responsible for receiving information from the game board, passing it to the appropriate
 * method of the Game object, and calling appropriate functions in the GUI Managers
 * with the updated information.
 */
public class GameManager : MonoBehaviour
{
    // Internal game object
    public Game _game;

    // Game objects
    public GameSetting _gameOption;

    public GridManager _gridManager;
    public HandManager _p1Hand, _p2Hand;
    public Timer _moveTimer, _gameTimer;
    public PopUp _popup;

    public Score _score, _resultScore;
    public TMP_Text _resultText;
    public GameObject _result;

    public Tutorial _tutorial;

    public Tile _tilePrefab;

    // Selected tiles
    // Although it's not really good to pass these to the game manager, it's easier to deal with nullable objects
    // to check for selection.
    private Tile _numTile, _boardTile;
    private List<(int, int)> _highlightedTiles;

    private WaitForSeconds _wait = new WaitForSeconds(1.5f);

    private void Awake()
    {
        if (_p1Hand != null)
            _p1Hand.Initialize(_tilePrefab, true, this);
        else
            print("P1 Hand Manager not selected");

        if (_p2Hand != null)
            _p2Hand.Initialize(_tilePrefab, false, this);
        else
            print("P2 Hand Manager not selected");

        if (_gridManager != null)
            _gridManager.Initialize(_tilePrefab, this);
        else
            print("Grid Manager not selected");

    }

    public void StartOnlineGame()
    {
        _gameOption._option.mode = GameOption.Mode.online;
        StartGame();
    }

    public void StartLocalGame()
    {
        _gameOption._option.mode = GameOption.Mode.local;
        StartGame();
    }

    public void StartAIGame(int difficulty)
    {
        switch(_gameOption._difficulty)
        {
            case 0:
                _gameOption._option.mode = GameOption.Mode.computerBasic;
                break;
            case 1:
                _gameOption._option.mode = GameOption.Mode.computerAdvanced;
                break;
            default:
                _gameOption._option.mode = GameOption.Mode.computerBasic;
                break;
        }

        StartGame();
    }

    // Starts the game by removing any previous GameObjects, instantiating a new game with the given
    // Game options, and passing it to the GridManager and HandManagers to be displayed.
    void StartGame()
    {
        // Initialize the board tiles
        _gridManager.InitializeGrid(_gameOption._option.dim);

        Vector2 cellSize = _gridManager.GetComponent<GridLayoutGroup>().cellSize;
        _p1Hand.SetSize(cellSize.x * 0.4f, cellSize.x);
        _p2Hand.SetSize(cellSize.x * 0.4f, cellSize.x);

        // Start the game
        RestartGame();
    }

    // Given a game state, updates the view based on it.
    public void SetGame(Game game)
    {
        _game = game;

        // Update the view to reflect the new game state
        // Sync the board information
        _gridManager.UpdateGrid(_game.board);

        // Set the score
        _score.SetScore(game.p1.GetScore(), game.p2.GetScore());

        EndMove();
    }

    // Method to restart the game. Assumes that (1) there was a game played previously, and
    // (2) the grid dimensions are the same
    public void RestartGame()
    {
        // Turn off the results page
        _result.SetActive(false);
                
        // Make a new game
        _game = _gameOption._option.InitGame();

        if (_gameOption._option.IsOnlineGame())
        {
            // TODO: Fetch the board and hand state
        }

        // Sync the board information
        _gridManager.UpdateGrid(_game.board);

        // Set the initial score
        _score.SetScore(0, 0);

        // Alert the first player
        string player = _game.isP1Turn ? "1" : "2";

        if (!_gameOption._isTutorial)
            _popup.StartDisplay(_game.isP1Turn, $"Player {player}'s Turn!");

        _gameTimer.ResetTimer();

        EndMove();
    }

    // Pauses the timers temporalily if pause is true. Else, restarts the timers.
    public void PauseGame(bool pause)
    {
        if (pause)
        {
            _gameTimer.StopTimer();
            _moveTimer.StopTimer();
        } else
        {
            _gameTimer.StartTimer();
            _moveTimer.StartTimer();
        }
    }

    // Terminates the game by removing any tiles in the players' hand and displaying the appropriate win message.
    public void EndGame()
    {
        _p1Hand.ClearHand();
        _p2Hand.ClearHand();
        _moveTimer.StopTimer();

        // Unhighlight the previous tiles
        if (_gameOption._enableHelper && _highlightedTiles != null)
            _gridManager.HighlightTiles(_highlightedTiles, false);

        // Make the references null just in case
        _boardTile = null;
        _numTile = null;
        _highlightedTiles = null;

        _result.SetActive(true);
        _resultScore.SetScore(_game.p1.GetScore(), _game.p2.GetScore());
        _resultText.text = _game.TerminateGame();
    }

    // **** Tile Selection ****

    // Keeps track of the position within the grid
    public void SetPosition(Tile tile)
    {
        _boardTile = tile;
        if (_numTile != null)
            StartCoroutine(MakeMove(_boardTile._position, _numTile._num));
    }

    public void SetNumber(Tile tile)
    {
        // Unhighlight the previous tiles
        if (_numTile != null && _gameOption._enableHelper)
            _gridManager.HighlightTiles(_highlightedTiles, false);

        _numTile = tile;
        if (_boardTile != null)
        {
            StartCoroutine(MakeMove(_boardTile._position, _numTile._num));
        } 
        else if (_gameOption._enableHelper)
        {
            _highlightedTiles = _game.PossibleMoves(tile._num,_game.isP1Turn);
            _gridManager.HighlightTiles(_highlightedTiles, true);
        }
    }

    // With the appropriate tile and number tile selected, tries to make the move by passing it to the
    // Game object. If valid, then updates the board accordingly and adds the new number to the opponent's deck.
    public IEnumerator MakeMove((int, int) position, int num)
    {
        // If it's P2's turn and it's AI, then pause for a bit
        if (!_game.isP1Turn && (_gameOption._option.IsComputerGame() || _gameOption._isTutorial))
            yield return _wait;

        int res = _game.MakeMove(position, num);
        if (res > 0)
        {
            // Update the board
            _gridManager.UpdateGrid(_game.board);

            // Update the score
            _score.SetScore(_game.p1.GetScore(), _game.p2.GetScore());

            EndMove();
        } else
        {
            string message;
            switch(res)
            {
                case 0: 
                    message = "Invalid Position!";
                    break;
                case -1:
                    message = "Can't overwrite an opponent's number if it is larger than your's!";
                    break;
                case -2:
                    message = "The left cell is incompatible!";
                    break;
                case -3:
                    message = "The top cell is incompatible!";
                    break;
                case -4:
                    message = "The right cell is incompatible!";
                    break;
                case -5:
                    message = "The bottom cell is incompatible!";
                    break;
                case -6:
                    message = "You can't place a prime number on the opponent's side!";
                    break;
                default:
                    message = "The number must be prime or be an extension of an existing tile!";
                    break;
            }
            _popup.StartDisplay(message);
            _boardTile = null;
            _numTile = null;
        }
    }

    // Flips the players' turns and ends the move
    public void Skip()
    {
        _game.isP1Turn = !_game.isP1Turn;
        EndMove();
    }

    // Advances the game to the other player's turn
    void EndMove()
    {
        // We should have a new hand, so let's just redisplay everything for now
        DisplayHand(_p1Hand, _game.p1);
        DisplayHand(_p2Hand, _game.p2);

        // Enable/disable the hands
        _p1Hand.SetEnable(_game.isP1Turn && !_gameOption._isTutorial);
        _p2Hand.SetEnable(!_game.isP1Turn && !_gameOption._option.IsComputerGame() && !_gameOption._isTutorial);

        // Unhighlight the previous tiles
        if (_gameOption._enableHelper && _highlightedTiles != null)
            _gridManager.HighlightTiles(_highlightedTiles, false);

        // If it's the tutorial, we don't want to restart the timer
        if (!_gameOption._isTutorial)
            _moveTimer.ResetTimer();

        bool isUpright = _game.isP1Turn || _gameOption._option.IsComputerGame() || (Screen.width > Screen.height);

        _moveTimer.transform.rotation = isUpright ? Quaternion.identity : Quaternion.Euler(0, 0, 180);

        _boardTile = null;
        _numTile = null;
        _highlightedTiles = null;

        // If it's p2's turn and it's computer or online, then we need to fetch the move
        if (!_game.isP1Turn && (_gameOption._option.IsComputerGame() || _gameOption._option.IsOnlineGame()))
        {
            (int, int) position = (-1, -1);
            int num = 0;
            if (_gameOption._option.IsComputerGame())
            {
                ComputerPlayer cp = (ComputerPlayer)_game.p2;
                ((int, int), int) move = cp.FindMove();
                position = move.Item1;
                num = move.Item2;
            } else
            {
                // Fetch the online moves
            }

            if (num > 0)
            {
                // The move should be valid
                StartCoroutine(MakeMove(position, num));
            } else
            {
                // We'll assume they skipped
                Skip();
            }
        }

        // If it's the tutorial mode, then notify whenever a move is made by the player
        // It would be p2's turn if it was p1's turn
        if (_gameOption._isTutorial && !_game.isP1Turn)
        {
            _tutorial.Continue();
        }

    }


    // Helper function called to instantiate the game with respective calls to the hand manager.
    public void DisplayHand(HandManager hand, Player player)
    {
        hand.ClearHand();

        foreach (int num in player.numberPool)
        {
            hand.AddNumber(num);
        }
    }
}

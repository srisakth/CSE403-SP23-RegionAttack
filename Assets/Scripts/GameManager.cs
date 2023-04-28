using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/*
 * Responsible for receiving information from the game board, passing it to the appropriate
 * method of the Game object, and calling appropriate functions in the GUI Managers
 * with the updated information.
 */
public class GameManager : MonoBehaviour
{
    // Internal game object
    Game _game;

    // Options
    bool _isOpponentAI = false;
    int _dim = 6;


    // Option possibilities
    public static readonly int[] DimOptions = {4, 6, 8};


    // Game objects
    public GridManager _gridManager;
    public HandManager _p1Hand, _p2Hand;
    public PopUp _popup;
    public Timer _moveTimer;
    
    public TMP_Text _score1, _score2;

    // Selected tiles
    // Although it's not really good to pass these to the game manager, it's easier to deal with nullable objects
    // to check for selection.
    private Tile _numTile, _boardTile;


    // Starts the game by removing any previous GameObjects, instantiating a new game with the given
    // Game options, and passing it to the GridManager and HandManagers to be displayed.
    public void StartGame()
    {
        // Wipe out any previous stuff
        _gridManager.Clear();

        // Make a new game
        _game = new Game(_dim, _isOpponentAI);

        // Use the game to initialize the board UI
        _gridManager.Initialize(_dim, _game.board);

        // Make the references null just in case
        _boardTile = null;
        _numTile = null;

        // Set the initial score
        _score1.text = "0";
        _score2.text = "0";

        // Display the hand
        DisplayHand(_p1Hand, _game.p1);
        DisplayHand(_p2Hand, _game.p2);

        // Enable/disable the hands
        _p1Hand.SetEnable(_game.isP1Turn);
        _p2Hand.SetEnable(!_game.isP1Turn);

        // Alert the first player
        string player = _game.isP1Turn ? "1" : "2";

        _popup.StartDisplay(true, $"Player {player}'s Turn!");

        // Start the timer
        _moveTimer.ResetTimer();
        _moveTimer.StartTimer();

    }

    // Terminates the game by removing any tiles in the players' hand and displaying the appropriate win message.
    public void EndGame()
    {
        print("STOP!");
        _p1Hand.ClearHand();
        _p2Hand.ClearHand();
        _popup.StartDisplay(false, _game.TerminateGame());
        _moveTimer.StopTimer();

        // Make the references null just in case
        _boardTile = null;
        _numTile = null;
    }

    // **** Game Mode extraction ****

    public void SetPlayerMode(bool isOpponentAI)
    {
        _isOpponentAI = isOpponentAI;
    }

    public void SetDimension(int option)
    {
        _dim = DimOptions[option];
    }

    // **** Tile Selection ****

    // Keeps track of the position within the grid
    public void SetPosition(Tile tile)
    {
        _boardTile = tile;
        if (_numTile != null)
            MakeMove();
    }

    public void SetNumber(Tile tile)
    {
        _numTile = tile;
        if (_boardTile != null)
            MakeMove();
    }

    // With the appropriate tile and number tile selected, tries to make the move by passing it to the
    // Game object. If valid, then updates the board accordingly and adds the new number to the opponent's deck.
    private void MakeMove()
    {
        Debug.Assert(_boardTile != null && _numTile != null);
        int num = _game.MakeMove(_boardTile._position, _numTile._num);
        Debug.Log(_boardTile._position);
        if (num > 0)
        {
            // Update the board
            _gridManager.UpdateGrid(_game.board);

            // Update the score
            _score1.text = $"{_game.p1.getScore()}";
            _score2.text = $"{_game.p2.getScore()}";

            EndMove();
        } else
        {
            _popup.StartDisplay("Invalid Move!");
        }
        _boardTile = null;
        _numTile = null;
    }

    // Advances the game to the other player's turn
    public void EndMove()
    {
        // If either one is set to null, then we must have not placed a number
        if (_numTile == null || _boardTile == null)
            print("Skipped!");
        // _game.Skip();

        // We should have a new hand, so let's just redisplay everything for now
        DisplayHand(_p1Hand, _game.p1);
        DisplayHand(_p2Hand, _game.p2);

        // Enable/disable the hands
        _p1Hand.SetEnable(_game.isP1Turn);
        _p2Hand.SetEnable(!_game.isP1Turn);

        _moveTimer.ResetTimer();
    }


    // Helper function called to instantiate the game with respective calls to the hand manager.
    void DisplayHand(HandManager hand, Player player)
    {
        hand.ClearHand();

        foreach (int num in player.numberPool)
        {
            hand.AddNumber(num);
        }
    }
}

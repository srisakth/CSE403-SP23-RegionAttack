using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public PopUp _popup, _score1, _score2;

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

        // Use the game 
        _gridManager.Initialize(_dim, _game.board);

        DisplayHand(_p1Hand, _game.p1);
        DisplayHand(_p2Hand, _game.p2);

        // Make the references null just in case
        _boardTile = null;
        _numTile = null;
    }

    // Terminates the game by removing any tiles in the players' hand and displaying the appropriate win message.
    public void EndGame()
    {
        print("STOP!");
        _p1Hand.ClearHand();
        _p2Hand.ClearHand();
        _popup.StartDisplay(false, _game.TerminateGame());

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
        bool isP1Turn = _game.isP1Turn;
        int num = _game.MakeMove(_boardTile._position, _numTile._num);
        if (num > 0)
        {
            // The move is valid: delete the hand and redisplay the hand for the other player
            DisplayHand(_p1Hand, _game.p1);
            DisplayHand(_p2Hand, _game.p2);

            // Update the board
            _gridManager.UpdateGrid(!isP1Turn, _boardTile._position, _numTile._num);

            // Update the score
            _score1.StartDisplay(false, _game.p1.getScore().ToString());
            _score2.StartDisplay(false, _game.p2.getScore().ToString());

        } else
        {
            _popup.StartDisplay("Invalid Move!");
        }
        _boardTile = null;
        _numTile = null;
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

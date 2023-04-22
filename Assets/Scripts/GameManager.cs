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
    public PopUp _popup;

    private Tile _numTile, _boardTile;

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
    }

    public void EndGame()
    {
        print("STOP!");
        _p1Hand.ClearHand();
        _p2Hand.ClearHand();
        _popup.StartDisplay(_game.TerminateGame());
    }

    void DisplayHand(HandManager hand, Player player)
    {
        hand.ClearHand();

        foreach (int num in player.numberPool)
        {
            hand.AddNumber(num);
        }
    }

    public void SetPlayerMode(bool isOpponentAI)
    {
        _isOpponentAI = isOpponentAI;
    }

    public void SetDimension(int option)
    {
        _dim = DimOptions[option];
    }

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

    private void MakeMove()
    {
        Debug.Assert(_boardTile != null && _numTile != null);
        bool isP1Turn = _game.isP1Turn;
        int num = _game.MakeMove(_boardTile._position, _numTile._num);
        if (num != 0)
        {
            // The move is valid: delete the hand and redisplay the hand for the other player
            DisplayHand(_p1Hand, _game.p1);
            DisplayHand(_p2Hand, _game.p2);
            // Update the board
            _gridManager.UpdateGrid(isP1Turn, _boardTile._position, _numTile._num);

        } else
        {
            _popup.StartDisplay("Invalid Move!");
        }
        _boardTile = null;
        _numTile = null;
    }
}

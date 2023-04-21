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
    bool _isOpponentAI;
    int _gridDim;

    public static readonly int[] DimOptions = {4, 6, 8};


    // Game objects
    public GridManager _gridManager;
    public HandManager _p1Hand, _p2Hand;

    public (int, int) _position;
    readonly (int, int) nullPosition = (-1, -1);
    public int _number;

    public void StartGame()
    {
        // Wipe out any previous stuff
        _gridManager.Clear();
        _gridManager.Initialize();

        // Make a new game
        _game = new Game(_gridDim);

        DisplayHand(_p1Hand, _game.p1);
        DisplayHand(_p2Hand, _game.p2);

        // Set the position and number to "null"
        _position = nullPosition;
        _number = -1;
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
        _gridDim = DimOptions[option];
    }

    // Keeps track of the position within the grid
    public void SetPosition((int, int) position)
    {
        _position = position;
        if (_number != -1)
            MakeMove();
    }

    public void SetNumber(int number)
    {
        _number = number;
    }

    private void MakeMove()
    {
        Debug.Assert(_position != nullPosition && _number > 0);
        bool isP1Turn = _game.isP1Turn;
        if (_game.MakeMove(_position, _number))
        {
            // The move is valid: delete the hand and redisplay the hand for the other player
            if (isP1Turn)
            {
                _p1Hand.RemoveNumber(_number);
                DisplayHand(_p2Hand, _game.p2);
            }
            else
            {
                _p2Hand.RemoveNumber(_number);
                DisplayHand(_p1Hand, _game.p1);
            }
            // Update the board
            _gridManager.UpdateGrid(isP1Turn, _position, _number);
        }
        _position = nullPosition;
        _number = -1;
    }
}

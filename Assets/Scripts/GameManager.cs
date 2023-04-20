using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public (int, int) _position;
    public int _number;

    public void StartGame()
    {
        _game = _isOpponentAI ? new SinglePlayerGame(_gridDim) : new Game(_gridDim);

        _gridManager.Initialize();
    }

    public void SetPlayerMode(bool isOpponentAI)
    {
        _isOpponentAI = isOpponentAI;
    }

    public void SetDimension(int option)
    {
        _gridDim = DimOptions[option];
    }

    public void SetPosition()
    {

    }

    private void MakeMove()
    {

    }
}

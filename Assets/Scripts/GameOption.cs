using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOption : MonoBehaviour
{
    // Options
    bool _isOpponentAI = false;
    bool _isOnline = false;
    bool _enableHelper = false;
    int _dim = 6;

    // UI components



    // Option possibilities
    public static readonly int[] DimOptions = { 4, 6, 8 };

    // **** Game Mode extraction ****

    public void SetPlayerMode(bool isOpponentAI)
    {
        _isOpponentAI = isOpponentAI;
    }

    public void SetHelperMode(bool enableHelper)
    {
        _enableHelper = enableHelper;
    }

    public void SetDimension(int option)
    {
        _dim = DimOptions[option];
    }
}

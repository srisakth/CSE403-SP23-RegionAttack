using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSetting : MonoBehaviour
{
    // Options
    public bool _isOpponentAI = false;
    public bool _isOnline = false;
    public bool _enableHelper = false;
    public bool _isTutorial = false;
    public int _dim = 6;

    // UI components
    public Toggle _AIToggle, _onlineToggle, _helperToggle;
    public TMP_Dropdown _dimDropdown;


    // Option possibilities
    public static readonly int[] DimOptions = { 4, 6, 8 };

    // **** Game Mode extraction ****

    public void SetPlayerMode(bool isOpponentAI)
    {
        _isOpponentAI = isOpponentAI;
        _AIToggle.isOn = isOpponentAI;
    }

    public void SetHelperMode(bool enableHelper)
    {
        _enableHelper = enableHelper;
        _helperToggle.isOn = enableHelper;
    }

    public void SetOnlineMode(bool isOnline)
    {
        _isOnline  = isOnline;
        _onlineToggle.isOn = isOnline;
    }

    public void SetDimension(int option)
    {
        _dim = DimOptions[option];
        _dimDropdown.value = option;
    }

    public void SetTutorial(bool isTutorial)
    {
        _isTutorial = isTutorial;
    }
}

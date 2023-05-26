using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSetting : MonoBehaviour
{
    // Options
    public bool _enableHelper = false;
    public bool _isTutorial = false;
    public GameOption _option;
    public int _difficulty = 0;

    // UI components
    public Toggle _helperToggle;
    public TMP_Dropdown _dimDropdown, _difficultyDropdown;

    // Option possibilities
    public static readonly int[] DimOptions = { 4, 6, 8 };

    private void Awake()
    {
        _option = new GameOption(6, true, GameOption.Mode.local);
    }


    // **** Game Mode extraction ****

    public void SetHelperMode(bool enableHelper)
    {
        _enableHelper = enableHelper;
        _helperToggle.isOn = enableHelper;
    }

    public void SetDimension(int option)
    {
        _option.dim = DimOptions[option];
        _dimDropdown.value = option;
    }

    public void SetTutorial(bool isTutorial)
    {
        _isTutorial = isTutorial;
    }

    public void SetDifficulty(int option)
    {
        _difficulty = option;
        _difficultyDropdown.value = option;
    }
}

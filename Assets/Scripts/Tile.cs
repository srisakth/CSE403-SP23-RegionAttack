using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Describes the properties of the tiles
public class Tile : MonoBehaviour
{
    public bool _isP1Side;
    public bool _isP1Num;
    public int _num, _x, _y;

    private Text _text;

    public void Init(bool isP1, int x, int y)
    {
        _isP1Side = isP1;
        _num = 0;
        _x = x;
        _y = y;
    }

    public void setNum(int num, bool isP1Num)
    {
        _num = num;
        _isP1Num = isP1Num;
        _text.text = num.ToString();
    }
}

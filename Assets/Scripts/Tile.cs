using TMPro;
using UnityEngine;

// Describes the properties of the tiles
public class Tile : MonoBehaviour
{
    public bool _isP1Side;
    public bool _isP1Num;
    public int _num;
    public (int, int) _position;

    public TMP_Text _text;

    public void Init(bool isP1, (int, int) position)
    {
        _isP1Side = isP1;
        _num = 0;
        _text.text = "";
        _position = position;
    }

    public void SetNum(bool isP1Num, int num)
    {
        _num = num;
        _isP1Num = isP1Num;
        _text.text = num.ToString();
    }
}

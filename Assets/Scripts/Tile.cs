using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Describes the properties of the tiles
public class Tile : MonoBehaviour
{
    // Internal information
    public bool _isP1;
    public int _num;
    public (int, int) _position;

    // References to the UI that changes
    public TMP_Text _text;
    public Button _button;

    public ColorBlock _p1ColorBlock, _p2ColorBlock;


    public void Init(bool isP1, (int, int) position)
    {
        _isP1 = isP1;
        _num = 0;
        _text.text = "";
        _position = position;
        _button.colors = isP1 ? _p1ColorBlock : _p2ColorBlock;
    }

    public void SetNum(bool isP1, int num)
    {
        _num = num;
        _isP1 = isP1;
        _text.text = num.ToString();

        _button.colors = isP1 ? _p1ColorBlock : _p2ColorBlock;
    }
}

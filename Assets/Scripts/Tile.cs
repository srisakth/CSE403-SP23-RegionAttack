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

    public float _hue = 0;

    public ColorBlock _p1ColorBlock, _p2ColorBlock;


    public void Init(bool isP1, (int, int) position)
    {
        _isP1 = isP1;
        _num = 0;
        _text.text = "";
        _position = position;
        float other = _hue > 0.5 ? _hue - 0.5f : _hue + 0.5f;
        _p1ColorBlock = GenerateColorBlock(_hue);
        _p2ColorBlock = GenerateColorBlock(other);
        _button.colors = isP1 ? _p1ColorBlock : _p2ColorBlock;
    }

    public void SetNum((int, bool) tile)
    {
        _num = tile.Item1;
        _isP1 = tile.Item2;
        if (_num != 0)
            _text.text = _num.ToString();

        _button.colors = _isP1 ? _p1ColorBlock : _p2ColorBlock;
    }

    ColorBlock GenerateColorBlock(float hue)
    {
        ColorBlock colorBlock = new ColorBlock();
        colorBlock.normalColor = Color.HSVToRGB(hue, 0.5f, 1);
        colorBlock.selectedColor = Color.HSVToRGB(hue, 0.25f, 1);
        colorBlock.pressedColor = Color.HSVToRGB(hue, 0.75f, 0.75f);
        colorBlock.disabledColor = Color.HSVToRGB(hue, 0.75f, 0.6f);
        colorBlock.highlightedColor = colorBlock.selectedColor;
        colorBlock.colorMultiplier = 1;
        return colorBlock;
    }
}

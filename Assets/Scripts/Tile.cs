using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Describes the properties of the tiles
public class Tile : MonoBehaviour
{
    [SerializeField] private Color _p1Color, _p2Color;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    public void Init(bool isP1)
    {
        _renderer.color = isP1 ? _p1Color : _p2Color;
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
        print("ON");
    }

    private void OnMouseExit() 
    {
        _highlight.SetActive(false);
    }
}

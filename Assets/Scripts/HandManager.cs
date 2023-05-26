using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    Tile _tilePrefab;
    GameManager _gameManager;
    public GridLayoutGroup _parent;

    bool _isP1;

    public List<Tile> _hand = new List<Tile>();

    public void Initialize(Tile tilePrefab, bool isP1, GameManager gameManager)
    {
        _tilePrefab = tilePrefab;
        _gameManager = gameManager;
        _isP1 = isP1;
    }

    public void ClearHand()
    {
        foreach (Tile tile in _hand)
        {
            Destroy(tile.gameObject);
        }
        _hand.Clear();
        _parent.transform.DetachChildren();
    }

    public void AddNumber(int number)
    {
        Tile tile = Instantiate(_tilePrefab);

        tile.Init(_isP1, (-1, -1));
        tile.SetNum((number, _isP1));
        tile._button.onClick.AddListener(() => { _gameManager.SetNumber(tile); });
        tile.transform.SetParent(_parent.transform, false);
        _hand.Add(tile);
    }

    // Removes the number at the given index and 
    public void RemoveNumber(Tile tile)
    {
        int idx = _hand.IndexOf(tile);
        _hand.RemoveAt(idx);
        tile.transform.SetParent(null);
        Destroy(tile);
    }

    // Enables or disables the tiles
    public void SetEnable(bool enable)
    {
        foreach (Tile tile in _hand)
        {
            tile._button.interactable = enable;
        }
    }

    // Set
    public void SetSize(float min, float max)
    {
        float shortEdge, longEdge, cellSize;
        
        RectTransform rect = transform.GetComponent<RectTransform>();
        if (rect.sizeDelta.x > rect.sizeDelta.y)
        {
            shortEdge = rect.sizeDelta.y;
            longEdge = rect.sizeDelta.x;
        }
        else
        {
            shortEdge = rect.sizeDelta.x;
            longEdge = rect.sizeDelta.y;
        }

        if (shortEdge < min)
            cellSize = min;
        else if (shortEdge > max)
            cellSize = max;
        else
            cellSize = shortEdge;

        float remaining = Math.Max(longEdge - cellSize * 4, 0);

        
        _parent.cellSize = Vector2.one * cellSize;
        _parent.spacing = Vector2.one * remaining / 5;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public Tile _tilePrefab;
    public GameManager _gameManager;
    public GameObject _parent;

    public bool _isP1;

    public List<Tile> _hand = new List<Tile>();

    public void ClearHand()
    {
        foreach (Tile tile in _hand)
        {
            GameObject.Destroy(tile.gameObject);
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
        GameObject.Destroy(tile);
    }

    // Enables or disables the tiles
    public void SetEnable(bool enable)
    {
        foreach (Tile tile in _hand)
        {
            tile._button.enabled = enable;
        }
    }
}

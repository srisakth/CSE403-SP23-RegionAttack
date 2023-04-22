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

    private List<Tile> _hand = new List<Tile>();

    public void ClearHand()
    {
        foreach (Tile tile in _hand)
        {
            GameObject.Destroy(tile.gameObject);
        }
        _hand.Clear();
    }

    public void AddNumber(int number)
    {
        Tile tile = Instantiate(_tilePrefab);

        tile.Init(_isP1, (-1, -1));
        tile.SetNum(_isP1, number);
        tile._button.onClick.AddListener(() => { _gameManager.SetNumber(tile); });
        tile.transform.SetParent(_parent.transform);
        _hand.Add(tile);
    }

    // Removes the number at the given index and 
    public void RemoveNumber(Tile tile)
    {
        int idx = _hand.IndexOf(tile);
        _hand.RemoveAt(idx);
        GameObject.Destroy(tile);

    }
}

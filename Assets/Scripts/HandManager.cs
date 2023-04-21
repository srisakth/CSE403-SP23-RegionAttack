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
    }

    public void AddNumber(int number)
    {
        Tile tile = Instantiate(_tilePrefab);

        tile.Init(_isP1, (_hand.Count, -1));
        tile.SetNum(_isP1, number);
        tile._button.onClick.AddListener(() => { _gameManager.SetNumber(number); });
        tile.transform.SetParent(_parent.transform);
        _hand.Add(tile);
    }

    // Removes the number at the given index and 
    public void RemoveNumber(int number)
    {
        int idx = _hand.FindIndex(tile => tile._num == number);
        Tile num = _hand[idx];
        _hand.RemoveAt(idx);
        GameObject.Destroy(num);

    }
}

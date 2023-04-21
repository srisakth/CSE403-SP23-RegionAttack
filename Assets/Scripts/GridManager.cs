using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/* 
 * The GridManager is responsible for displaying the game board on the given canvas.
 */
public class GridManager : MonoBehaviour
{
    // Board size
    public int _dimension;

    // The canvas to populate
    public GameObject _canvas;

    // The GameManager to send coordinate info to
    public GameManager _gameManager;

    // Prefabs for the tiles
    public Tile _tilePrefab;

    // Arbitrary constants
    // Smaller screen edge : board ratio
    float _boardRatio = 0.8f;
    // Cell : spacing ratio
    float _spaceRatio = 0.1f;

    // Reference to the tile GameObjects
    Tile[,] _tiles;


    private void Start()
    {
        // Initial sanity check statements
        // Does the given tile prefab contain Button and Tile?
        Debug.Assert(_tilePrefab.GetComponent<Button>() != null);
        Debug.Assert(_tilePrefab.GetComponent<Tile>() != null);

        // Do we have a canvas?
        Debug.Assert( _canvas != null );

        // Do we know the GameManager?
        Debug.Assert(_gameManager != null);
    }

    public void Initialize()
    {
        // Make the grid nice
        InitializeGrid();

        // Populate the grid
        PopulateGrid();
    }

    public void Clear()
    {
        if (_tiles == null)
            return;
        // Remove the tiles in case we choose a different grid
        foreach (Tile tile in _tiles)
        {
            GameObject.Destroy(tile.gameObject);
        }
    }

    // Sets the tile at the given position to hold the given number
    public void UpdateGrid(bool isP1Num, (int, int) position, int number)
    {
        if (IsInBound(position))
        {
            _tiles[position.Item1, position.Item2].SetNum(isP1Num, number);
        }
        else
            Console.Error.WriteLine("Invalid position");
    }

    // Helper function to determine whether the given position is valid
    bool IsInBound((int, int) position)
    {
        return position.Item1 >= 0 && position.Item1 < _dimension && position.Item2 >= 0 && position.Item2 < _dimension;
    }

    public void SetDimension(int option)
    {
        _dimension = GameManager.DimOptions[option];
    }

    // Helper function to return whether the grid at that coordinate is player 1's grid
    bool IsP1Side(int i, int j)
    {
        // For now, we can just set the upper half as P1's but we can eventually have different configurations
        return j < _dimension / 2;
    }


    // Helper function to scale and initialize the canvas for the tiles
    void InitializeGrid()
    {
        // Use the screen size to determine a nice square to use
        int width = Screen.width;
        int height = Screen.height;
        int size = width > height ? height : width;

        // We'll just arbitrarily scale it via the ratio
        size = Mathf.FloorToInt(size * _boardRatio);

        // Set the canvas to that size
        RectTransform rect = _canvas.GetComponent<RectTransform>();
        rect.sizeDelta = Vector2.one * size;
        rect.localPosition = Vector2.zero;

        GridLayoutGroup grid = _canvas.GetComponent<GridLayoutGroup>();

        // Calculate the cell size and space size
        // We have _size cells + _size-1 spaces for size much space
        int cellSize = Mathf.FloorToInt(size / ((1f + _spaceRatio) * _dimension - _spaceRatio));
        int spaceSize = Mathf.FloorToInt(cellSize * _spaceRatio);

        // Set the cell and spacing size
        grid.cellSize = Vector2.one * cellSize;
        grid.spacing = Vector2.one * spaceSize;

        // Since we want _size tiles for each column/row, set the constraint count
        grid.constraintCount = _dimension;
    }

    // Helper function to instantiate the tile prefabs
    void PopulateGrid()
    {
        // Initialize the tiles array
        _tiles = new Tile[_dimension, _dimension];

        for (int i = 0; i < _dimension; i++)
        {
            for (int j = 0; j < _dimension; j++)
            {
                bool isP1 = IsP1Side(i, j);
                _tiles[i, j] = Instantiate(_tilePrefab);
                _tiles[i, j].name = $"tile{i}-{j}";
                _tiles[i, j].transform.SetParent(_canvas.transform, false);
                _tiles[i, j]._button.onClick.AddListener(() => { _gameManager.SetPosition((i, j)); });
                _tiles[i, j].Init(isP1, (i, j));
            }
        }
    }
}

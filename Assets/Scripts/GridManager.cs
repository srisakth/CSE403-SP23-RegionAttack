using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/* 
 * The GridManager is responsible for displaying the game board on the given canvas.
 */
public class GridManager : MonoBehaviour
{
    // Board size
    public int _dimension = 6;

    // The GameManager to send coordinate info to
    public GameManager _gameManager;

    // Prefabs for the tiles
    public Tile _tilePrefab;

    // Cell : spacing ratio
    public static float _spaceRatio = 0.1f;

    // Reference to the tile GameObjects
    Tile[,] _tiles;

    // Given the dimension and the internal representation of the board,
    // extracts the necessary information and initializes the board
    public void Initialize(int dimension, (int, bool)[,] board)
    {
        _dimension = dimension;

        // Make the grid nice
        SetGridSize();

        // Populate the grid
        PopulateGrid(board);
    }

    public void Clear()
    {
        if (_tiles == null)
            return;
        // Remove the tiles
        foreach (Tile tile in _tiles)
        {
            GameObject.Destroy(tile.gameObject);
        }
    }

    // Updates the tiles to reflect the current board
    public void UpdateGrid((int, bool)[,] board)
    {
        for (int i = 0; i < _dimension; i++)
        {
            for (int j = 0; j < _dimension; j++)
            {
                _tiles[i, j].SetNum(board[i, j]);
            }
        }
    }

    // Helper function to scale the canvas, set up the grid, and set the font size of the tile
    // Returns the cell size to make it easier for the screen adjuster
    public float SetGridSize(float size)
    {
        // Set the canvas to that size
        RectTransform rect = transform.GetComponent<RectTransform>();
        rect.sizeDelta = Vector2.one * size;
        rect.anchoredPosition = Vector2.zero;

        GridLayoutGroup grid = transform.GetComponent<GridLayoutGroup>();

        // Calculate the cell size and space size
        // We have _size cells + _size-1 spaces for size much space
        float cellSize = size / ((1f + _spaceRatio) * _dimension - _spaceRatio);
        float spaceSize = cellSize * _spaceRatio;

        // Set the cell and spacing size
        grid.cellSize = Vector2.one * cellSize;
        grid.spacing = Vector2.one * spaceSize;

        // Since we want _size tiles for each column/row, set the constraint count
        grid.constraintCount = _dimension;

        // Set the text size to something a bit smaller than half the tile
        // IDK why, but this also sets the font size for the hand tiles so...
        _tilePrefab._text.fontSize = cellSize * 0.4f;

        return cellSize;
    }

    // Overload to use when starting the game
    public void SetGridSize()
    {
        // Set the canvas to that size
        RectTransform rect = transform.GetComponent<RectTransform>();
        float size = rect.sizeDelta.x;

        GridLayoutGroup grid = transform.GetComponent<GridLayoutGroup>();

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
    void PopulateGrid((int, bool)[,] board)
    {
        // Initialize the tiles array
        _tiles = new Tile[_dimension, _dimension];

        for (int i = 0; i < _dimension; i++)
        {
            for (int j = 0; j < _dimension; j++)
            {
                (int, int) pos = (i, j);
                bool isP1 = board[i, j].Item2;
                Tile tile = Instantiate(_tilePrefab);
                tile.name = $"tile{i}-{j}";
                tile.transform.SetParent(transform, false);
                tile._button.onClick.AddListener(() => { _gameManager.SetPosition(tile); });
                tile.Init(isP1, pos);
                _tiles[i, j] = tile;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    // Board size
    public int _size;

    // The canvas to populate
    public GameObject _canvas;
    // Prefabs for the tiles
    public Tile _p1TilePrefab, _p2TilePrefab;

    // Arbitrary constants
    // Smaller screen edge : board ratio
    float _boardRatio = 0.8f;
    // Cell : spacing ratio
    float _spaceRatio = 0.1f;


    private void Start()
    {
        // Make the grid nice
        InitializeGrid();

        // 
        PopulateGrid();
    }

    // Helper function to return whether the grid at that coordinate is player 1's grid
    bool IsP1Side(int i, int j)
    {
        // For now, we can just set the upper half as P1's but we can eventually have different configurations
        return j < _size / 2;
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
        int cellSize = Mathf.FloorToInt(size / ((1f + _spaceRatio) * _size - _spaceRatio));
        int spaceSize = Mathf.FloorToInt(cellSize * _spaceRatio);

        // Set the cell and spacing size
        grid.cellSize = Vector2.one * cellSize;
        grid.spacing = Vector2.one * spaceSize;

        // Since we want _size tiles for each column/row, set the constraint count
        grid.constraintCount = _size;
    }

    // Helper function to instantiate the tile prefabs
    void PopulateGrid()
    {
        Tile spawned;
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                bool isP1 = IsP1Side(i, j);
                spawned = isP1 ? Instantiate(_p1TilePrefab) : Instantiate(_p2TilePrefab);
                spawned.name = $"tile{i}-{j}";
                spawned.transform.SetParent(_canvas.transform, false);
                spawned.Init(isP1, i, j);
            }
        }
    }
}

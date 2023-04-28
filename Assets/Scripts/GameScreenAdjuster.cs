using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenAdjuster : MonoBehaviour
{
    public GameManager gameManager;
    public RectTransform _moveTimer, _gameTimer;

    private static float _gridRatio = 0.8f;
    private static float _timerRatio = 0.08f;

    public bool _isLandScape;

    private void Start()
    {
        //_isLandScape = Screen.width > Screen.height;
        //ChangePosition();
        //Adjust();
    }

    private void OnRectTransformDimensionsChange()
    {
        if (_isLandScape != (Screen.width > Screen.height))
        {
            _isLandScape = Screen.width > Screen.height;
            ChangePosition();
        }
            
        Adjust();
    }

    void ChangePosition()
    {
        //print("YUP");
        float gridSize;
        if (Screen.width > Screen.height)
        {
            // Landscape
            gridSize = Screen.height * _gridRatio;

            // Set P1's position to the left
            RectTransform rect = gameManager._p1Hand.transform.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0, 0.5f);
        }
        else
        {
            // Set P1's position to the top
            RectTransform rect = gameManager._p1Hand.transform.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0.5f, 0); // pivot on bottom (since rotated)
            rect.anchorMax = new Vector2(0.5f, 1); // anker on top
            rect.anchorMin = new Vector2(0.5f, 1);
            rect.rotation = Quaternion.Euler(0, 0, 180);  // Flip it

            // Set P2's position to the bottom
            rect = gameManager._p2Hand.transform.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0.5f, 0);  // pivot on bottom
            rect.anchorMax = new Vector2(0.5f, 0); // Anker to the bottom of screen
            rect.anchorMin = new Vector2(0.5f, 0);
            rect.rotation = Quaternion.identity;

            // Set the size and position of the timers
            _moveTimer.pivot = Vector2.one * 0.5f;  // Pivot on center
            _moveTimer.anchorMax = new Vector2(0, 0.5f); // anker on left
            _moveTimer.anchorMin = new Vector2(0, 0.5f);

            _gameTimer.pivot = Vector2.one * 0.5f;  // Pivot on center
            _gameTimer.anchorMax = new Vector2(1, 0.5f); // anker on right
            _gameTimer.anchorMin = new Vector2(1, 0.5f);
            _gameTimer.rotation = Quaternion.Euler(0, 0, -90);  // rotate it clockwise
        }
    }

    void Adjust()
    {
        float gridSize, height;
        if (Screen.width > Screen.height)
        {
            // Landscape
            gridSize = Screen.height * _gridRatio;

            // Set P1's position to the left
            RectTransform rect = gameManager._p1Hand.transform.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0, 0.5f);
        }
        else
        {
            // Vertical
            //print($"{Screen.width} x {Screen.height}");
            gridSize = Screen.width * _gridRatio;
            height = (Screen.height - gridSize) / 6;

            //print($"Height: {height}");
            // Adjust the size
            RectTransform rect = gameManager._p1Hand.transform.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(gridSize, height);
            rect.anchoredPosition = new Vector3(0, -height, 0);

            rect = gameManager._p2Hand.transform.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(gridSize, height);
            rect.anchoredPosition = new Vector3(0, height, 0);

            _gameTimer.sizeDelta = new Vector2(gridSize / 2, Screen.width * _timerRatio);
            _gameTimer.anchoredPosition = new Vector3(-Screen.width * (1 - _gridRatio)/4, 0, 0);
            _moveTimer.sizeDelta = Vector2.one * Screen.width * _timerRatio;
            _moveTimer.anchoredPosition = new Vector3(Screen.width * (1 - _gridRatio)/4, 0, 0);
        }

        float cellSize = gameManager._gridManager.SetGridSize(gridSize);
        float spaceSize = cellSize * GridManager._spaceRatio;

        GridLayoutGroup grid = gameManager._p1Hand._parent.GetComponent<GridLayoutGroup>();
        grid.cellSize = Vector2.one * cellSize;
        grid.spacing = Vector2.one * spaceSize;

        grid = gameManager._p2Hand._parent.GetComponent<GridLayoutGroup>();
        grid.cellSize = Vector2.one * cellSize;
        grid.spacing = Vector2.one * spaceSize;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GameScreenAdjuster : MonoBehaviour
{
    public GameManager _gameManager;
    public RectTransform _moveTimer, _gameTimer;

    private static float _gridRatio = 0.8f;
    private static float _timerRatio = 0.08f;


    private void OnRectTransformDimensionsChange()
    {
        ChangePosition();
        Adjust();
    }

    void ChangePosition()
    {
        if (Screen.width > Screen.height)
        {
            // Landscape
            // Set P1's position to the right
            RectTransform rect = _gameManager._p1Hand.transform.GetComponent<RectTransform>();
            rect.pivot = new Vector2(1, 0.5f);  // pivot on right
            rect.anchorMax = new Vector2(1, 0.5f); // Anker to the right of screen
            rect.anchorMin = new Vector2(1, 0.5f);
            rect.rotation = Quaternion.identity;

            // Set P2's position to the left
            rect = _gameManager._p2Hand.transform.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0, 0.5f); // pivot on left
            rect.anchorMax = new Vector2(0, 0.5f); // anker on left
            rect.anchorMin = new Vector2(0, 0.5f);
            rect.rotation = Quaternion.identity;

            // Set the size and position of the timers
            _moveTimer.pivot = Vector2.one * 0.5f;  // Pivot on center
            _moveTimer.anchorMax = new Vector2(0.5f, 0); // anker on bottom
            _moveTimer.anchorMin = new Vector2(0.5f, 0);

            _gameTimer.pivot = Vector2.one * 0.5f;  // Pivot on center
            _gameTimer.anchorMax = new Vector2(0.5f, 1); // anker on top
            _gameTimer.anchorMin = new Vector2(0.5f, 1);
            _gameTimer.rotation = Quaternion.identity;  // rotate it upright
        }
        else
        {
            // Set P1's position to the bottom
            RectTransform rect = _gameManager._p1Hand.transform.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0.5f, 0);  // pivot on bottom
            rect.anchorMax = new Vector2(0.5f, 0); // Anker to the bottom of screen
            rect.anchorMin = new Vector2(0.5f, 0);
            rect.rotation = Quaternion.identity;

            // Set P2's position to the top
            rect = _gameManager._p2Hand.transform.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0.5f, 0); // pivot on bottom (since rotated)
            rect.anchorMax = new Vector2(0.5f, 1); // anker on top
            rect.anchorMin = new Vector2(0.5f, 1);
            rect.rotation = Quaternion.Euler(0, 0, 180);  // Flip it

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
        float gridSize, handShort;
        if (Screen.width > Screen.height)
        {
            // Landscape
            gridSize = Screen.height * _gridRatio;
            handShort = (Screen.width - gridSize) / 6;

            RectTransform rect = _gameManager._p1Hand.transform.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(handShort, gridSize);
            rect.anchoredPosition = new Vector3(-handShort, 0, 0);

            rect = _gameManager._p2Hand.transform.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(handShort, gridSize);
            rect.anchoredPosition = new Vector3(handShort, 0, 0);

            _gameTimer.sizeDelta = new Vector2(gridSize / 2, Screen.height * _timerRatio);
            _gameTimer.anchoredPosition = new Vector3(0, -Screen.height * (1 - _gridRatio) / 4, 0);
            _moveTimer.sizeDelta = Vector2.one * Screen.height * _timerRatio;
            _moveTimer.anchoredPosition = new Vector3(0, Screen.height * (1 - _gridRatio) / 4, 0);
        }
        else
        {
            // Vertical
            //print($"{Screen.width} x {Screen.height}");
            gridSize = Screen.width * _gridRatio;
            handShort = (Screen.height - gridSize) / 6;

            //print($"Height: {height}");
            // Adjust the size
            RectTransform rect = _gameManager._p1Hand.transform.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(gridSize, handShort);
            rect.anchoredPosition = new Vector3(0, handShort, 0);

            rect = _gameManager._p2Hand.transform.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(gridSize, handShort);
            rect.anchoredPosition = new Vector3(0, -handShort, 0);


            _gameTimer.sizeDelta = new Vector2(gridSize / 2, Screen.width * _timerRatio);
            _gameTimer.anchoredPosition = new Vector3(-Screen.width * (1 - _gridRatio)/4, 0, 0);
            _moveTimer.sizeDelta = Vector2.one * Screen.width * _timerRatio;
            _moveTimer.anchoredPosition = new Vector3(Screen.width * (1 - _gridRatio)/4, 0, 0);
        }
        // Adjust the cell sizes
        float cellSize = _gameManager._gridManager.SetGridSize(gridSize);

        _gameManager._p1Hand.SetSize(cellSize * 0.4f, cellSize);
        _gameManager._p2Hand.SetSize(cellSize * 0.4f, cellSize);

        // Text box sizes
        RectTransform message = _gameManager._tutorial._popUp._container.transform.GetComponent<RectTransform>();
        message.sizeDelta = new Vector2(gridSize, gridSize * 2 / 3);
        message = _gameManager._popup._container.transform.GetComponent<RectTransform>();
        message.sizeDelta = new Vector2(gridSize, gridSize * 2 / 3);
    }
}

using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int _size;
    public float _space;

    public Transform _camera;
    public Canvas _canvas;

    public GameObject _p1TilePrefab, _p2TilePrefab;


    private void Start()
    {
        InitializeGrid();

        // Move the camera to the center
        _camera.transform.position = new Vector3((float)(_size - 1) * (_space + 1) / 2, (float)(_size - 1) * (_space + 1) / 2, -10);
    }


    void InitializeGrid()
    {
        GameObject spawned;
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                if (j >= _size/2)
                {
                    spawned = Instantiate(_p1TilePrefab, new Vector3((_space + 1) * i, (_space + 1) * j), Quaternion.identity);
                } else
                {
                    spawned = Instantiate(_p2TilePrefab, new Vector3((_space + 1) * i, (_space + 1) * j), Quaternion.identity);
                }
                spawned.transform.localScale = Vector3.one;
                spawned.name = $"tile{i}-{j}";
                spawned.transform.parent = _canvas.transform;
            }
        }

        
    }
}

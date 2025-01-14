using UnityEngine;

public class GridSystem : MonoBehaviour {
    [SerializeField] Vector3 _origins;
    [SerializeField] Vector2Int _gridSize;
    [SerializeField] float _cellSize;
    [SerializeField] Cell[,] _cells;



    public GameObject _cellObject;
    void Start() {
        InitializeGrid();
    }
    void InitializeGrid() {
        _cells = new Cell[_gridSize.x, _gridSize.y];
        for (int x = 0; x < _gridSize.x; x++) {
            for (int y = 0; y < _gridSize.y; y++) {
                Vector3 wPos = new(_origins.x + x * _cellSize, _origins.y + Random.Range(-0.5f, 0.5f), _origins.z + y * _cellSize);
                Cell cell = new() {
                    _gridPos = new(x, y),
                    _worldPos = wPos,
                    _ID = (uint)(x * _gridSize.x * _gridSize.y + y)
                };
                _cells[x, y] = cell;
            }
        }
    }
    void Update() {

    }
}

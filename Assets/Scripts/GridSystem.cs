using UnityEngine;

public class GridSystem : MonoBehaviour {
    [SerializeField] Vector3 _origins;
    [SerializeField] Vector2Int _gridSize;
    [SerializeField] float _cellSize;
    [SerializeField] Cell[,] _cells;
    [SerializeField] bool centerToOrigin;

    public static GridSystem _I;
    public GameObject _cellObject;
    public Material[] mats;

    void Awake() {
        _I = this;
    }
    void Start() {

        InitializeGrid();
    }
    void InitializeGrid() {
        if (centerToOrigin) _origins = new Vector3(_origins.x - _gridSize.x / 2, _origins.y, _origins.z - _gridSize.y / 2);
        _cells = new Cell[_gridSize.x, _gridSize.y];
        for (int x = 0; x < _gridSize.x; x++) {
            for (int y = 0; y < _gridSize.y; y++) {
                Vector3 wPos = new(_origins.x + x * _cellSize, _origins.y, _origins.z + y * _cellSize);
                Cell cell = new() {
                    _gridPos = new(x, y),
                    _worldPos = wPos,
                    _ID = (uint)(x * _gridSize.x * _gridSize.y + y),
                };
                _cells[x, y] = cell;

                // DEBUG OPTIONS:

                // Instantiate(_cellObject, wPos, Quaternion.identity);
                // cell._content.GetComponent<MeshRenderer>().material = mats[Random.Range(0, mats.Length)];
                // Debug.DrawLine(wPos + new Vector3(-_cellSize / 2, 2f, -_cellSize / 2), wPos + new Vector3(_cellSize / 2, 2f, _cellSize / 2), Color.black, 100f);
                // Debug.DrawLine(wPos + new Vector3(-_cellSize / 2, 2f, _cellSize / 2), wPos + new Vector3(_cellSize / 2, 2f, -_cellSize / 2), Color.black, 100f);
            }
        }
    }
    public Cell GetGridCell(Vector3 pos) {
        int x = Mathf.FloorToInt((pos.x - _origins.x) / _cellSize + _cellSize / 2);
        int y = Mathf.FloorToInt((pos.z - _origins.z) / _cellSize + _cellSize / 2);
        return _cells[x, y];
    }
    void Update() {

    }
}

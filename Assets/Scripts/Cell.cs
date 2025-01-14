using UnityEngine;

public class Cell {
    public Vector3 _worldPos;
    public Vector3 _gridPos;
    public uint _ID;
    public bool _isWalkable = true;
    public bool _isEmpty = true;
    public GameObject _content;
}

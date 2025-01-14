using Unity.VisualScripting;
using UnityEngine;

public class MouseController : MonoBehaviour {
    public LayerMask _targetLayer;
    public GameObject[] _trees;
    public GameObject _turret;
    public Transform _treeContainer;
    public ClickMode _clickMode;
    void Update() {
        if (Input.GetMouseButtonDown(0)) OnLeftClick();
        if (Input.GetKeyDown(KeyCode.Comma)) {
            _clickMode += 1;
            if (_clickMode > ClickMode.deleteContent) _clickMode = 0;
        } else if (Input.GetKeyDown(KeyCode.Period)) {
            _clickMode -= 1;
            if (_clickMode < 0) _clickMode = ClickMode.deleteContent;
        }
    }
    public void OnLeftClick() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _targetLayer)) {

            Cell cell = GridSystem._I.GetGridCell(hit.point);
            Debug.Log($"Clicked on cell with pos {cell._worldPos}, when cursor was on {hit.transform.position}");

            switch (_clickMode) {

                case ClickMode.placeTree: {
                        if (cell._isEmpty) {
                            var t = Instantiate(_trees[Random.Range(0, _trees.Length)], cell._worldPos, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                            t.transform.SetParent(_treeContainer, true);
                            cell._content = t;
                            cell._isEmpty = false;
                        }
                        break;
                    }
                case ClickMode.placeTurret: {
                        if (cell._isEmpty) {
                            var t = Instantiate(_turret, cell._worldPos, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                            cell._content = t;
                            t.transform.SetParent(_treeContainer, true);
                            cell._isEmpty = false;
                        }
                        break;
                    }
                case ClickMode.deleteContent: {
                        if (!cell._isEmpty) {
                            Destroy(cell._content);
                            cell._isEmpty = true;
                            cell._content = null;
                        }
                        break;
                    }
                case ClickMode.doNothing: {
                        Debug.Log("Do nothing click mode is active");
                        break;
                    }

                default: break;
            }

        }
    }
}
public enum ClickMode {
    doNothing,
    placeTree,
    placeTurret,
    deleteContent,

}

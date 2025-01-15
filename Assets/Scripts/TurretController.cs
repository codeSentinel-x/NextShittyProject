using UnityEngine;

public class TurretController : MonoBehaviour {
    public Transform _turretHead;
    public Transform _firePoint;
    public GameObject _bullet;

    public float _range;
    public float _rotSpeed;
    public int _fov;
    public string _targetTag;
    public float _turretH;

    public bool _targetInRange;
    public GameObject _target;
    public float _shootDelay;
    private float _nextShootTime;

    void Awake() {

    }


    void Update() {
        CastRay();
        Rotate();
        if (_targetInRange) Shoot();
    }

    public void Shoot() {
        if (_nextShootTime < Time.time) {


            var b = Instantiate(_bullet, _firePoint.position, Quaternion.identity);
            _nextShootTime = Time.time + _shootDelay;
            b.transform.GetChild(0).rotation = _turretHead.rotation * Quaternion.Euler(new Vector3(0, 180, 0));
            b.GetComponentInChildren<Bullet>()._target = _target.transform;
        }
    }
    float _animRot = 120;
    public float _animationSpeed;
    public void Rotate() {
        if (_targetInRange) {
            Vector3 direction = (_target.transform.position - _turretHead.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation *= Quaternion.Euler(new Vector3(0, 180, 0));
            _turretHead.rotation = Quaternion.Slerp(_turretHead.rotation, targetRotation, _rotSpeed * Time.deltaTime);

        } else {

            var tr = Quaternion.Euler(new Vector3(0, _animRot));
            _turretHead.rotation = Quaternion.Slerp(_turretHead.rotation, tr, _animationSpeed * Time.deltaTime);
            if ((Quaternion.Angle(_turretHead.rotation, tr) < 1f)) {
                _animRot = _animRot == 120 ? 240 : 120;
            }
        }
    }
    public void CastRay() {

        var turretHeadPos = _turretHead.position + new Vector3(0, _turretH, 0);
        float hHalfFov = _fov / 2; // Horizontal Half-Field of View (Degrees)
        float vHalfFov = _fov / 2; // Vertical Half-Field of View (Degrees)

        // Find all colliders within ViewRange
        var hits = Physics.OverlapSphere(turretHeadPos, _range);
        bool findTarget = false;
        foreach (var hit in hits) {

            if (!hit.transform.gameObject.CompareTag(_targetTag)) continue;
            if (hit.gameObject == _turretHead.gameObject) continue; // don't hit self

            // Check FOV
            var direction = turretHeadPos - hit.transform.position;
            var hDir = Vector3.ProjectOnPlane(direction, transform.up).normalized; // Project onto transform-relative XY plane to check hFov
            var vDir = Vector3.ProjectOnPlane(direction, transform.right).normalized; // Project onto transform-relative YZ plane to check vFov

            var hOffset = Vector3.Dot(hDir, -_turretHead.forward) * Mathf.Rad2Deg; // Calculate horizontal angular offset in Degrees
            var vOffset = Vector3.Dot(vDir, -_turretHead.forward) * Mathf.Rad2Deg; // Calculate vertical angular offset in Degrees

            if (hOffset > hHalfFov || vOffset > vHalfFov) continue; // Outside of FOV

            Debug.DrawLine(turretHeadPos, hit.transform.position, Color.red);

            if (hit.transform.gameObject.CompareTag(_targetTag)) {
                Debug.Log(hit.transform.name);
                _targetInRange = true;
                _target = hit.gameObject;
                findTarget = true;
                break;
            }
        }
        if (!findTarget) {
            _target = null;
            _targetInRange = false;
        }
    }





}

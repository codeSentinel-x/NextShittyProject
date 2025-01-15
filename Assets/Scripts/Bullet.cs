using UnityEngine;

public class Bullet : MonoBehaviour {
    public float _bulletSpeed;
    public Transform _target;
    public float _damage;
    public float _maxDist;
    public float _dirChangeSpeed;
    private float _dist;


    void Update() {

        transform.localPosition += transform.forward * _bulletSpeed * Time.deltaTime;

        Vector3 direction = (_target.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, targetRotation, _dirChangeSpeed * Time.deltaTime);

        _dist += _bulletSpeed * Time.deltaTime;
        if (_dist > _maxDist) Destroy(transform.parent.gameObject);
    }
    void OnCollisionEnter(Collision other) {
        Destroy(transform.parent.gameObject);
    }
}

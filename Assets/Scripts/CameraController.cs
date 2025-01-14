using UnityEngine;

public class CameraController : MonoBehaviour {
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;

    private float pitch = 0f;  // Vertical camera rotation
    private float yaw = 0f;    // Horizontal camera rotation

    void Update() {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move;

        yaw += Input.GetAxis("Mouse X") * lookSpeed;
        pitch -= Input.GetAxis("Mouse Y") * lookSpeed;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}

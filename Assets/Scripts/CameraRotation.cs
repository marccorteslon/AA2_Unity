using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [Header("Ratón")]
    public float mouseSensitivity = 100f; // Sensibilidad
    public float maxLookAngle = 80f;      // Límites
    public float minLookAngle = -80f;     // Límites

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotación vertical
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookAngle, maxLookAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}

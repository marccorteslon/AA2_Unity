using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [Header("Ratón")]
    public float mouseSensitivity = 100f; // Sensibilidad configurable desde inspector

    void Update()
    {
        RotatePlayer();
    }

    void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Rotación horizontal del jugador
        transform.Rotate(Vector3.up * mouseX);
    }
}

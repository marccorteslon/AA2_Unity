using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float pickupRange = 2f;            // Distancia m�xima para coger objetos
    public float holdDistance = 1.5f;         // Distancia delante de la c�mara donde se mantiene el objeto
    public LayerMask pickupLayer;             // Asignar la capa "Pickup" en el Inspector

    private Camera playerCamera;
    private Rigidbody objectPicked;
    private Transform holdPoint;

    void Start()
    {
        playerCamera = Camera.main;

        // Crear un punto delante de la c�mara donde se mantendr�n los objetos cogidos
        holdPoint = new GameObject("HoldPoint").transform;
        holdPoint.SetParent(playerCamera.transform);
        holdPoint.localPosition = new Vector3(0, 0, holdDistance);
    }

    void Update()
    {
        // Si el jugador mantiene click derecho
        if (Input.GetMouseButton(1))
        {
            // Si no hay objeto cogido, intenta coger uno
            if (objectPicked == null)
            {
                TryPickupObject();
            }
            else
            {
                // Mantener el objeto centrado
                HoldObject();
            }
        }
        else
        {
            // Si suelta el click derecho y tiene objeto cogido, soltarlo
            if (objectPicked != null)
            {
                DropObject();
            }
        }
    }

    void TryPickupObject()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb != null)
            {
                objectPicked = rb;
                objectPicked.useGravity = false;
                objectPicked.linearDamping = 10f;  // Suaviza el movimiento
            }
        }
    }

    void HoldObject()
    {
        // Mover el objeto suavemente hacia el punto de sujeci�n
        Vector3 desiredPosition = holdPoint.position;
        Vector3 moveDirection = desiredPosition - objectPicked.position;
        objectPicked.linearVelocity = moveDirection * 10f;
    }

    void DropObject()
    {
        objectPicked.useGravity = true;
        objectPicked.linearDamping = 0f;
        objectPicked = null;
    }
}

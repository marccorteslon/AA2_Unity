using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float jumpForce = 5f;

    [Header("Detección de suelo")]
    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;

    private InputSystem_Actions inputs;

    private void Awake()
    {
        inputs = new InputSystem_Actions();
    }
    void Start()
    {
        inputs.Enable();
}

    void Update()
    {
        Vector3 dir = inputs.Player.Move.ReadValue<Vector2>();
        dir.z = dir.y;
        dir.y = 0;

        dir = transform.TransformDirection(dir);

        transform.position += transform.forward * dir.z * speed * Time.deltaTime;
        transform.position += transform.right * dir.x * speed * Time.deltaTime;

        // Detectar si está en el suelo
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);

        // Mensaje en consola cuando puede saltar
        if (isGrounded)
        {
            Debug.Log("El jugador puede saltar");
        }

        // Saltar
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Limpiar la velocidad vertical antes de saltar
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Raycast visible
        Debug.DrawRay(groundCheck.position, Vector3.down * groundDistance, isGrounded ? Color.green : Color.red);

    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundDistance);
        }
    }


    // Código antiguo

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    // Bloquear rotaciones para que el personaje no se tumbe
    //    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    //}

    //void Update()
    //{
    //    // Movimiento horizontal
    //    float moveX = Input.GetAxis("Horizontal");
    //    float moveZ = Input.GetAxis("Vertical");
    //    Vector3 move = transform.right * moveX + transform.forward * moveZ;
    //    Vector3 velocity = new Vector3(move.x * speed, rb.linearVelocity.y, move.z * speed);
    //    rb.linearVelocity = velocity;

    //    // Detectar si está en el suelo
    //    isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);

    //    // Mensaje en consola cuando puede saltar
    //    if (isGrounded)
    //    {
    //        Debug.Log("¡El jugador puede saltar!");
    //    }

    //    // Saltar
    //    if (Input.GetButtonDown("Jump") && isGrounded)
    //    {
    //        // Limpiar la velocidad vertical antes de saltar
    //        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
    //        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //    }

    //    // Raycast visible
    //    Debug.DrawRay(groundCheck.position, Vector3.down * groundDistance, isGrounded ? Color.green : Color.red);
    //}

    //void OnDrawGizmos()
    //{
    //    if (groundCheck != null)
    //    {
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundDistance);
    //    }
    //}
}



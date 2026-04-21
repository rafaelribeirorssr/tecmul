using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Motor do Hacker (Velocidades)")]
    public float playerSpeed = 10f;
    public float acceleration = 0.7f;
    public float maxSpeed = 100f;

    [Header("Sistema de Pistas (Lanes)")]
    private int desiredLane = 1;
    public float laneDistance = 3f;
    public float sideSpeed = 100f;

    [Header("Mecânica de Salto")]
    public float jumpForce = 10f;
    private Rigidbody rb;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (playerSpeed < maxSpeed)
        {
            playerSpeed += acceleration * Time.deltaTime;
        }

        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);

        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                desiredLane++;
                if (desiredLane == 3) desiredLane = 2;
            }

            if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                desiredLane--;
                if (desiredLane == -1) desiredLane = 0;
            }

            if ((Keyboard.current.spaceKey.wasPressedThisFrame ||
                Keyboard.current.wKey.wasPressedThisFrame ||
                Keyboard.current.upArrowKey.wasPressedThisFrame) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        Vector3 targetPosition = transform.position;

        if (desiredLane == 0) targetPosition.x = -laneDistance;
        else if (desiredLane == 1) targetPosition.x = 0;
        else if (desiredLane == 2) targetPosition.x = laneDistance;

        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, targetPosition.x, sideSpeed * Time.deltaTime),
            transform.position.y,
            transform.position.z
        );
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = true;
        }
    }
}
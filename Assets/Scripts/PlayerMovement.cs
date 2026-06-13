using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;

    [Header("Input")]
    [SerializeField] private InputActionReference moveActionReference;

    public Rigidbody2D rb;
    private Vector2 movementInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void OnEnable()
    {
        // ต้องเรียกผ่าน .action 
        if (moveActionReference != null)
        {
            moveActionReference.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (moveActionReference != null)
        {
            moveActionReference.action.Disable();
        }
    }

    private void Update()
    {
        if (moveActionReference != null)
        {
            movementInput = moveActionReference.action.ReadValue<Vector2>();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movementInput.normalized * speed;
    }
}

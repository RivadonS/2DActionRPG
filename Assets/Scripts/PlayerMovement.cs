using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    private int facingDirection = 1; // 1 for right, -1 for left

    [Header("Input & Animation")]
    [SerializeField] private InputActionReference moveActionReference;
    [SerializeField] private Animator anim;

    public Rigidbody2D rb;
    private Vector2 movementInput;

    private readonly int animHorizontal = Animator.StringToHash("horizontal");
    private readonly int animVertical = Animator.StringToHash("vertical");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        if (anim == null) anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
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

        UpdateAnimation();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movementInput.normalized * speed;

        CheckFlip();
    }

    private void UpdateAnimation()
    {
        anim.SetFloat(animHorizontal, Mathf.Abs(movementInput.x));
        anim.SetFloat(animVertical, Mathf.Abs(movementInput.y));
    }

    private void CheckFlip()
    {
        if (movementInput.x > 0 && transform.localScale.x < 0 ||
            movementInput.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}

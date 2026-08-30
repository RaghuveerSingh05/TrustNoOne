using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip jumpSound;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("References")]
    public SpriteRenderer spriteRenderer;
    public Transform leftPupil;
    public Transform rightPupil;

    [Header("Pupil Settings")]
    public float leftPupilDefaultX = 0.3043505f;
    public float rightPupilDefaultX = 0.3020331f;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private float moveInput = 0f;
    private bool jumpPressed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpPressed = true;

            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }

        moveInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            moveInput = -1f;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            moveInput = 1f;

        if (spriteRenderer != null)
        {
            if (moveInput < 0)
                spriteRenderer.flipX = true;
            else if (moveInput > 0)
                spriteRenderer.flipX = false;
        }

        UpdatePupils();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            isGrounded = false;
        }

        jumpPressed = false;

        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb.linearVelocity.y
        );
    }

    private void UpdatePupils()
    {
        if (leftPupil == null || rightPupil == null)
            return;

        float currentLeftX = leftPupilDefaultX;
        float currentRightX = rightPupilDefaultX;

        if (moveInput < 0)
        {
            currentLeftX = -leftPupilDefaultX;
            currentRightX = -rightPupilDefaultX;
        }

        Vector3 leftPos = leftPupil.localPosition;
        leftPos.x = currentLeftX;
        leftPupil.localPosition = leftPos;

        Vector3 rightPos = rightPupil.localPosition;
        rightPos.x = currentRightX;
        rightPupil.localPosition = rightPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(
                groundCheck.position,
                groundCheckRadius
            );
        }
    }

    public bool IsGrounded() => isGrounded;
}
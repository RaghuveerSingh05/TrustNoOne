using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    [Header("Climbing")]
    [SerializeField] private float climbSpeed = 4f;

    private Rigidbody2D rb;

    private bool isOnLadder;
    private float verticalInput;

    private float originalGravityScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
    }

    private void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        if (isOnLadder)
        {
            
            rb.gravityScale = 0f;

            // Move up/down the ladder
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                verticalInput * climbSpeed
            );
        }
    }

    private void FixedUpdate()
    {
        if (!isOnLadder)
            return;

        
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            verticalInput * climbSpeed
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = false;
            rb.gravityScale = originalGravityScale;
        }
    }
}
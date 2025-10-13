using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
    public float moveSpeed = 8f;
    public float jumpForce = 14f;
    private float moveInput;

    // Ground checking
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask whatIsGround;
    private bool isGrounded;

    private Rigidbody2D player;
    private bool facingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // Flip sprite direction
        if (facingRight == false && moveInput > 0)
            Flip();
        else if (facingRight == true && moveInput < 0)
            Flip();

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            player.linearVelocity = new Vector2(player.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Move
        player.linearVelocity = new Vector2(moveInput * moveSpeed, player.linearVelocity.y);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.y *= -1;
        transform.localScale = Scaler;
    }
}

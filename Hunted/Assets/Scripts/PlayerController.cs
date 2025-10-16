using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Movement
    private float baseSpeed = 4f;
    public float moveSpeed;
    public float jumpForce = 7f;
    private float moveInput;
    private bool canJump = true;

    // Ground checking
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask whatIsGround;
    private bool isGrounded;

    private Rigidbody2D player;
    private bool facingRight = true;

    // Health
    private float maxHealth = 10f;
    public float currentHealth;
    public float healthRestore = 3f;

    // Weapon Slot - TODO
    public GameObject equippedWeapon;

    // Rage Effects
    public bool isRaging = false;
    public float rageSpeedMult = 2f;
    public float rageDuration = 10f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        moveSpeed = baseSpeed;
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

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            canJump = true;
        }

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded && canJump)
        {
            player.linearVelocity = new Vector2(player.linearVelocity.x, jumpForce);
            canJump = false;
        }

        // Move
        player.linearVelocity = new Vector2(moveInput * moveSpeed, player.linearVelocity.y);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded) 
        { 
            canJump = true; 
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.y *= -1;
        transform.localScale = Scaler;
    }

    // activates rage and then applies boosts in update
    public void ActivateRage()
    {
        if (!isRaging)
            StartCoroutine(RageRoutine());
    }

    private IEnumerator RageRoutine()
    {
        isRaging = true;

        // Apply rage effects
        moveSpeed = baseSpeed * rageSpeedMult;
        currentHealth = Mathf.Min(currentHealth + healthRestore, maxHealth);

        // Rage duration
        float timer = 0f;
        while (timer < rageDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // Reset effects
        moveSpeed = baseSpeed;
        isRaging = false;
    }
}
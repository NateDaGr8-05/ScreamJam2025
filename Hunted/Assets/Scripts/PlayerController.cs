using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Movement
    private float baseSpeed = 4f;
    public float moveSpeed;
    public float jumpForce = 7f;
    private float moveInput;

    // Ground checking
    //public Transform groundCheck;
    //public float checkRadius = 0.2f;
    //public LayerMask whatIsGround;
    private bool isGrounded = false;

    private Rigidbody2D player;
    private bool facingRight = true;

    // Health
    private float maxHealth = 10f;
    public float currentHealth;
    public float healthRestore = 3f;

    // Weapon Slot 
    public Transform weaponHolder;  //empty child
    public Weapon equippedWeapon;   //weapon prefab
    private Weapon equippedWeaponInstance;

    // Rage Effects
    public bool isRaging = false;
    public float rageSpeedMult = 2f;
    public float rageDuration = 10f;

    // Animation
    Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        moveSpeed = baseSpeed;

        EquipWeapon(equippedWeapon);

        //Get Animator component
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // Check if grounded
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        //Debug.Log(isGrounded);

        // Flip sprite direction
        if (!facingRight && moveInput > 0)
            Flip();
        else if (facingRight && moveInput < 0)
            Flip();

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            player.linearVelocity = new Vector2(player.linearVelocity.x, jumpForce);

            // Start jump animation
            animator.SetBool("isJumping", true);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Debug.Log("Landed on ground");
                isGrounded = true;
                // End the jump animation
                animator.SetBool("isJumping", false);
            }
        }

        // Move
        player.linearVelocity = new Vector2(moveInput * moveSpeed, player.linearVelocity.y);

        //Attack
        HandleAttack();

        // More Animation
        float move = Input.GetAxisRaw("Horizontal");
        animator.SetBool("isWalking", move != 0);

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }


    void HandleAttack()
    {
        // Left mouse to attack
        if (Input.GetButtonDown("Fire1") && equippedWeaponInstance != null)
        {
            Debug.Log("I am attacking");
            equippedWeaponInstance.Attack();
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
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

    public void EquipWeapon(Weapon newWeapon)
    {
        if (newWeapon != null)
        {
            // Instantiate weapon as child of weaponHolder
            equippedWeaponInstance = Instantiate(newWeapon, weaponHolder.position, weaponHolder.rotation, weaponHolder);
            equippedWeaponInstance.playerTransform = transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            currentHealth -= 1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
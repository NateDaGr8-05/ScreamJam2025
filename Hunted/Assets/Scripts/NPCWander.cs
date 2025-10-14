using UnityEngine;

public class NPCWander : MonoBehaviour
{
    //Movement presets - adjust in unity
    public float moveSpeed = 1f;
    public float moveTime = 2f;
    public float waitTime = 1.5f;

    private Rigidbody2D npc;
    private Vector2 moveDirection;
    private float moveTimer;
    private float waitTimer;
    private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npc = GetComponent<Rigidbody2D>();
        waitTimer = waitTime;
        ChooseNewDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // Moves for specific set time
            moveTimer -= Time.deltaTime;
            npc.linearVelocity = new Vector2(moveDirection.x * moveSpeed, npc.linearVelocity.y);    // Keeps gravity but still moves
            if (moveTimer <= 0)
            {
                // Stop and wait for a sec
                isMoving = false;
                npc.linearVelocity = Vector2.zero;
                waitTimer = waitTime;
            }
        }
        else
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0)
            {
                ChooseNewDirection();
            }
        }
        
    }

    void ChooseNewDirection()
    {
        // Pick a random direction, left or right
        // Pick only left (-1) or right (+1)
        float randomDir = Random.value < 0.5f ? -1f : 1f;
        moveDirection = new Vector2(randomDir, 0f);

        moveTimer = moveTime;
        isMoving = true;

        // Flip sprite
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * randomDir;
        transform.localScale = scale;
    }
}

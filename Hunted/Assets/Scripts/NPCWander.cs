using UnityEngine;

public class NPCWander : MonoBehaviour
{
    // Movement presets - adjust in unity
    public float moveSpeed = 1f;
    public float moveTime = 2f;
    public float waitTime = 1.5f;

    private Rigidbody2D npc;
    private Vector2 moveDirection;
    private float moveTimer;
    private float waitTimer;
    private bool isMoving = false;

    // Clamp presets
    public float minX = -1.5f;
    public float maxX = 1.5f;

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

        ClampPosition();
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

    void ClampPosition()
    {
        // Clamp position
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;

        // If the NPC hits a boundary, make it turn around
        if (pos.x <= minX || pos.x >= maxX)
        {
            bool isMoving = false;
        }
    }
}

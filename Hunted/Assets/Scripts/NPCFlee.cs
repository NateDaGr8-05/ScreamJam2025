using UnityEngine;

public class NPCFlee : MonoBehaviour
{
    public float fleeSpeed = 3f;            // Speed while fleeing
    public float fleeDuration = 1.5f;       // How long to keep running after losing sight
    public string playerTag = "Player";     // Tag to detect players only

    //clam

    private Rigidbody2D rb;
    private GameObject player;
    private NPCWander wanderScript;
    private float fleeTimer;
    private bool isFleeing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wanderScript = GetComponent<NPCWander>();
    }

    void Update()
    {
        if (isFleeing && player != null)
        {
            Vector2 currentPos = rb.position;
            Vector2 fleeDir = (currentPos - (Vector2)player.transform.position).normalized;

            // Calculate new position if moving
            Vector2 nextPos = currentPos + fleeDir * fleeSpeed * Time.fixedDeltaTime;

            // Clamp movement BEFORE applying
            if (wanderScript != null)
            {
                nextPos.x = Mathf.Clamp(nextPos.x, wanderScript.minX, wanderScript.maxX);

                // If we’ve hit the edge, stop fleeing motion
                if (Mathf.Approximately(nextPos.x, wanderScript.minX) ||
                    Mathf.Approximately(nextPos.x, wanderScript.maxX))
                {
                    rb.linearVelocity = Vector2.zero;
                    StopFleeing();
                    return;
                }
            }

            rb.MovePosition(nextPos);

            // Flip sprite to face away
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (fleeDir.x > 0 ? 1 : -1);
            transform.localScale = scale;

            // Countdown flee timer
            fleeTimer -= Time.fixedDeltaTime;
            if (fleeTimer <= 0)
            {
                StopFleeing();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            player = other.gameObject;
            StartFleeing();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            // Stop fleeing after short delay
            fleeTimer = fleeDuration;
        }
    }

    void StartFleeing()
    {
        isFleeing = true;
        fleeTimer = fleeDuration;

        if (wanderScript != null)
        {
            wanderScript.enabled = false; // Stop wandering
        }
    }

    void StopFleeing()
    {
        isFleeing = false;
        player = null;

        // Stop motion
        rb.linearVelocity = Vector2.zero;

        if (wanderScript != null)
        {
            wanderScript.enabled = true; // Start wandering again
        }
    }
}

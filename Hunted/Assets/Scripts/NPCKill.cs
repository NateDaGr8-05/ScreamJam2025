using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCKill : MonoBehaviour
{
    // is the player touching?
    private bool playerNear = false;

    // health
    public float maxHealth = 10f;
    public float currentHealth;

    // UI
    public GameObject promptWorld;
    public GameObject healthBarPrefab;
    private Slider healthSlider;

    private GameObject spawnedHealthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;

        if (healthBarPrefab != null)
        {
            spawnedHealthBar = Instantiate(healthBarPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            healthSlider = spawnedHealthBar.GetComponentInChildren<Slider>();

            if (healthSlider != null)
            {
                healthSlider.maxValue = maxHealth;
                healthSlider.value = currentHealth;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Player kills NPC
        if (playerNear && Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(2f); // change to weapon specfic damage later
        }

    }

    /// <summary>
    /// Reduce health or destroy NPC if 0
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Add rage to meter
        RageMeter rage = FindFirstObjectByType<RageMeter>();
        if (rage != null)
        {
            rage.AddRage();

        }

        // Destroy health bar
        if (spawnedHealthBar != null)
            Destroy(spawnedHealthBar);

        // Destroy NPC
        Destroy(gameObject);
    }


    /// <summary>
    /// switches bool depending on if player is in range or not
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    /// <summary>
    /// switches bool if player falls out of range
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }

    void LateUpdate()
    {
        /*if (promptWorld != null)
        {
            // Follow NPC
            promptWorld.transform.position = transform.position + new Vector3(1.5f, .5f, 0);
            promptWorld.transform.LookAt(Camera.main.transform);
            promptWorld.transform.Rotate(0, 180, 0);
        }*/

        // Move health bar with NPC
        if (spawnedHealthBar != null)
        {
            Vector3 healthBarOffset = new Vector3(0f, 1f, -0.1f); 
            spawnedHealthBar.transform.position = transform.position + healthBarOffset;
        }
    }
}

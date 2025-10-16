using UnityEngine;
using TMPro;

public class NPCKill : MonoBehaviour
{
    // is the player touching?
    private bool playerNear = false;


    public GameObject promptWorld;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(gameObject);

            // Add rage to meter
            RageMeter rage = FindFirstObjectByType<RageMeter>();
            if (rage != null) 
            { 
                rage.AddRage();
                
            }
        }

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
        if (promptWorld != null)
        {
            // Follow NPC
            promptWorld.transform.position = transform.position + new Vector3(1.5f, .5f, 0);
            promptWorld.transform.LookAt(Camera.main.transform);
            promptWorld.transform.Rotate(0, 180, 0);
        }
    }
}

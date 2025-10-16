using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    CopSystem copSystem;
    double timeOffscreen = 0;
    public GameObject player;
    public GameObject floor;
    private int direction = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //checks offscreen and destroys cop after a short period offscreen, changes copSpawned in CopSystem
        if (gameObject.transform.position.x < -9)
        {
            timeOffscreen += .1 * Time.deltaTime;
            if (timeOffscreen > 100)
            {
                copSystem.copSpawned = false;
                Destroy(gameObject);
            }
        }
        if (transform.position.x <= player.transform.position.x)
        {
            direction = 1;
            
        }
        else
        {
            direction = -1;
        }
        transform.position += new Vector3((1*direction), 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)//jump when obstacles are hit
    {
        if (collision.collider != floor.GetComponent<Collider2D>())
        {
            
            transform.position += new Vector3((2*direction), 3, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)//kill player
    {
        if (other.CompareTag("Player"))
        {
            //gameover
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
            //restarts
        }
    }
}

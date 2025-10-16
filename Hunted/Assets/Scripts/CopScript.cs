using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NewMonoBehaviourScript : MonoBehaviour
{
    CopSystem copSystem;
    double timeOffscreen = 0;
    public GameObject player;
    public GameObject floor;
    public int direction = 1;
    bool shooting = false;
    public GameObject bullet;
    private GameObject newBullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //checks offscreen and destroys cop after a short period offscreen, changes copSpawned in CopSystem
        if (gameObject.transform.position.x < player.transform.position.x - 10)
        {
            timeOffscreen += .1 * Time.deltaTime;
            if (timeOffscreen > 100)
            {
                copSystem.copSpawned = false;
                Destroy(gameObject);
            }
        }
        //determine player direction
        if (transform.position.x <= player.transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        //flip sprite
        Vector3 flip = transform.localScale;
        transform.localScale = new Vector3(Mathf.Abs(flip.x)*direction, flip.y, 1);
        //move
        transform.position += new Vector3((1*(float)direction/100), 0, 0);
        //chance to shoot if wanted is high enough
        if(copSystem.coplevel > 1)
        {
            if (1 == Random.Range(1, 121))
            {
                newBullet = GameObject.Instantiate(bullet, new Vector2(transform.position.x + (2*direction), transform.position.y), Quaternion.identity);
                newBullet.GetComponent<BulletScript>().copDirection = direction;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//jump when obstacles are hit
    {
        if (collision.collider == player.GetComponent<Collider2D>())
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            //gameover
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
            //restarts
        }
        else if (collision.collider != floor.GetComponent<Collider2D>())
        {
            transform.position += new Vector3((2*direction), 3, 0);
        }
    }
}

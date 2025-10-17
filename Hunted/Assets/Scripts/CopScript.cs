using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject copManager;
    public double timeOffscreen = 0;
    public GameObject player;
    public GameObject floor;
    public int direction = 1;
    bool shooting = false;
    public GameObject bullet;
    private GameObject newBullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //checks offscreen and destroys cop after a short period offscreen, changes copSpawned in CopSystem
        if (transform.position.x < player.transform.position.x - 8.5)
        {
            timeOffscreen += 1 * Time.deltaTime;
            if (timeOffscreen > 5)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                timeOffscreen = 0;
                Destroy(gameObject);

                copManager.GetComponent<CopSystem>().copSpawned = false;
                return;
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
        transform.localScale = new Vector3(Mathf.Abs(flip.x) * direction, flip.y, 1);
        //move
        transform.position += new Vector3((1 * (float)direction / 200), 0, 0);
        //chance to shoot if wanted is high enough
        if (copManager.GetComponent<CopSystem>().coplevel > 1)
        {
            if (1 == Random.Range(1, 551))
            {
                newBullet = GameObject.Instantiate(bullet, new Vector2(transform.position.x + (2 * direction), transform.position.y), Quaternion.Euler(new Vector3(0, 0, 90)));
                newBullet.GetComponent<BulletScript>().copDirection = direction;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == player.GetComponent<Collider2D>())//kill player
        {
            //gameover
            SceneManager.LoadScene("StartScreen");
            //restarts
        }
        else if (collision.collider != floor.GetComponent<Collider2D>())//jump when obstacles are hit
        {
            transform.position += new Vector3(0, 2, 0);
        }
    }
}


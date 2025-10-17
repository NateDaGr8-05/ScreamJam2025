using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int copDirection = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //flip sprite
        if (copDirection == -1)
        {
            Vector3 flip = transform.localScale;
            transform.localScale = new Vector3(flip.x * -1, flip.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        transform.position += new Vector3((10 * Time.deltaTime * copDirection), 0, 0);
        if (transform.position.x > Camera.main.transform.position.x+9 || transform.position.x < Camera.main.transform.position.x - 9)
        {
            Destroy(gameObject);
            return;
        }
    }

    //gets destroy on collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

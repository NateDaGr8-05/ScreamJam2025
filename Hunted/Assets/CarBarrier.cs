using UnityEngine;
using UnityEngine.SceneManagement;

public class CarBarrier : MonoBehaviour
{
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject == player)
        {
            SceneManager.LoadScene("StartScreen");
        }
    }
}

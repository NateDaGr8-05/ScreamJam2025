using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //follows player
        transform.position = new Vector3(player.transform.position.x+3, player.transform.position.y + 8, 0);
    }
}

using System.Linq.Expressions;
using UnityEngine;

public class CopSystem : MonoBehaviour
{
    public GameObject player;
    private double copDist = -50;
    public GameObject cop;
    public bool copSpawned = false;
    private int coplevel = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //represents cops chasing player while offscreen
        if (!copSpawned)
        {
            copDist += .05 * Time.deltaTime;
        }
        //tracks real time for wanted levels
        if(Time.realtimeSinceStartup > 270)
        {
            coplevel = 4;
        }
        else if (Time.realtimeSinceStartup > 180)
        {
            coplevel = 3;
        }
        else if (Time.realtimeSinceStartup > 90)
        {
            coplevel = 2;
        }
        //checks to see if the cops have caught up
        if (copDist > player.transform.position.x && !copSpawned)
        {
            switch(coplevel)
            {
                case 1:
                    GameObject.Instantiate(cop, new Vector2(player.transform.position.x - 9, 0), Quaternion.identity);
                    break;
                case 2:
                    //level2 cops
                    break;
                case 3:
                    //level3 cops
                    break;
                case 4:
                    //level4 cops
                    break;
            }  
        }
    }
}

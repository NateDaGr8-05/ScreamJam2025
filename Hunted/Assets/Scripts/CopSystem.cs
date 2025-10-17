using System.Linq.Expressions;
using UnityEngine;

public class CopSystem : MonoBehaviour
{
    public GameObject player;
    public double copDist = -50;
    public GameObject cop;
    public bool copSpawned = false;
    public int coplevel = 1;
    GameObject newCop;
    public GameObject floor;
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //represents cops chasing player while offscreen
        if (!copSpawned)
        {
            copDist += 4 * Time.deltaTime;
        }
        //tracks real time for wanted levels
        if(Time.realtimeSinceStartup > 120)
        {
            coplevel = 4;
        }
        else if (Time.realtimeSinceStartup > 80)
        {
            coplevel = 3;
        }
        else if (Time.realtimeSinceStartup > 40)
        {
            coplevel = 2;
        }
        //checks to see if the cops have caught up
        if (copDist > player.transform.position.x && !copSpawned)
        {
            copSpawned = true;
            switch(coplevel)
            {
                case 1:
                    NewCop();
                    break;
                case 2:
                    //level2 cops (shooting in copscript)
                    NewCop();
                    break;
                case 3:
                    //level3 cops (more cops)
                    NewCop();
                    NewCop();
                    break;
                case 4:
                    //level4 cops (more cops)
                    NewCop();
                    NewCop();
                    NewCop();
                    break;
            } 
        }
    }

    private void NewCop()
    {
        newCop = GameObject.Instantiate(cop, new Vector2(player.transform.position.x - 9, 0), Quaternion.identity);
        newCop.GetComponent<NewMonoBehaviourScript>().player = player;
        newCop.GetComponent<NewMonoBehaviourScript>().floor = floor;
        newCop.GetComponent<NewMonoBehaviourScript>().copManager = gameObject;
    }
}

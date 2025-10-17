using UnityEngine;
using System.Collections;

public class SirenScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Alternate());
    }
    IEnumerator Alternate()
    {
        while (true) // runs forever — or use a condition to stop
        {
            yield return new WaitForSeconds(1f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(1f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

            // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // assign in unity
    public Transform target; 
    public float speed = 5f;
    public Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (!target) return;

        Vector3 position = new Vector3(target.position.x, 0f, target.position.z) + offset;
        Vector3 smoothed = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
        transform.position = new Vector3(smoothed.x, 0f, transform.position.z);
    }
}

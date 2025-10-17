using UnityEngine;

public class SnowSound : MonoBehaviour
{
    public AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audio != null && !audio.isPlaying)
        {
            audio.Play();
        }
    }
}

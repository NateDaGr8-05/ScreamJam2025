using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStartButtonClicked()
    {
        int scene = Random.Range(1, 4);
        switch (scene)
        {
            case 1:
                //change to scene #1 name
                SceneManager.LoadScene("TestingGrounds");
                break;
            case 2:
                //change to scene #2 name
                SceneManager.LoadScene("TestingGrounds");
                break;
            case 3:
                //change to scene #3 name
                SceneManager.LoadScene("TestingGrounds");
                break;
        }
        
    }
}

using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    // assign in Unity
    public Transform startPoint;
    public List<GameObject> chunks;
    public Transform player;
    public int initialChunks = 10;              //starting chunks
    public float generateDistanceAhead = 30f;   //how far ahead of you to keep generating. adjust for performance

    private Vector2 spawnPosition;
    private List<GameObject> spawnedChunks = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPosition = startPoint.position;

        // spawn initial chunks
        for (int i = 0; i < initialChunks; i++) 
        {
            SpawnRandomChunk();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Generate chunk if player is close to end of spawned chunks
        float distanceToLast = spawnPosition.x - player.position.x;
        if (distanceToLast < generateDistanceAhead)
        {
            SpawnRandomChunk();
            CleanupChunks();
        }
    }

    void SpawnRandomChunk()
    {
        var prefab = chunks[Random.Range(0, chunks.Count)];
        var chunk = Instantiate(prefab, spawnPosition, Quaternion.identity);

        // Determine width of preset
        float chunkWidth = 0f;
        Renderer rend = chunk.GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            chunkWidth = rend.bounds.size.x;
        }
        else
        {
            chunkWidth = 10f; // default width
        }
        spawnPosition.x += chunkWidth;
        spawnedChunks.Add(chunk);
    }

    void CleanupChunks()
    {
        // Remove chunks behind player for performance
        if (spawnedChunks.Count > 14) //can be replaced w/ any number over initial spawn chunk size
        {
            GameObject oldest = spawnedChunks[0];
            //despawn preset that is a number behind the current x
            if (oldest.transform.position.x < player.position.x - 50f)
            { 
                spawnedChunks.RemoveAt(0);
                Destroy(oldest);
                
            }
        }

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleManager : MonoBehaviour
{
    public GameObject[] obstacles;  // Array of obstacle prefabs
    public Camera mainCamera;
    public float minSpawnRate = 0f;  // Minimum time between spawns
    public float maxSpawnRate = 2.0f;  // Maximum time between spawns
    private float timer = 0f;
    private float currentSpawnRate;
    private List<GameObject> activeObstacles = new List<GameObject>();

    void Start()
    {
        SetRandomSpawnRate();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= currentSpawnRate)
        {
            int obstacle = Random.Range(0, obstacles.Length);
            SpawnObstacle(obstacle);
            SetRandomSpawnRate();
            timer = 0;
        }
        CleanupObstacles();
    }

    void SetRandomSpawnRate()
    {
        currentSpawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        // Additional logic can be added here to adjust spawn rate based on game difficulty or player performance
    }

    void SpawnObstacle(int obstacleIndex)
    {
        GameObject obstacle = Instantiate(obstacles[obstacleIndex]);
        if (obstacleIndex == 0 || obstacleIndex == 2)
            obstacle.transform.position = new Vector3((mainCamera.transform.position.x + mainCamera.orthographicSize * 2 + 1), 1.5f, 0);  // Adjust position as needed
        else if (obstacleIndex == 1 || obstacleIndex == 3)
            obstacle.transform.position = new Vector3((mainCamera.transform.position.x + mainCamera.orthographicSize * 2 + 1), -0.6f, 0);
        activeObstacles.Add(obstacle);
    }

    void CleanupObstacles()
    {
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            if (activeObstacles[i].transform.position.x < mainCamera.transform.position.x - mainCamera.orthographicSize * 2 - 1)
            {
                Destroy(activeObstacles[i]);
                activeObstacles.RemoveAt(i);
            }
        }
    }
}
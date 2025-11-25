using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public float spawnZ = 30f;
    public float spawnInterval = 1.2f;
    public Transform[] lanePositions; // pos x para cada pista

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        int lane = Random.Range(0, lanePositions.Length);
        Vector3 pos = new Vector3(lanePositions[lane].position.x, lanePositions[lane].position.y, spawnZ + transform.position.z);
        GameObject obj = Instantiate(obstacles[Random.Range(0, obstacles.Length)], pos, Quaternion.identity);
    }
}

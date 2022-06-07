using UnityEngine;
// @author rasmushy
public class EnemySpawning : MonoBehaviour
{
    public float minTimeBtwSpawns = 1f; // seconds
    public float maxTimeBtwSpawns = 3f; // seconds
    public float spawnTime = 15f;
    public GameObject[] spawnPoints;
    public GameObject[] enemies;
    public bool canSpawn;
    public bool spawnerDone;
    private int randomIndex;
    private GameObject currentPoint;

    private void Start()
    {
        Invoke("SpawnEnemy", 0.5f);
    }

    private void Update()
    {
        if (canSpawn)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime < 0)
            {
                canSpawn = false;
                spawnerDone = true;
            }
        }
    }

    void SpawnEnemy()
    {
        randomIndex = Random.Range(0, spawnPoints.Length);
        currentPoint = spawnPoints[randomIndex];
        float timeBtwSpawns = Random.Range(minTimeBtwSpawns, maxTimeBtwSpawns);

        if (canSpawn)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], currentPoint.transform.position, Quaternion.identity);
        }

        Invoke("SpawnEnemy", timeBtwSpawns);

        if (spawnerDone)
        {
            //Done spawning
            Destroy(gameObject);
        }
    }

    public void StartSpawner(bool canSpawn)
    {
        this.canSpawn = canSpawn;
        SpawnEnemy();
    }

}

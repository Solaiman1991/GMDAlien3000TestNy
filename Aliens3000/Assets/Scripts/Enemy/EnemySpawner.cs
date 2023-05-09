using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 60f;

    private MissionManager missionManager;

    private void Start()
    {
        missionManager = FindObjectOfType<MissionManager>();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            foreach (GameObject enemyPrefab in enemyPrefabs)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                EnemyShortRange enemyShortRange = spawnedEnemy.GetComponent<EnemyShortRange>();
                if (enemyShortRange != null)
                {
                    enemyShortRange.missionManager = missionManager;
                }
                missionManager.IncrementTotalEnemies();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform spawnPoint; 
    public Transform pointA; 
    public Transform pointB; 
    public Transform player; 
    public float spawnInterval = 3f; 

    private void Start()
    {
        // Start spawning enemies
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            // Instantiate enemy at spawn point
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

           
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.player = player;
                enemyController.pointA = pointA;
                enemyController.pointB = pointB;
            }
            else
            {
                Debug.LogWarning("Enemy prefab is missing EnemyController component!");
            }

           
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

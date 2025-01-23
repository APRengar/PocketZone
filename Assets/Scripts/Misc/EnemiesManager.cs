using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private BoxCollider2D spawnArea;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private int numberOfEnemies = 5;
    
    private void Start() 
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector2 randomPosition = GetRandomPositionInBox();

            int randomIndex = Random.Range(0, enemyPrefabs.Count);
            GameObject randomEnemy = enemyPrefabs[randomIndex];
            
            Instantiate(randomEnemy, randomPosition, Quaternion.identity);
        }
    }

    private Vector2 GetRandomPositionInBox()
    {
        Vector2 center = spawnArea.bounds.center;
        Vector2 size = spawnArea.bounds.size;

        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);

        return new Vector2(randomX, randomY);
    }
}

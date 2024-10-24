using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    int count = 5;
    int minCount = 1;
    int maxCount = 5;
    public List<Transform> spawnPoints;
    void Start()
    {
        count = Random.Range(minCount, maxCount);
        for (int i = 0; i < count; i++)
        {
            var rndPoint = Random.Range(0, spawnPoints.Count);
            SpawnEnemies(spawnPoints[rndPoint]);
        }
    }

    void SpawnEnemies(Transform point) => Instantiate(enemy, GetRandomPosInRadius(point.position, 3), Quaternion.identity);

    Vector3 GetRandomPosInRadius(Vector3 centerPoint, float radius) => centerPoint + (Random.insideUnitSphere * radius);
}

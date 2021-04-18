using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] int numberOfEnemiesSpawn = 1;
    [SerializeField] float spawnRadius = 10f;
    [SerializeField] GameObject enemyPrefab;

    private void Awake()
    {
        SpawnEnemies();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemiesSpawn; i++)
        {
            float randX = Random.Range(0, spawnRadius / 2);
            float randZ = Random.Range(0, spawnRadius / 2);
            Vector3 spawnPoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);
            Instantiate(enemyPrefab, spawnPoint, enemyPrefab.transform.rotation);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.gray;

        Handles.DrawWireDisc(transform.position, Vector3.up, spawnRadius);
    }
#endif
}

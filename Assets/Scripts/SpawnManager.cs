using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private Vector2 spawnPos = new Vector2(30, 0);

    // Test values 
    private float startDelay = 2f;
    private float repeatRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);  
    }
}

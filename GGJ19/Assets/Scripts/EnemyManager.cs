using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject enemy;
    public float spawnTime = 3.0f;
    public Transform[] spawners;
    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0.0f, spawnTime);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn() {
    
        
        int spawnPointIndex = Random.Range(0, spawners.Length);
        Instantiate(enemy, spawners[spawnPointIndex].position, spawners[spawnPointIndex].rotation);
    }
}

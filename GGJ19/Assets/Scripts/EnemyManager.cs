using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour {
    
    public GameObject enemy;
    public float spawnTime = 3.0f;
    public Transform[] spawners;
    private Boolean spawning;


    // Start is called before the first frame update
    void Start() {
        GameManager.GM.setEnemyManager(this);
    }

    // Update is called once per frame
    void Update() {
    }

    void Spawn() {
        int spawnPointIndex = Random.Range(0, spawners.Length);
        Instantiate(enemy, spawners[spawnPointIndex].position, spawners[spawnPointIndex].rotation);
    }

    public void startSpawn() {
        if (!spawning) {
            spawning = true;
            InvokeRepeating("Spawn", 0.0f, spawnTime);
        }
    }

    public void stopSpawn() {
        if (spawning) {
            spawning = false;
            CancelInvoke();
        }
    }
}
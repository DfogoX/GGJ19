using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEvents : MonoBehaviour {
    private bool spawning;

    private void Start() {
        spawning = GameManager.GM.spawningMobs();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && spawning) {
            GameManager.GM.deactivateSpawner();
            spawning = !spawning;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("spawn. " + spawning);
        if (other.gameObject.CompareTag("Player") && !spawning) {
            GameManager.GM.activateSpawner();
            spawning = !spawning;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        if (GameManager.GM.inside) {
            GameManager.GM.healPlayer(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            GameManager.GM.inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            GameManager.GM.inside = false;
        }
    }
}
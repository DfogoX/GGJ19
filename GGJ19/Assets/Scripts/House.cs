using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {
    private bool inside;

    // Update is called once per frame
    void Update() {
        if (inside) {
            GameManager.GM.healPlayer(1);
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            inside = true;
            GameManager.GM.
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            inside = false;
        }
    }
}
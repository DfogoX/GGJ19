using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OpenDoor : MonoBehaviour {

    public Sprite openDoor;

    private SpriteRenderer sr;

    private PolygonCollider2D pc;
    // Start is called before the first frame update
    void Start() {
        sr = GetComponent<SpriteRenderer>();
        pc = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (GameManager.GM.hasItem(0)) {
                sr.sprite = openDoor;
                
            }
        }
    }
}
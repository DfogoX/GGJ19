using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {
    private Collider2D _collider;
    private bool _hasItem;

    // Start is called before the first frame update
    void Start() {
        _hasItem = false;
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update() {
        if (!_hasItem) {
            if (GameManager.GM.hasItem("Boia")) {
                _collider.isTrigger = true;
                _hasItem = true;
            }
        }
    }
}
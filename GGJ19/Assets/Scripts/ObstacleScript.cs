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
        if (Input.GetKeyDown(KeyCode.B)) {
            _hasItem = true;
        }

        if (_hasItem) {
            _collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.name);
    }
}
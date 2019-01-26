using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {
    private Collider2D _collider;
    private bool _hasItem;

    // Start is called before the first frame update
    void Start() {
        _hasItem = false;
        _collider = GetComponent<CompositeCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Y)) {
            _hasItem = true;
        }

        if (_hasItem) {
            Debug.Log("river is now trigger");
            Debug.Log(_collider.name);
            
            _collider.isTrigger = true;
        }
    }
}
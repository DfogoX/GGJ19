using System.Collections.Generic;
using UnityEngine;

public class player_moves : MonoBehaviour {
    public float speed = 6.0f;
    public float sprintSpeed = 12.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    private void Start() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        var currSpeed = speed;
        moveDirection = Vector3.zero;

        if (Input.GetKey("up") || Input.GetKey("w")) {
            moveDirection += new Vector3(0.0f, 1.0f, 0.0f);
        }

        if (Input.GetKey("left") || Input.GetKey("a")) {
            moveDirection += new Vector3(-1.0f, 0.0f, 0.0f);
        }

        if (Input.GetKey("down") || Input.GetKey("s")) {
            moveDirection += new Vector3(0.0f, -1.0f, 0.0f);
        }

        if (Input.GetKey("right") || Input.GetKey("d")) {
            moveDirection += new Vector3(1.0f, 0.0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            currSpeed = sprintSpeed;
        }

        moveDirection = moveDirection * currSpeed;

        //Debug.Log("mov_f: " + moveDirection);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
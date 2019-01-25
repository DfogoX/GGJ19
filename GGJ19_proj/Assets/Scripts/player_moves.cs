using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_moves : MonoBehaviour
{
    
    public float speed = 6.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        // let the gameObject fall down
        gameObject.transform.position = new Vector3(0, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Vector3.zero;

        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            moveDirection+= new Vector3(0.0f, 1.0f, 0.0f );
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            moveDirection+= new Vector3(-1.0f, 0.0f,0.0f );
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            moveDirection+= new Vector3(0.0f, -1.0f, 0.0f );
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            moveDirection+= new Vector3(1.0f, 0.0f,0.0f );
        }
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;
        //Debug.Log("mov_f: " + moveDirection);
        controller.Move(moveDirection * Time.deltaTime);

    }

}
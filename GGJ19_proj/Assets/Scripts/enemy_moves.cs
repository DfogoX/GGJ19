using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_moves : MonoBehaviour
{
    
    public Transform Player;
    private CharacterController controller;
    public int MoveSpeed = 4;
    public float MaxDist = 4;
    public float MinDist = 2;
    public float RunDist = 0.1f;
    private Vector3 moves;
    private float cd;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cd = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(Player);

        var dist = Vector3.Distance(transform.position, Player.position);
        if ( dist > MinDist && cd < 0.1f) {
          //  transform.position += transform. * MoveSpeed * Time.deltaTime;
          moves = Vector3.Normalize(Player.position - transform.position);
            controller.Move(moves * MoveSpeed* Time.deltaTime);
            if (dist <= MaxDist)
            {
                Debug.Log("in range");
                controller.Move(moves * MoveSpeed * Time.deltaTime);
            }
 
        }
        else {
            if (dist < RunDist)
                controller.Move(moves * 1/4 * MoveSpeed * Time.deltaTime);
            else
                controller.Move(moves * 2*MoveSpeed * Time.deltaTime);
            if (cd < 0.1f)
                cd = 1.0f;
            cd -= Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_moves : MonoBehaviour
{
    
    private Rigidbody2D rigid;
    public int MoveSpeed = 4;
    public float MaxDist = 4;
    public float MinDist = 2;
    public float RunDist = 0.1f;
    private Vector3 moves;
    private float cd = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(Player);
        Transform playerTransform = GameManager.GM.findPlayer(); 
        var dist = Vector3.Distance(transform.position, playerTransform.position);
        if ( dist > MinDist && cd < 0.1f) {
          //  transform.position += transform. * MoveSpeed * Time.deltaTime;
          moves = Vector3.Normalize(playerTransform.position - transform.position);
          rigid.AddForce(moves * MoveSpeed* Time.deltaTime);
            if (dist <= MaxDist)
            {
                rigid.AddForce(moves * MoveSpeed * Time.deltaTime);
            }
 
        }
        else {
            if (dist < RunDist)
                rigid.AddForce(moves * 1/4 * MoveSpeed * Time.deltaTime);
            else
                rigid.AddForce(moves * 2*MoveSpeed * Time.deltaTime);
            if (cd < 0.1f)
                cd = 1.0f;
            cd -= Time.deltaTime;
        }
    }
}

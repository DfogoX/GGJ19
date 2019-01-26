using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_moves : MonoBehaviour {

    
    private SpriteRenderer rend;
    private Rigidbody2D rigid;
    private Collider2D coll;
    private AudioSource source;

    public AudioClip soundSpawn;
    public int MoveSpeed = 4;
    public float MaxDist = 4;
    public float MinDist = 2;
    public float RunDist = 0.1f;
    private Vector2 moves;
    private float cd = 0.0f;
    private Boolean touched = false;
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        
        source.PlayOneShot(soundSpawn,70);
    }

    // Update is called once per frame
    void Update()
    {
        Transform playerTransform = GameManager.GM.findPlayer(); 
        var dist = Vector2.Distance(transform.position, playerTransform.position);
        if (touched) {
            rigid.MovePosition(rigid.position +  moves * MoveSpeed * Time.deltaTime);
            if(!rend.isVisible)
                Destroy((this.gameObject));
        }
        else if ( dist > MinDist && cd < 0.1f) {
          //  transform.position += transform. * MoveSpeed * Time.deltaTime;
          moves = Vector3.Normalize(playerTransform.position - transform.position);
          rigid.MovePosition(rigid.position +  moves * MoveSpeed* Time.deltaTime);
            if (dist <= MaxDist)
            {
                rigid.MovePosition(rigid.position +  moves * MoveSpeed*2 * Time.deltaTime);
            }
 
        }
        else {
            if (dist < RunDist)
                rigid.MovePosition(rigid.position +  moves * 1/4 * MoveSpeed * Time.deltaTime);
            else {
                rigid.MovePosition(rigid.position +  moves * 2*MoveSpeed * Time.deltaTime);
                if (cd < 0.1f)
                    cd = 1.5f;
            }
            cd -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            touched = true;
            coll.enabled = false;
        }
    }
}

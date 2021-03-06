﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_moves : MonoBehaviour {
    private SpriteRenderer rend;
    private Rigidbody2D rigid;
    private Collider2D coll;
    private AudioSource source;
    private Animator animator;
    public AudioClip soundSpawn;
    public AudioClip soundAttack;
    public int MoveSpeed = 4;
    public float MaxDist = 4;
    public float MinDist = 2;
    public float RunDist = 0.1f;
    private Vector2 moves;
    private float cd = 0.0f;
    private Boolean touched = false;

    // Start is called before the first frame update
    void Start() {
        source = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponentInChildren<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        animator = GetComponentInChildren<Animator>();
        source.clip = soundSpawn;
        source.Play();
        StartCoroutine(FadeOut(1f, 2f));
    }

    // Update is called once per frame
    void Update() {
        if(GameManager.GM.isPlayerDead())
            Destroy(this.gameObject);
        Transform playerTransform = GameManager.GM.findPlayer();
        var dist = Vector2.Distance(transform.position, playerTransform.position);
        if (touched || !GameManager.GM.spawningMobs()) {
            rigid.MovePosition(rigid.position + moves * MoveSpeed * Time.deltaTime);
            if(!rend.isVisible)
            Destroy((this.gameObject));
        }
        else if (dist > MinDist && cd < 0.1f) {
            //  transform.position += transform. * MoveSpeed * Time.deltaTime;
            moves = Vector3.Normalize(playerTransform.position - transform.position);
            if (moves.x > 0) {
                animator.Play("GhostIdleRight");
            }
            else {
                animator.Play("Idle");
            }

            rigid.MovePosition(rigid.position + moves * MoveSpeed * Time.deltaTime);
            if (dist <= MaxDist) {
                rigid.MovePosition(rigid.position + moves * MoveSpeed * 2 * Time.deltaTime);
            }
        }
        else {
            //transform.localScale = new Vector3(2 / 3, 2 / 3, 0);
            if (dist < RunDist) {
                //rigid.MovePosition(rigid.position + moves * 1 / 4 * MoveSpeed * Time.deltaTime);

            }
            else {
                //source.clip = soundAttack;
                //source.Play();
                rigid.MovePosition(rigid.position + moves * 2 * MoveSpeed * Time.deltaTime);
                if (cd < 0.1f) {
                    cd = 1.5f;
                }
            }

            cd -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            touched = true;
            coll.enabled = false;
            GameManager.GM.damagePlayer(1);
        }
    }

    public IEnumerator FadeOut(float WaitTime, float FadeTime)
    {
        yield return new WaitForSeconds(WaitTime);
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        source.Stop();
        source.volume = startVolume;
    }
}
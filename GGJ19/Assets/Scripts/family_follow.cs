using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class family_follow : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject itemo;
    private Animator animator;
    private bool caught = false;
    private bool itemGiven = false;
    void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (caught) {
            Transform playerTransform = GameManager.GM.findPlayer();
            var dist = Vector3.Distance(transform.position, playerTransform.position);
            var direction = Vector3.zero;
            if (dist > 1) {
                direction = Vector3.Normalize(playerTransform.position - transform.position);
                transform.position = transform.position + direction * Time.deltaTime;
            }

            if (direction != Vector3.zero) {
                //Moving more horizontaly
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
                    if (direction.x > 0) {
                        animator.Play("MoveRight");
                    }
                    else {
                        animator.Play("MoveLeft");
                    }
                }//Moving more verticaly
                else {
                    if (direction.y > 0) {
                        animator.Play("MoveUp");
                    }
                    else {
                        animator.Play("MoveDown");
                    }
                }
            }
            else {
                animator.Play("Idle");
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && !caught) {
            animator.Play("Idle");
            caught = true;
        }
        else if (other.gameObject.CompareTag("Home") && !itemGiven) {
            Invoke("Spawn", 0);
            itemGiven = true;
        }

    }

    void Spawn() {
        Instantiate(itemo, transform.position, Quaternion.identity);
    }
}
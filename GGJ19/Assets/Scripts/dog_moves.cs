using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class dog_moves : MonoBehaviour {
    // Start is called before the first frame update
    public float speed = 3.0f;
    public int NumOfWayPoints;
    public GameObject _Item;
    private Animator animator;
    private bool caught = false;
    public Transform[] WayPoints;
    private int currWayPoint = 0;
    private bool keyGiven = false;

    void Start() {
        animator = GetComponentInChildren<Animator>();
        GameManager.GM.setDog(this.transform);
    }

    // Update is called once per frame
    void Update() {
        if (caught && (currWayPoint < NumOfWayPoints)) {
            var dist = Vector3.Distance(transform.position, WayPoints[currWayPoint].position);
            var direction = Vector3.zero;
            if (dist > 1) {
                direction = Vector3.Normalize(WayPoints[currWayPoint].position - transform.position);
                transform.position = transform.position + direction * Time.deltaTime * speed;
            }

            if (direction != Vector3.zero) {
                if (direction.x > 0) {
                    animator.Play("DogWalkGeneralLeftKey");
                }
                else {
                    animator.Play("DogWalkGeneralRightKey");
                }
            }
            else {
                currWayPoint++;
                animator.Play("DogIdleHappy");
            }
        }

        if (currWayPoint == NumOfWayPoints - 1 && !keyGiven) {
            Invoke("Spawn", 0);
            GameManager.GM.addItem("Key");
            keyGiven = true;
        }

        if (currWayPoint > NumOfWayPoints) {
            animator.Play("DogIdleHappy");
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && !caught) {
            animator.Play("DogIdleHappy");
            caught = true;
        }
    }

    void Spawn() {
        Instantiate(_Item, transform.position, Quaternion.identity);
    }

    public void respawn() {
        this.transform.position = Vector3.left;
        this.transform.GetComponentInChildren<Animator>().Play("DogCryingSad");
    }
}
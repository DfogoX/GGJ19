using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class dog_moves : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;

    private bool caught = false;
    void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (caught) {
            Transform playerTransform = GameManager.GM.findPlayer(); 
            var dist = Vector3.Distance(transform.position, playerTransform.position);
            var direction = Vector3.zero;
            if (dist > 1) {
                direction = Vector3.Normalize(playerTransform.position - transform.position);
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
                    direction = Vector3.Normalize(Vector3.right *direction.x );
                }
                else {
                    direction = Vector3.Normalize(Vector3.up * direction.y );
                }
                transform.position = transform.position + direction * Time.deltaTime;
            }

            if (direction != Vector3.zero) {
                if (direction.x > 0) {
                    animator.Play("DogWalkGeneralLeft");
                }
                else {
                    animator.Play("DogWalkGeneralRight");
                }
            }
            else {
                animator.Play("DogIdleHappy");
            }
        }
    }
   

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            animator.Play("DogIdleHappy");
            caught = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dog_moves : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;
    private CharacterController controller;

    private bool caught = false;
    void Start() {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Hello");
        if (other.gameObject.CompareTag("Player")) {
            animator.Play("DogIdleHappy");
        }
    }
}
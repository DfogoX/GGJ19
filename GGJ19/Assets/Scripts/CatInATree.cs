using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CatInATree : MonoBehaviour {
    // Start is called before the first frame update
    // Item is family member that is stuck in the tree
    public GameObject Item;
    private Animator animator;
    private bool saved = false;
    private bool animationEnded = false;

    void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (saved) {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1
                && !animator.IsInTransition(0) &&
                animator.GetCurrentAnimatorStateInfo(0).IsName("Saved") && !animationEnded) {
                animationEnded = true;
                var stuck = transform.GetChild(1);
                stuck.GetComponent<SpriteRenderer>().enabled = false;
                Invoke("Spawn", 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && !saved
            && GameManager.GM.hasItem("Rope")) {
            var rope = transform.GetChild(2);
            rope.GetComponent<SpriteRenderer>().enabled = true;
            animator.Play("Saved");
            saved = true;
        }
    }

    void Spawn() {
        Instantiate(Item, transform.position + new Vector3(1.3f,-1.6f,0.0f), Quaternion.identity);
    }
}
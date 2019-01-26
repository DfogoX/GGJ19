using System;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rigid;
    private Animator animator;
    private Vector2 moveDirection = Vector2.zero;
    public float speed = 6.0f;
    public float sprintSpeed = 12.0f;
    public float stamina = 100.0f;

    private void Start() {
        GameManager.GM.setPlayer(this);
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        move();

        spawning();
    }

    private void move() {
        var currSpeed = 0.0f;
        var decay = 20.0f;
        var restore = 15.0f;

        if (Input.GetKeyDown("up") || Input.GetKeyDown("w")) {
            moveDirection = new Vector2(0.0f, 1.0f);
            playAnim("MoveUp");
        }

        if (Input.GetKeyDown("left") || Input.GetKeyDown("a")) {
            moveDirection = new Vector2(-1.0f, 0.0f);
            playAnim("MoveLeft");
        }

        if (Input.GetKeyDown("down") || Input.GetKeyDown("s")) {
            moveDirection = new Vector2(0.0f, -1.0f);
            playAnim("MoveDown");
        }

        if (Input.GetKeyDown("right") || Input.GetKeyDown("d")) {
            moveDirection = new Vector2(1.0f, 0.0f);
            playAnim("MoveRight");
        }

        if (Input.GetKey("up") || Input.GetKey("w") || Input.GetKey("left") || Input.GetKey("a")
            || Input.GetKey("down") || Input.GetKey("s") || Input.GetKey("right") || Input.GetKey("d")) {
            currSpeed = speed;
        }
        else {
            moveDirection = Vector2.zero;
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            currSpeed = sprintSpeed;
            if (moveDirection != Vector2.zero) {
                stamina -= decay * Time.deltaTime;
            }

            if (stamina <= 0.0f) {
                stamina = -1.0f;
                currSpeed = speed;
            }
        }

        var endMoveDirection = moveDirection * currSpeed;
        if (endMoveDirection == Vector2.zero) {
            playAnim("Idle");
            stamina += restore * Time.deltaTime;
            if (stamina > 100.0f) {
                stamina = 100.0f;
            }
        }

        animator.speed = currSpeed / speed;
        rigid.MovePosition(rigid.position + endMoveDirection * Time.deltaTime);
    }

    private void playAnim(string animName) {
        animator.Play(animName);
    }

    private void spawning() {
        if(Input.GetKey(KeyCode.K))
            GameManager.GM.activateSpawner();
        if(Input.GetKey(KeyCode.L))
            GameManager.GM.deactivateSpawner();
    }
}
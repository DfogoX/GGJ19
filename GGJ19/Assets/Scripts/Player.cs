using System;
using System.Collections;
using UnityEngine;
using UnityEngine.WSA;
using Application = UnityEngine.Application;

public class Player : MonoBehaviour {
    
    private Rigidbody2D rigid;
    private Animator animator;
    private SpriteRenderer rend;
    private Vector2 moveDirection = Vector2.zero;
    private float cd_damage = 0.5f;
    private String lastAnim = "Idle";
    
    public float speed = 6.0f;
    public float sprintSpeed = 12.0f;
    public float stamina = 100.0f;
    public float moveX = 0f;
    public float moveY = 0f;
    private int playerHP = 5;
    private bool immune;

    private void Start() {
        immune = false;
        GameManager.GM.setPlayer(this);
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        move();

        spawning();

        damaging();

    }

    private void move() {
        var currSpeed = 0.0f;
        var decay = 20.0f;
        var restore = 15.0f;
        
        playAnim(lastAnim);
        if (Input.GetKeyDown("up") || Input.GetKeyDown("w")) {
            moveY += 1f;
            playAnim("MoveUp");
        }

        if (Input.GetKeyUp("up") || Input.GetKeyUp("w")) {
            moveY -= 1f;
            if (moveY != 0f) playAnim("MoveDown");
            else
            {
                if (moveX > 0f) playAnim("MoveRight");
                else if (moveX < 0f) playAnim("MoveLeft");
            }
        }

        if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
        {
            moveY -= 1f;
            playAnim("MoveDown");
        }

        if (Input.GetKeyUp("down") || Input.GetKeyUp("s"))
        {
            moveY += 1f;
            if (moveY != 0f) playAnim("MoveUp");
            else
            {
                if (moveX > 0f) playAnim("MoveRight");
                else if (moveX < 0f) playAnim("MoveLeft");
            }
        }

        if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
        {
            moveX += 1f;
            playAnim("MoveRight");
        }

        if (Input.GetKeyUp("right") || Input.GetKeyUp("d"))
        {
            moveX -= 1f;
            if (moveX != 0f) playAnim("MoveLeft");
            else
            {
                if (moveY > 0f) playAnim("MoveUp");
                else if (moveY < 0f) playAnim("MoveDown");
            }
        }

        if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
        {
            moveX -= 1f;
            playAnim("MoveLeft");
        }

        if (Input.GetKeyUp("left") || Input.GetKeyUp("a"))
        {
            moveX += 1f;
            if (moveX != 0f) playAnim("MoveRight");
            else
            {
                if (moveY > 0f) playAnim("MoveUp");
                else if (moveY < 0f) playAnim("MoveDown");
            }
        }

        if (moveX != 0f || moveY != 0f) {
            currSpeed = speed;
        }

        moveDirection = new Vector2(moveX, moveY);

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

        var endMoveDirection = moveDirection.normalized * currSpeed;
        if (endMoveDirection == Vector2.zero) {
            playAnim("Idle");
            stamina += restore * Time.deltaTime;
            if (stamina > 100.0f) {
                stamina = 100.0f;
            }
        }

        animator.speed = currSpeed / speed;
        
        rigid.MovePosition(rigid.position + endMoveDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    private void playAnim(string animName) {
        animator.Play(animName);
        lastAnim = animName;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("river")) {
            var sprOne = transform.GetChild(1);
            sprOne.GetComponent<SpriteRenderer>().enabled = false;
            var sprTwo = transform.GetChild(2);
            sprTwo.GetComponent<SpriteRenderer>().enabled = true;
            animator = sprTwo.GetComponent<Animator>();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("river")) {
            var sprOne = transform.GetChild(1);
            sprOne.GetComponent<SpriteRenderer>().enabled = true;
            var sprTwo = transform.GetChild(2);
            sprTwo.GetComponent<SpriteRenderer>().enabled = false;
            animator = sprOne.GetComponent<Animator>();
        }        
    }
//SPAWNING
    private void spawning() {
        if (Input.GetKey(KeyCode.K))
            GameManager.GM.activateSpawner();
        if (Input.GetKey(KeyCode.L))
            GameManager.GM.deactivateSpawner();
    }
//DAMAGING
    private void damaging() {
        if (Input.GetKeyDown(KeyCode.C)) {
            GameManager.GM.damagePlayer(1);
            Debug.Log("hp: " + playerHP);
        }

        else if (Input.GetKeyDown(KeyCode.C)) {
            GameManager.GM.damagePlayer(2);
            Debug.Log("hp: " + playerHP);
        }
        else if (Input.GetKeyDown(KeyCode.B)) {
            GameManager.GM.healPlayer(1);
            Debug.Log("hp: " + playerHP);
        }

        cd_damage = 3.0f;

    }

    public bool takeDamage(int amount) {
        if (!immune) {
            rend.color = Color.red;
            playerHP -= amount;
            immune = true;
            StartCoroutine(ImmunityCooldown());
            return true;
        }

        return false;
    }

    private IEnumerator ImmunityCooldown() {
        yield return new WaitForSeconds(cd_damage);
        immune = false;
        rend.color = Color.white;
    }

    public void giveHeal(int amount) {
        playerHP += amount;
    }

    public int HP() {
        return this.playerHP;
    }
}
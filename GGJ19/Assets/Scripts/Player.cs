using UnityEngine;

public class Player : MonoBehaviour {
    
    private Rigidbody2D rigid;
    private Animator animator;
    private Vector2 moveDirection = Vector2.zero;
    public float speed = 6.0f;
    public float sprintSpeed = 12.0f;
    
    private void Start() {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        GameManager.GM.player = this;
    }

    // Update is called once per frame
    void Update() {

        move();
        //Debug.Log("mov_f: " + moveDirection);
    }

    private void move() {
        
        var currSpeed = 0.0f;
        
        if (Input.GetKeyDown("up") || Input.GetKeyDown("w")) {
            moveDirection = new Vector2(0.0f, 1.0f);
            playAnim("PlayerMoveUp");
        }

        if (Input.GetKeyDown("left") || Input.GetKeyDown("a")) {
            moveDirection = new Vector2(-1.0f, 0.0f);
            playAnim("PlayerMoveLeft");
        }

        if (Input.GetKeyDown("down") || Input.GetKeyDown("s")) {
            moveDirection = new Vector2(0.0f, -1.0f);
            playAnim("PlayerMoveDown");
        }

        if (Input.GetKeyDown("right") || Input.GetKeyDown("d")) {
            moveDirection = new Vector2(1.0f, 0.0f);
            playAnim("PlayerMoveRight");
        }

        if (Input.GetKey("up") || Input.GetKey("w") || Input.GetKey("left") || Input.GetKey("a")
            ||Input.GetKey("down") || Input.GetKey("s") || Input.GetKey("right") || Input.GetKey("d")) {
            currSpeed = speed;
        }
        
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            currSpeed = sprintSpeed;
        }

        var endMoveDirection = moveDirection * currSpeed;

        if (endMoveDirection == Vector2.zero) {
            playAnim("PlayerIdle");
        }
        
        rigid.MovePosition(rigid.position +  endMoveDirection * Time.deltaTime);
    }

    private void playAnim(string animName) {
        animator.Play(animName);
    }
}
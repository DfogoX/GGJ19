using UnityEngine;

public class player_moves : MonoBehaviour {
    public float speed = 6.0f;
    public float sprintSpeed = 12.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Animator animator;

    private void Start() {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        
        var currSpeed = 0.0f;
        
        if (Input.GetKeyDown("up") || Input.GetKeyDown("w")) {
            moveDirection = new Vector3(0.0f, 1.0f, 0.0f);
            animator.Play("PlayerMoveUp");
        }

        if (Input.GetKeyDown("left") || Input.GetKeyDown("a")) {
            moveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
            animator.Play("PlayerMoveLeft");
        }

        if (Input.GetKeyDown("down") || Input.GetKeyDown("s")) {
            moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
            animator.Play("PlayerMoveDown");
        }

        if (Input.GetKeyDown("right") || Input.GetKeyDown("d")) {
            moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
            animator.Play("PlayerMoveRight");
        }

        if (Input.GetKey("up") || Input.GetKey("w") || Input.GetKey("left") || Input.GetKey("a")
        ||Input.GetKey("down") || Input.GetKey("s") || Input.GetKey("right") || Input.GetKey("d")) {
            currSpeed = speed;
        }
        
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            currSpeed = sprintSpeed;
        }

        var endMoveDirection = moveDirection * currSpeed;

        if (endMoveDirection == Vector3.zero) {
            animator.Play("PlayerIdle");
        }
        //Debug.Log("mov_f: " + moveDirection);
        controller.Move(endMoveDirection * Time.deltaTime);

    }
}
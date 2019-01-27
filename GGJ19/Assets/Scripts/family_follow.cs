using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class family_follow : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject itemo;
    public int itemIndex;
    private Animator animator;
    private bool caught = false;
    private bool itemGiven = false;
    public float speed;
    public float NumOfWayPoints;
    public Transform[] WayPoints;
    private int currWayPoint = 0;
    private Vector3 initPos;
    private bool stopmovement = false;
    void Start() {
        animator = GetComponentInChildren<Animator>();
        initPos = transform.position;
    }

    public void stop() {
        stopmovement = true;
        transform.GetComponentInChildren<Animator>().Play("Idle");
    }
    // Update is called once per frame
    void Update() {
        if (stopmovement) return;
        if (caught && !itemGiven) {
            Transform playerTransform = GameManager.GM.findPlayer();
            var dist = Vector3.Distance(transform.position, playerTransform.position);
            var direction = Vector3.zero;
            if (dist > 1) {
                direction = Vector3.Normalize(playerTransform.position - transform.position);
                transform.position = transform.position + direction * Time.deltaTime * speed;
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
                } //Moving more verticaly
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

        //Start Waypoints if any exist
        if (itemGiven && currWayPoint < NumOfWayPoints) {
            var dist = Vector3.Distance(transform.position, WayPoints[currWayPoint].position);
            var direction = Vector3.zero;
            if (dist > 0.05f) {          
                direction = Vector3.Normalize(WayPoints[currWayPoint].position - transform.position);
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
                } //Moving more verticaly
                else {
                    if (direction.y > 0) {
                        animator.Play("MoveUp");
                    }
                    else {
                        animator.Play("MoveDown");
                    }
                transform.position = transform.position + direction * Time.deltaTime * speed;
                }
            }else {
                currWayPoint++;
            }
        }
        if (currWayPoint >= NumOfWayPoints) {
            animator.Play("Idle");
        }
    }

    public void respawn() {
        if (!itemGiven) {
            transform.position = initPos;
            caught = false;
        }
        transform.GetComponentInChildren<Animator>().Play("Idle");
        stopmovement = false;
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && !caught) {
            animator.Play("Idle");
            caught = true;
        }
        else if (other.gameObject.CompareTag("Home") && !itemGiven) {
            Invoke("Spawn", 0);
            itemGiven = true;
            GameManager.GM.addItem(itemIndex);
            GameManager.GM.increaseHouseLevel();
        }
    }

    void Spawn() {
        Instantiate(itemo, transform.position, Quaternion.identity);
    }
}
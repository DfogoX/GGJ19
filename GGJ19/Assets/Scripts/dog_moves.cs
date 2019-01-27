using System;
using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
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
    private AudioSource[] sources;

    void Start() {
        animator = GetComponentInChildren<Animator>();
        GameManager.GM.setDog(this.transform);
        sources = GetComponents<AudioSource>();

        StartCoroutine(RandomBark());
    }

    private IEnumerator RandomBark() {
        if (!GameManager.GM.spawningMobs()) {
            sources[0].Play();
        }

        yield return new WaitForSeconds(Random.Range(3.0f, 10.0f));
        StartCoroutine(RandomBark());
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
                sources[1].Play();
            }
        }

        if (currWayPoint == NumOfWayPoints - 1 && !keyGiven) {
            Invoke("Spawn", 0);
            GameManager.GM.addItem("Key");
            keyGiven = true;
        }

        if (currWayPoint > NumOfWayPoints) {
            animator.Play("DogIdleHappy");
            sources[1].Play();
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && !caught) {
            animator.Play("DogIdleHappy");
            sources[1].Play();
            caught = true;
        }
    }

    void Spawn() {
        Instantiate(_Item, transform.position, Quaternion.identity);
    }

    public void respawn() {
        transform.position = Vector3.left / 2 + new Vector3(0, -5, 0);
        GetComponentInChildren<Animator>().Play("DogCryingSad");
        Invoke(nameof(Sad), 2.0f);
    }

    void Sad() {
        sources[2].Play();
    }
}
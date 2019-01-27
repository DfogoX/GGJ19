using System.Collections;
using UnityEngine;

public class CutDownTree : MonoBehaviour {
    public Sprite openDoor;
    public GameObject item;

    private SpriteRenderer sr;
    public float timeTillDrop;

    // Start is called before the first frame update
    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (GameManager.GM.hasItem(0)) {
                sr.sprite = openDoor;
                Destroy(GetComponent<PolygonCollider2D>());
                Instantiate(item, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator DropLogs() {
        yield return new WaitForSeconds(timeTillDrop);
        Instantiate(item, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
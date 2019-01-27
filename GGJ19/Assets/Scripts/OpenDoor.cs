using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(AudioSource))]
public class OpenDoor : MonoBehaviour {

    public Sprite openDoor;
    public AudioClip LockedGateSound;
    public AudioClip UnlockSound;

    private SpriteRenderer sr;
    private AudioSource source;

    // Start is called before the first frame update
    void Start() {
        sr = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (GameManager.GM.hasItem(0)) {
                sr.sprite = openDoor;
                Destroy(GetComponent<PolygonCollider2D>());
                gameObject.AddComponent<PolygonCollider2D>();
                source.clip = UnlockSound;
                source.Play();
            }
            else
            {
                source.clip = LockedGateSound;
                source.Play();
            }
        }
    }
}
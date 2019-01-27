using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Audio.Google;

public class GettingInside : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
//        if (Input.GetKeyDown(KeyCode.Keypad1)) {
//            GameManager.GM.currentHouseLevel = 1;
//            Debug.Log("level1");
//        }
//
//        if (Input.GetKeyDown(KeyCode.Keypad2)) {
//            GameManager.GM.currentHouseLevel = 2;
//            Debug.Log("level2");
//        }

        if (GameManager.GM.inside) {
            Debug.Log(GameManager.GM.currentHouseLevel);
            transform.GetChild(0).gameObject.SetActive(false);
            for (int i = 1; i < transform.childCount; i++) {
                if (i == GameManager.GM.currentHouseLevel) {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                else {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        else {
            transform.GetChild(0).gameObject.SetActive(true);
            for (int i = 1; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
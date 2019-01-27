using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Family : MonoBehaviour {

    public GameObject dog;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GM.setFamily(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform getDog() {
        return this.transform;
    }

    public void respawnDog() {
        //dog.respawn();
    }

//        //return dog.respawn();
//    }
}

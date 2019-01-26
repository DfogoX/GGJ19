using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager GM;
    public Player player;

    private void Awake() {
        if( GM != null )
            Destroy( GM );
        else
            GM = this;
        DontDestroyOnLoad( GM );
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    public Transform findPlayer() {
        return GM.player.transform;
    }

}
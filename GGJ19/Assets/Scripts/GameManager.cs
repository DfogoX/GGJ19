using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager GM;
    private Player player;
    private EnemyManager enemManager;
    
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

    public void setPlayer(Player player) {
        this.player = player;
    }

    public void setEnemyManager(EnemyManager enemManager) {
        this.enemManager = enemManager;
    }

    public void activateSpawner() {
        GameManager.GM.enemManager.startSpawn();
    }
    
    public void deactivateSpawner() {
        GameManager.GM.enemManager.stopSpawn();
    }

}
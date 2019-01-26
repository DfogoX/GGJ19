using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager GM;
    private Player player;
    private EnemyManager enemManager;
    private Canvas canvas;

    //Key, Rope, Boia, Machado, Rock;
    private bool[] items = new bool[5];

    private void Awake() {
        if (GM != null)
            Destroy(GM);
        else
            GM = this;
        DontDestroyOnLoad(GM);
    }

    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < 5; i++) {
            items[i] = false;
        }
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

    public void setCanvas(Canvas canvas) {
        this.canvas = canvas;
    }

    public void activateSpawner() {
        GameManager.GM.enemManager.startSpawn();
    }

    public void deactivateSpawner() {
        GameManager.GM.enemManager.stopSpawn();
    }

    public void damagePlayer(int ammount) {
        if (GameManager.GM.player.takeDamage(ammount)) {
            Debug.Log("take heart");
            GameManager.GM.canvas.takeHeart();
        }

    }

    public void healPlayer(int ammount) {
        player.giveHeal(ammount);
        canvas.giveHeart();
    }

    public int playerHP() {
        return GameManager.GM.player.HP();
    }

    public void addItem(int index) {
        if (index < 5 && index >= 0) {
            items[index] = true;
        }
    }

    public void addItem(String item) {
        switch (item) {
            case "Key":
                items[0] = true;
                break;
            case "Chave":
                items[0] = true;
                break;
            case "Rope":
                items[1] = true;
                break;

            case "Corda":
                items[1] = true;
                break;

            case "Boia":
                items[2] = true;
                break;
            case "Floater":
                items[2] = true;
                break;
            case "Machado":
                items[3] = true;
                break;
            case "Axe":
                items[3] = true;
                break;
            case "Rock":
                items[4] = true;
                break;
            case "Pedra":
                items[4] = true;
                break;
            case "Comida":
                items[4] = true;
                break;
            case "Food":
                items[4] = true;
                break;
        }
    }

    public bool hasItem(int index) {
        if (index < 5 && index >= 0) {
            return items[index];
        }

        return false;
    }

    public bool hasItem(String item) {
        switch (item) {
            case "Key": return items[0];
            case "Chave": return items[0];
            case "Rope": return items[1];
            case "Corda": return items[1];
            case "Boia": return items[2];
            case "Floater": return items[2];
            case "Machado": return items[3];
            case "Axe": return items[3];
            case "Rock": return items[4];
            case "Pedra": return items[4];
            case "Comida": return items[4];
            case "Food": return items[4];
            default: return false;
        }
    }
}
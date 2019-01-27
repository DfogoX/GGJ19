using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager GM;
    private Player player;
    private Transform dog;
    private SceneHandler sceneHandler;
    private Family family;
    private EnemyManager enemManager;
    private CanvasUI canvas;
    private bool spawning = false;
    private int currentHouseLevel;
    public bool inside;
    private AudioSource[] sources;

    //Key, Rope, Boia, Machado, Rock;
    private bool[] items = new bool[5];

    private void Awake() {
        //if (GM != null)
        //    Destroy(GM);
        //else
        GM = this;
        DontDestroyOnLoad(GM);
    }

    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < 5; i++) {
            items[i] = false;
        }

        currentHouseLevel = 1;
        sources = GetComponents<AudioSource>();
    }

    public Transform findPlayer() {
        return GM.player.transform;
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public void setFamily(Family family1) {
        this.family = family1;
    }

    public void setSceneHandler(SceneHandler scene) {
        this.sceneHandler = scene;
    }

    public void setEnemyManager(EnemyManager enemManager) {
        this.enemManager = enemManager;
    }

    public void setCanvas(CanvasUI canvas) {
        this.canvas = canvas;
    }

    public void activateSpawner() {
        GameManager.GM.enemManager.startSpawn();
        spawning = true;
    }

    public void deactivateSpawner() {
        GameManager.GM.enemManager.stopSpawn();
        spawning = false;
    }

    public bool spawningMobs() {
        return spawning;
    }

    public void damagePlayer(int ammount) {
        if (GameManager.GM.player.takeDamage(ammount)) {
            GameManager.GM.canvas.takeHeart();
        }
    }

    public void healPlayer(int ammount) {
        bool healed = player.giveHeal(ammount);
        if (healed) {
            canvas.giveHeart();
        }
    }

    public void fullRestorePlayer() {
        player.getFullHP();
        canvas.giveFullHearts();
    }

    public int playerHP() {
        return GameManager.GM.player.HP();
    }

    public void changeSliderValue(float value) {
        canvas.changeSliderVal(value);
    }


    public void addItem(int index) {
        if (index < 5 && index >= 0) {
            items[index] = true;
            canvas.activate(index);
            sources[1].Play();
        }
    }

    public void addItem(String item) {
        var index = -1;
        sources[1].Play();
        switch (item) {
            case "Key":
            case "Chave":
                index = 0;
                items[0] = true;
                break;
            case "Rope":
            case "Corda":
                index = 1;
                items[1] = true;
                break;
            case "Boia":
            case "Floater":
                index = 2;
                items[2] = true;
                break;
            case "Machado":
            case "Axe":
                index = 3;
                items[3] = true;
                break;
            case "Rock":
            case "Pedra":
            case "Comida":
            case "Food":
                index = 4;
                items[4] = true;
                break;
        }

        canvas.activate(index);
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

    private void reset() {
        Debug.Log("End");
        canvas.BlackScreen();
        player.relive();
        dog.GetComponentInChildren<Animator>().Play("DogIdleHappy");
        family.respawn();
    }

    private IEnumerator waitForReset() {
        yield return new WaitForSeconds(1);
        reset();
    }

    private IEnumerator waitForBlackScreen1() {
        yield return new WaitForSeconds(1);
        canvas.BlackScreen();
        StartCoroutine(waitForBlackScreen());
    }

    private IEnumerator waitForBlackScreen() {
        yield return new WaitForSeconds(3);
        canvas.BlackScreen();
        StartCoroutine(waitForReset());
    }


    public void restart() {
        canvas.BlackScreen();
        this.player.respawn();
        fullRestorePlayer();
        dog.GetComponent<dog_moves>().respawn();
        family.stop();
        StartCoroutine(waitForBlackScreen1());
    }

    public void setDog(Transform d) {
        dog = d;
    }

    public void addTimmy(family_follow timmy) {
        family.addTimmy(timmy);
    }

    public void increaseHouseLevel() {
        currentHouseLevel++;
        if (currentHouseLevel >= 5) currentHouseLevel = 5;
        for (int i = 1; i < currentHouseLevel; i++) {
            familyChangeParent(i - 1, currentHouseLevel-1);
        }
        if (currentHouseLevel == 5) {
            familyChangeParent(4, currentHouseLevel-1);
        }
    }

    public bool isPlayerDead() {
        return player.isDead();
    }

    public int getHouseLevel() {
        return currentHouseLevel;
    }

    public void familyChangeParent(int index, int level) {
        family.changeParent(index,level);
    }
}
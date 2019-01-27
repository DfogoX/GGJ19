using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Family : MonoBehaviour {
    private family_follow mae;
    private family_follow avo;
    private family_follow avoo;
    private family_follow filho;

    private bool hasTimmy = false;
    private PaiFollow pai;

    public Transform[] Parents;
    // Start is called before the first frame update
    void Start() {
        GameManager.GM.setFamily(this);
        pai = this.transform.GetChild(0).GetComponent<PaiFollow>();
        mae = this.transform.GetChild(1).GetComponent<family_follow>();
        avoo = this.transform.GetChild(2).GetComponent<family_follow>();
        avo = this.transform.GetChild(3).GetComponent<family_follow>();
    }

    public Transform getFamily() {
        return this.transform;
    }

    public void respawn() {
        mae.respawn();
        pai.respawn();
        avo.respawn();
        avoo.respawn();
        if (hasTimmy) {
            filho.respawn();
        }
    }

    public void stop() {
        mae.stop();
        pai.stop();
        avo.stop();
        avoo.stop();
        if (hasTimmy) {
            filho.stop();
        }
    }

    public void addTimmy(family_follow timmy) {
        filho = timmy;
        hasTimmy = true;
    }

    public void changeParent(int index, int level) {
        switch (index) {
            case 0:
                mae.transform.SetParent(Parents[level], true);
                Debug.Log("Mae: " + level);
                break;
            case 1:
                filho.transform.SetParent(Parents[level], true);
                Debug.Log("Filho: " + level);

                break;
            case 2:
                pai.transform.SetParent(Parents[level], true);
                Debug.Log("Pai: " + level);
                break;
            case 3:
                avo.transform.SetParent(Parents[level], true);
                Debug.Log("Avo: " + level);
                break;
            case 4:
                avoo.transform.SetParent(Parents[level], true);
                Debug.Log("Avoo: " + level);
                break;
        }
    }
}
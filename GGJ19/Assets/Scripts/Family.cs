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
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("aafam");
        GameManager.GM.setFamily(this);
        pai = this.transform.GetChild(0).GetComponent<PaiFollow>();
        mae = this.transform.GetChild(1).GetComponent<family_follow>();
        avoo = this.transform.GetChild(2).GetComponent<family_follow>();
        avo = this.transform.GetChild(3).GetComponent<family_follow>();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.GM.setFamily(this);
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

}

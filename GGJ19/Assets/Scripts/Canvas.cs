using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour {

    private int lastHeartIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GM.setCanvas(this);
    }
    
    public void takeHeart() {
        if (lastHeartIndex < transform.childCount) {
            var child = this.transform.GetChild(lastHeartIndex);
            var childImg = child.transform.GetChild(0);
            var img = childImg.GetComponent<Image>();
            img.enabled = false;
            lastHeartIndex++;    
        }
        
    }

    public void giveHeart() {
        if (lastHeartIndex > 0 ) {
            var child = this.transform.GetChild(lastHeartIndex-1);
            var childImg = child.transform.GetChild(0);
            var img = childImg.GetComponent<Image>();
            img.enabled = true;
            lastHeartIndex--;    
        }
    }
}

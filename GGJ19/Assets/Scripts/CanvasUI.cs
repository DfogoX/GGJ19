using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour {

    private int lastHeartIndex = 0;
    private Transform inventory;
    private Transform blackScreen;

    public Sprite fullHeart;
    public Sprite emptyHeart;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GM.setCanvas(this);
        inventory = this.transform.GetChild(2);
        blackScreen = this.transform.GetChild(4);
        deactivateAll();
    }
    
    public void takeHeart() {
        if (lastHeartIndex < transform.GetChild(0).childCount) {
            var child = this.transform.GetChild(0).GetChild(lastHeartIndex);
            var childImg = child.transform.GetChild(0);
            var img = childImg.GetComponent<Image>();
            img.sprite = emptyHeart;
            lastHeartIndex++;    
        }
        
    }

    public void giveHeart() {
        if (lastHeartIndex > 0 ) {
            var child = this.transform.GetChild(0).GetChild(lastHeartIndex-1);
            var childImg = child.transform.GetChild(0);
            var img = childImg.GetComponent<Image>();
            img.sprite = fullHeart;
            lastHeartIndex--;
        }
    }

    public void changeSliderVal(float value) {
        var slide = this.transform.GetChild(1).GetComponent<Slider>();
        slide.value = value;
    }

    public void deactivateAll() {
        foreach (Transform item in inventory) {
            item.GetComponent<Image>().enabled = false;
        }
        activate(inventory.childCount-1);
    }
    
    public void activate(int index) {
        if (index < 0) return;
        inventory.GetChild(index).GetComponent<Image>().enabled = true;
        if (index > 0) {
            inventory.GetChild(index-1).GetComponent<Image>().enabled = false;
        }
    }
    public void BlackScreen() {
        blackScreen.GetComponent<Image>().enabled = !blackScreen.GetComponent<Image>().enabled;
    }
    
}

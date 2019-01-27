using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour {

    private int lastHeartIndex = 0;

    public Sprite fullHeart;
    public Sprite emptyHeart;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GM.setCanvas(this);
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
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaDelegate : MonoBehaviour {

    delegate void StamDelegate(float value);
    private StamDelegate myStam;

    // Start is called before the first frame update
    void Start() {
        myStam = updateSlider;


    }

    // Update is called once per frame
    void updateSlider(float value) {
        //Slider.value = value;
    }
}
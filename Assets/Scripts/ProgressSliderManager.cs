using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressSliderManager : MonoBehaviour
{
    public Slider slider;
    float slowStart;
    Color green = new Color(50f/255f, 233f/255f, 106f/255f);
    Color yellow = new Color(233f/255f, 184f/255f, 50f/255f);
    Color red = new Color(210f/255f, 41f/255f, 41f/255f);


    IEnumerator UpdateProgress() {
        for(;;)
        {
            slowStart = (float) Math.Max(0, (90000 - GameController.totalCarbonCredit) / 90000);
            slider.value = (float) (Math.Log(GameController.totalCarbonCredit,1.001f)*(1-slowStart) + Math.Log(GameController.totalCarbonCredit, 1.001f)*0.5*slowStart);
            //Debug.Log("Progress: " + slider.value);
            //Debug.Log("Progress: " + (Math.Log(GameController.totalCarbonCredit, 1.001f) * (1 - slowStart) + Math.Log(GameController.totalCarbonCredit, 1.001f) * 0.5 * slowStart));
            yield return new WaitForSeconds(.2f);
        }
    }

    void Start() {
        StartCoroutine(UpdateProgress());
    }
}

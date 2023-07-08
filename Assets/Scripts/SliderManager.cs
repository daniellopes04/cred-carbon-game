using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderManager : MonoBehaviour
{
    public TMP_Text valueText;
    public Slider slider;
    Color green = new Color(50f/255f, 233f/255f, 106f/255f);
    Color yellow = new Color(233f/255f, 184f/255f, 50f/255f);
    Color red = new Color(210f/255f, 41f/255f, 41f/255f);

    void OnSliderChanged(float value) {
        valueText.text = value.ToString();
    }

    IEnumerator UpdateProgress() {
        for(;;)
        {
            if (GameController.progress > 0)
            {
                RectTransform fillAreaRectTransform = slider.gameObject.transform.Find("Fill Area").GetComponent<RectTransform>();
                fillAreaRectTransform.anchorMin = new Vector2(0.5f, 0.25f);
                fillAreaRectTransform.anchorMax = new Vector2(1, 0.75f);
                slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = green;
                slider.direction = Slider.Direction.LeftToRight;
            }
            else
            {
                RectTransform fillAreaRectTransform = slider.gameObject.transform.Find("Fill Area").GetComponent<RectTransform>();
                fillAreaRectTransform.anchorMin = new Vector2(0, 0.25f);
                fillAreaRectTransform.anchorMax = new Vector2(0.5f, 0.75f);
                slider.direction = Slider.Direction.RightToLeft;
                if (GameController.progress < -25)
                    slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = red;
                else
                    slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = yellow;
            }


            slider.value = Math.Abs(GameController.progress);
            valueText.text = GameController.progress.ToString();
            yield return new WaitForSeconds(.2f);
        }
    }

    void Start() {

        StartCoroutine(UpdateProgress());
    }
}

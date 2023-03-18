using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderManager : MonoBehaviour
{
    public TMP_Text valueText;
    int progress = 0;
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
            if (progress >= 100) {
                break;
            }

            if (progress > 70 && progress < 90) {
                slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = yellow;
            } else if (progress >= 90) {
                slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = red;
            }

            progress++;
            slider.value = progress;
            valueText.text = progress.ToString();
            yield return new WaitForSeconds(.2f);
        }
    }

    void Start() {
        StartCoroutine(UpdateProgress());
    }
}

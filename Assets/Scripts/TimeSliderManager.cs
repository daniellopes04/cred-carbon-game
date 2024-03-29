using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSliderManager : MonoBehaviour
{
    public Slider slider;
    Color green = new Color(50f/255f, 233f/255f, 106f/255f);
    Color yellow = new Color(233f/255f, 184f/255f, 50f/255f);
    Color red = new Color(210f/255f, 41f/255f, 41f/255f);

    IEnumerator UpdateProgress() {
        for(;;)
        {
            if (GameController.year >= 2050) {
            }

            // if (GameController.year > 2034 && GameController.year < 2044) {
            //     slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = yellow;
            // } else if (GameController.year >= 2044) {
            //     slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = red;
            // }

            slider.value = GameController.year;
            yield return new WaitForSeconds(.2f);
        }
    }

    void Start() {
        StartCoroutine(UpdateProgress());
    }
}

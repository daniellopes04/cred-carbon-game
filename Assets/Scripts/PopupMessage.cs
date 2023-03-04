using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupMessage : MonoBehaviour
{
    public GameObject dialogUI;
    public GameObject actionUI;
    GameObject gameController;


    public void Open(string title, string body){
        dialogUI.SetActive(!dialogUI.activeSelf);

        if (dialogUI.activeSelf) {
            if (!string.IsNullOrEmpty(title)) {
                TMP_Text titleObject = dialogUI.gameObject.GetComponentsInChildren<TMP_Text>()[0];
                titleObject.text = title;
            }

            // if (!string.IsNullOrEmpty(body)) {
            //     TMP_Text bodyObject = dialogUI.gameObject.GetComponentsInChildren<TMP_Text>()[1];
            //     bodyObject.text = body;
            // }
            Time.timeScale = 0f;
        } 
    }   

    public void Close(){
        if (dialogUI.activeSelf) {
            dialogUI.SetActive(!dialogUI.activeSelf);
        } else {
            Time.timeScale = 1f;
        }

        if (actionUI.activeSelf) {
            actionUI.SetActive(!actionUI.activeSelf);
        } else {
            Time.timeScale = 1f;
        }
    }

    public void CloseMessageAndOpenAction(){
        dialogUI.SetActive(!dialogUI.activeSelf);

        if (!dialogUI.activeSelf) {
            Time.timeScale = 1f;
        }

        actionUI.SetActive(!actionUI.activeSelf);

        if (!actionUI.activeSelf) {
            Time.timeScale = 1f;
        }
    }

    public void ExecuteAction(){
        GameController.carbonCredit++;
    }
}

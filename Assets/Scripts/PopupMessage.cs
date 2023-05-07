using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupMessage : MonoBehaviour
{
    public GameObject previousActionUI;
    public GameObject actionUI;
    public GameObject nextActionUI;

    public void OpenCloseAction() {
        actionUI.SetActive(!actionUI.activeSelf);

        if (!actionUI.activeSelf) {
            Time.timeScale = 1f;
        }
    }

    public void NextActionPanel() {
        if (nextActionUI != null) {
            nextActionUI.SetActive(!nextActionUI.activeSelf);
            OpenCloseAction();
        }
    }

    public void PreviousActionPanel() {
        if (previousActionUI != null) {
            previousActionUI.SetActive(!previousActionUI.activeSelf);
            OpenCloseAction();
        }
    }
}

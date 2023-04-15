using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupMessage : MonoBehaviour
{
    public GameObject actionUI;

    public void OpenCloseAction() {
        actionUI.SetActive(!actionUI.activeSelf);

        if (!actionUI.activeSelf) {
            Time.timeScale = 1f;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }

public class MouseManager : MonoBehaviour
{
    // Know what objects are clickable
    public LayerMask clickableLayer;
    string[] speedControlButtons = {"Pause", "Normal Speed", "Fast Speed", "Very Fast Speed"};
    Color white = new Color(1, 1, 1, 150/255f);
    Color black = new Color(0, 0, 0, 180/255f);

    // Normal cursor
    public Texture2D pointer;
    // Target cursor (for clickable objects)
    public Texture2D target;
    Vector2 vector = new Vector2(16, 16);

    void Start()
    {
        Cursor.SetCursor(pointer, vector, CursorMode.Auto);
    }

    public void OnMouseOver() {
        Cursor.SetCursor(target, vector, CursorMode.Auto);
    }

    public void OnMouseExit() {
        Cursor.SetCursor(pointer, vector, CursorMode.Auto);
    }

    public void OnSpeedControlClick(GameObject control) {
        Image image = control.GetComponent<Image>();
        image.color = white;

        foreach (string speedControl in speedControlButtons) {
            if (control.name != speedControl) {
                GameObject.Find(speedControl).GetComponent<Image>().color = black;
            }
        }
    }
}

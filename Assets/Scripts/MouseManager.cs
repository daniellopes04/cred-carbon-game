using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Runtime.InteropServices;

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }

public class MouseManager : MonoBehaviour
{
    // Know what objects are clickable
    public LayerMask clickableLayer;
    
    // Swap cursors per object
    public Texture2D pointer;   // Normal cursor
    public Texture2D target;    // Target cursor (for clickable objects)

    public TMP_Text ccCounter; 
    public GameObject dialogUI;
    public GameObject actionUI;

    GameObject gameController;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value) 
            && !dialogUI.activeSelf
            && !actionUI.activeSelf)
        {
            if (hit.collider.gameObject.tag == "Clickable")
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }
            else {
                Cursor.SetCursor(pointer, new Vector2(16, 16), CursorMode.Auto);
            }
        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

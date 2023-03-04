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
    //public Texture2D doorway;   // Cursor for doorways
    //public Texture2D combat;    // Cursor for combat actions

    public TMP_Text ccCounter; 
    public GameObject dialogUI;
    public GameObject actionUI;

    //public EventVector3 OnClickEnvironment;


    GameObject gameController;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value) 
            && !dialogUI.activeSelf
            && !actionUI.activeSelf)
        {
            bool region = false;

            if (hit.collider.gameObject.tag == "Region")
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                region = true;
            }
            else {
                Cursor.SetCursor(pointer, new Vector2(16, 16), CursorMode.Auto);
            }

            // 0 is left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                gameController = GameObject.Find("GameController");
                PopupMessage popupMessage = dialogUI.GetComponent<PopupMessage>();

                if (region) 
                {    
                    popupMessage.Open(hit.collider.gameObject.name, "This is " + hit.collider.gameObject.name + "!");
                    Debug.Log("REGION");
                    //GameController.carbonCredit++;
                }
                else 
                {
                    Debug.Log("MAP");
                }
            }
        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

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

    //public EventVector3 OnClickEnvironment;

    PopupMessage popupMessage;
    GameObject gameController;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        int ccValue = Int32.Parse(ccCounter.text);

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
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
                popupMessage = gameController.GetComponent<PopupMessage>();

                if (region) 
                {    
                    popupMessage.Open(hit.collider.gameObject.name, "This is " + hit.collider.gameObject.name + "!");
                    Debug.Log("REGION");
                    ccValue++;
                    ccCounter.text = ccValue.ToString();
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

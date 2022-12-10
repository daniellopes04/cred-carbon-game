using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    //public EventVector3 OnClickEnvironment;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
        {
            bool region1 = false;
            bool region2 = false;
            bool region3 = false;
            bool region4 = false;
            bool region5 = false;
            bool region6 = false;
            bool region7 = false;

            if (hit.collider.gameObject.tag == "Region1")
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                region1 = true;
            }
            else if (hit.collider.gameObject.tag == "Region2")
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                region2 = true;
            }
            else if (hit.collider.gameObject.tag == "Region3")
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                region3 = true;
            }
            else if (hit.collider.gameObject.tag == "Region4")
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                region4 = true;
            }
            else if (hit.collider.gameObject.tag == "Region5")
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                region5 = true;
            }
            else if (hit.collider.gameObject.tag == "Region6")
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                region6 = true;
            }
            else if (hit.collider.gameObject.tag == "Region7")
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                region7 = true;
            }
            else {
                Cursor.SetCursor(pointer, new Vector2(16, 16), CursorMode.Auto);
            }

            // 0 is left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                if (region1) 
                {
                    Debug.Log("REGION 1");
                }
                else if (region2) 
                {
                    Debug.Log("REGION 2");
                }
                else if (region3) 
                {
                    Debug.Log("REGION 3");
                }
                else if (region4) 
                {
                    Debug.Log("REGION 4");
                }
                else if (region5) 
                {
                    Debug.Log("REGION 5");
                }
                else if (region6) 
                {
                    Debug.Log("REGION 6");
                }
                else if (region7) 
                {
                    Debug.Log("REGION 7");
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

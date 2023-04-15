using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CanvasController : MonoBehaviour
{
    public Sprite greenButtonSprite;
    public Sprite greyButtonSprite;
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    bool isActionOn = false;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.tag == "ActionButton")
                {
                    Image buttonImage = result.gameObject.GetComponent<Image>();
                    if (buttonImage.sprite.name == "SquareButton_green") {
                        buttonImage.sprite = greyButtonSprite;
                        isActionOn = false;
                    } else if (!isActionOn) {
                        buttonImage.sprite = greenButtonSprite;
                        isActionOn = true;
                    }
                }
                Debug.Log("Hit " + result.gameObject.name);
            }
        }
    }
}

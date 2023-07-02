using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CanvasController : MonoBehaviour
{
    public Sprite greenButtonSprite;
    public Sprite greyButtonSprite;
    public List<Button> actionLockedButtons;
    public List<Button> actionButtons;
    public List<Sprite> actionSprites;
    public List<GameObject> popUpBadges;
    static int openBadgeIndex = -1;
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    bool isActionOn = false;
    Image imageOn;


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
                if (result.gameObject.tag == "ActionButton" && result.gameObject.GetComponent<Button>().interactable)
                {
                    Image buttonImage = result.gameObject.GetComponent<Image>();

                    if (buttonImage.sprite.name == "SquareButton_green") {
                        buttonImage.sprite = greyButtonSprite;
                        isActionOn = false;
                        imageOn = null;
                    } else {
                        buttonImage.sprite = greenButtonSprite;

                        if (isActionOn && imageOn != null) {
                            imageOn.sprite = greyButtonSprite;
                        }
                        
                        isActionOn = true;
                        imageOn = result.gameObject.GetComponent<Image>();
                    }
                }
                Debug.Log("Hit " + result.gameObject.name);
            }
        }

        if (GameController.GetActionInProgress()) {
            OpenPopUpBadge();
        } else {
            ClosePopUpBadge();
        }
    }

    public void OpenPopUpBadge() {  
        if (openBadgeIndex == -1) {
            var rnd = new System.Random();
            openBadgeIndex = rnd.Next(0, popUpBadges.Count - 1);

            Image popUpImage = popUpBadges[openBadgeIndex].GetComponent<Image>();
            Animator anim = popUpBadges[openBadgeIndex].GetComponent<Animator>();

            popUpImage.sprite = actionSprites[GameController.GetAction() - 1];
            anim.SetTrigger("Open");
        }
    }

    public static bool IsBadgeOpen() {
        if (openBadgeIndex != -1) {
            return true;
        }
        return false;
    }

    public void ClosePopUpBadge() {
        if (openBadgeIndex != -1) {
            Image popUpImage = popUpBadges[openBadgeIndex].GetComponent<Image>();
            Animator anim = popUpBadges[openBadgeIndex].GetComponent<Animator>();

            anim.SetTrigger("Close");
            openBadgeIndex = -1;
        }
    }

    public void UnlockButton(int actionID) {
        bool canAfford = false;

        switch (actionID)
        {
            case 1:
                canAfford = Actions.Solar.CanAffordUpgrade();
                break;
            case 2:
                canAfford = Actions.Wind.CanAffordUpgrade();
                break;
        }

        if (canAfford) {
            actionLockedButtons[actionID - 1].gameObject.SetActive(false);
            actionButtons[actionID - 1].gameObject.SetActive(true);
            actionButtons[actionID - 1].interactable = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerInput : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    //There is still some work to do here, including being a bit more dynamic.
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        //Get the shopping list and hide it
        GameObject.Find("ShoppingList").GetComponent<Renderer>().enabled = false;

        //Get the list of shoppinglist items and hide them
        for(int i = 0; i < GameObject.Find("ShoppingList").transform.childCount; i++)
        {
            GameObject.Find("ShoppingList").transform.GetChild(i).GetComponent<Renderer>().enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.GetAxis() != Vector2.zero)
        {
            print(gameObject.name + " ------- " + Controller.GetAxis());
        }

 
        if (Controller.GetHairTriggerDown())
        {
            print(gameObject.name + " Trigger Press");

        }

        if (Controller.GetHairTriggerUp())
        {
            print(gameObject.name + " Trigger Release");

        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //Toggle visibility of shopping list and items when the user presses the touchpad on the right controller
            GameObject.Find("ShoppingList").GetComponent<Renderer>().enabled = !GameObject.Find("ShoppingList").GetComponent<Renderer>().enabled;

            for (int i = 0; i < GameObject.Find("ShoppingList").transform.childCount; i++)
            {
                GameObject.Find("ShoppingList").transform.GetChild(i).GetComponent<Renderer>().enabled = !GameObject.Find("ShoppingList").transform.GetChild(i).GetComponent<Renderer>().enabled;
            }

        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            print(gameObject.name + " Grip Release");

        }
    }
}

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

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        //Get the shopping list GameObject and hide it
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
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //Toggle visibility of shopping list and items when the user presses the touchpad on the controller
            GameObject.Find("ShoppingList").GetComponent<Renderer>().enabled = !GameObject.Find("ShoppingList").GetComponent<Renderer>().enabled;
            var inventory = GetComponent<ProductInventory>();

            for (int i = 0; i < GameObject.Find("ShoppingList").transform.childCount; i++)
            {
                var id = GameObject.Find("ShoppingList").transform.GetChild(i).GetComponent<ProductId>().id;
                
                if(inventory.IsShoppingListItem(id))
                {
                    GameObject.Find("ShoppingList").transform.GetChild(i).GetComponent<Renderer>().enabled = !GameObject.Find("ShoppingList").transform.GetChild(i).GetComponent<Renderer>().enabled;
                }                              
            }
        }
    }
}

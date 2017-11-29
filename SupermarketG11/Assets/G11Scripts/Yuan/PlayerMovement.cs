using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    private SteamVR_TrackedController controller;

    public float m_Speed = 6.0F;
    public float m_TurnSpeed = 6.0F;
    public float gravity = 20.0F;

    private float m_MovementInputValue;
    private float m_TurnInputValue;

    public CharacterController player;

    // Use this for initialization
    void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        controller = GetComponent<SteamVR_TrackedController>();
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (controller.padPressed) {
            float x = device.GetAxis().x;
            float y = device.GetAxis().y;

            Vector3 forward;

            if (x != 0 || y != 0)
            {

                int areaTag = area(x, y);

                if (areaTag == 1 || areaTag == 3)
                {
                    forward = controller.transform.TransformDirection(Vector3.forward);
                    float curSpeed = m_Speed * y;
                    player.SimpleMove(forward * curSpeed);
                }
                else if (areaTag == 2 || areaTag == 4) {
                    player.transform.Rotate(0, x * m_TurnSpeed, 0);
                }
            }
        }
    }

    int area(float x, float y)
    {

        int area_number = 0;
        bool move = false; //true: move, false: rotate

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            move = false;
        }
        else
        {
            move = true;
        }

        if (move)
        {
            if (y > 0)
            {
                area_number = 1;
            }
            else
            {
                area_number = 3;
            }
        }
        else
        {
            if (x > 0)
            {
                area_number = 4;
            }
            else
            {
                area_number = 2;
            }
        }

        return area_number;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement33 : MonoBehaviour
{


    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    private SteamVR_TrackedController controller;

    public float m_Speed = 2.0F;
    public float m_TurnSpeed = 2.0F;
    public float gravity = 20.0F;

    private float m_MovementInputValue;
    private float m_TurnInputValue;

    public CharacterController player;

    private Vector3 position = Vector3.zero;

    public GameObject tollary;
    //public int Limit = 10;
    private List<Vector3> HistoryPos;
    private List<Quaternion> HistoryRot;
    private bool timebreak = false;
    private bool record = false;
    private TimeSpan elapsedTime;
    private int step = 1;

    // Use this for initialization
    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        controller = GetComponent<SteamVR_TrackedController>();
        HistoryPos = new List<Vector3>();
        HistoryRot = new List<Quaternion>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (tollary.GetComponent<Tmove>().touch == 0)
        {
            if (controller.padPressed)
            {
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
                    else if (areaTag == 2 || areaTag == 4)
                    {
                        player.transform.Rotate(0, x * m_TurnSpeed, 0);
                    }
                }

                step = 1;
                if (record == false)
                {
                    //time = System.DateTime.Now;
                    elapsedTime = new TimeSpan(DateTime.Now.Ticks);
                    record = true;
                }
                TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan ts = ts2.Subtract(elapsedTime).Duration();

                if (record == true && ts.Seconds >= 1)
                {
                    if (tollary.GetComponent<Tmove>().touch == 0)
                    {

                        if (HistoryPos.Count > 5)
                        {
                            HistoryPos.RemoveAt(0);
                            HistoryRot.RemoveAt(0);
                        }

                        position = controller.GetComponent<Transform>().position;
                        //position.y = 0.5f;
                        Quaternion rot = transform.rotation;

                        HistoryPos.Add(position);
                        HistoryRot.Add(rot);
                        Debug.Log("记录在位置 " + HistoryPos.Count);

                        record = false;
                    }
                }
            }
        }
        else
        {
            if (HistoryPos.Count > 0)
            {
                Debug.Log("循环论多少遍啊" + step);
                int index = HistoryPos.Count - step;
                if (index < 0) { step = 1; index = HistoryPos.Count - step; }
                Vector3 temp=HistoryPos[index];
                temp.y = -0.3f;
                player.GetComponent<Transform>().position = temp;

            }
            if (HistoryRot.Count > 0)
            {
                int index = HistoryRot.Count - step;
                this.transform.rotation = HistoryRot[index];
            }
            //Debug.Log("循环多少遍啊" + step);
            if (step < 5) { step++; }            
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

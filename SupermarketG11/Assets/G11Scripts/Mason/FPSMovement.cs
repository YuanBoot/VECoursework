using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FPSMovement : MonoBehaviour {
	
	public float m_Speed = 2.0F;
	public float m_TurnSpeed = 2.0F;
	public float gravity = 20.0F;

	private float m_MovementInputValue;    
	private float m_TurnInputValue;   

	private CharacterController controller;
    public GameObject tollary;
    private Vector3 position = Vector3.zero;
    //private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

    private Vector3 moveDirection = Vector3.zero;

    //时间倒流
    public int Limit = 50;
    private List<Vector3> HistoryPos;
    private List<Quaternion> HistoryRot;
    private bool timebreak = false;
    public int time=1;

    private void Start()
	{

		controller = GetComponent<CharacterController>();
        HistoryPos= new List<Vector3>();
        HistoryRot = new List<Quaternion>();
    }

	void FixedUpdate() {
       // Debug.Log("它就不会变吗 " + timebreak);
        if (timebreak == true)
        {
            TimeBack();

        }
        else
        {
            //Debug.Log("就没有等于0的时候吗 " + tollary.GetComponent<Tmove>().touch);
           // if (tollary.GetComponent<Tmove>().touch == 0 ||timebreak == false)
            
            if (tollary.GetComponent<Tmove>().touch == 0 )
            {

                m_TurnInputValue = Input.GetAxis("Horizontal");
                m_MovementInputValue = Input.GetAxis("Vertical");

                transform.Rotate(0, Input.GetAxis("Horizontal") * m_TurnSpeed, 0);

                Vector3 forward = transform.TransformDirection(Vector3.forward);

                float curSpeed = m_Speed * Input.GetAxis("Vertical");

                controller.SimpleMove(forward * curSpeed);

                position = controller.GetComponent<Transform>().position;
                Quaternion rot = transform.rotation;

                HistoryPos.Add(position);
                HistoryRot.Add(rot);

                if (HistoryPos.Count > Limit)
                {
                    HistoryPos.RemoveAt(0);
                    HistoryRot.RemoveAt(0);
                }
            }
            else
            {

                timebreak = true;

            }

        }
    }

    void Move()
    {

    }
    void TimeBack()
    {
        
            Debug.Log("我想知道运行几次 " + time);
            if (HistoryPos.Count > 0)
            {
                int index = HistoryPos.Count - time;
                controller.GetComponent<Transform>().position = HistoryPos[index];
                 HistoryPos.RemoveAt(index);


            }
            if (HistoryRot.Count > 0)
            {
                int index = HistoryRot.Count - time;
                this.transform.rotation = HistoryRot[index];
                HistoryRot.RemoveAt(index);

            }
            timebreak = false;
           
        
        if (time == 5) {
            timebreak = false;
            Debug.Log("为什么还会进入timebreak是true的情况? " + timebreak);
            time = 5;
        }
        
    }
}
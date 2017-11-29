using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandpreController : MonoBehaviour {

	//A reference to the object being tracked. In this case, a controller.
	private SteamVR_TrackedObject trackedObj;

	//A Device property to provide easy access to the controller. 
	//It uses the tracked object’s index to return the controller’s input.

	public float m_Speed = 2.0F;
	public float m_TurnSpeed = 2.0F;
	public float gravity = 20.0F;

	private float m_MovementInputValue;    
	private float m_TurnInputValue;

    public CharacterController controller;

	private Vector3 moveDirection = Vector3.zero;

    private Vector3 position = Vector3.zero;
    public GameObject tollary;
    public int Limit = 100;
    private List<Vector3> HistoryPos;
    private List<Quaternion> HistoryRot;
    private bool timebreak = false;
    public int time = 1;

	private void Start()
	{

		//controller = GetComponent<CharacterController>();
        HistoryPos = new List<Vector3>();
        HistoryRot = new List<Quaternion>();

	}

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Update is called once per frame
	void Update () {
		// 1
        if (timebreak == true)//1
        {
            //Debug.Log("44445466444");
            TimeBack();

        }
        else
        {
            if (Controller.GetAxis() != Vector2.zero)
            {
                //Debug.Log(gameObject.name + Controller.GetAxis().x);

                //m_TurnInputValue = Controller.GetAxis().y;
                //m_MovementInputValue = Controller.GetAxis().x;

                
                if (tollary.GetComponent<Tmove>().touch == 0)
                {
                  //  Debug.Log("44445466444");

               m_TurnInputValue = Controller.GetAxis().y;
               m_MovementInputValue = Controller.GetAxis().x;

                controller.transform.Rotate(0, m_MovementInputValue * m_TurnSpeed, 0);

                Vector3 forward = controller.transform.TransformDirection(Vector3.forward);

                float curSpeed = m_Speed * m_TurnInputValue;

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
	}

    void TimeBack()
    {

        // Debug.Log("我想知道运行几次 " + time);
        if (HistoryPos.Count > 0)
        {
            int index = HistoryPos.Count - time;
            controller.GetComponent<Transform>().position = HistoryPos[index];
            //HistoryPos.RemoveAt(index);


        }
        if (HistoryRot.Count > 0)
        {
            int index = HistoryRot.Count - time;
            //person.GetComponent<Transform>().rotation = HistoryRot[index];
            this.transform.rotation = HistoryRot[index];           //键盘控制里的情况是这样
           // HistoryRot.RemoveAt(index);

        }
        //timebreak = false;
        time++;
        if (tollary.GetComponent<Tmove>().touch == 0) { timebreak = false; time = 0; }

    }

}


/*
		public const ulong System			= (1ul << (int)EVRButtonId.k_EButton_System); // reserved
		public const ulong ApplicationMenu	= (1ul << (int)EVRButtonId.k_EButton_ApplicationMenu);
		public const ulong Grip				= (1ul << (int)EVRButtonId.k_EButton_Grip);
		public const ulong Axis0			= (1ul << (int)EVRButtonId.k_EButton_Axis0);
		public const ulong Axis1			= (1ul << (int)EVRButtonId.k_EButton_Axis1);
		public const ulong Axis2			= (1ul << (int)EVRButtonId.k_EButton_Axis2);
		public const ulong Axis3			= (1ul << (int)EVRButtonId.k_EButton_Axis3);
		public const ulong Axis4			= (1ul << (int)EVRButtonId.k_EButton_Axis4);
		public const ulong Touchpad			= (1ul << (int)EVRButtonId.k_EButton_SteamVR_Touchpad);
		public const ulong Trigger			= (1ul << (int)EVRButtonId.k_EButton_SteamVR_Trigger);
*/

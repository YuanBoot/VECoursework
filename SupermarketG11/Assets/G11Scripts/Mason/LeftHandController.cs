using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandController : MonoBehaviour {

	
	private SteamVR_TrackedObject trackedObj;


	public float m_Speed = 2.0F;
	public float m_TurnSpeed = 2.0F;
	public float gravity = 20.0F;

	private float m_MovementInputValue;    
	private float m_TurnInputValue;

    public GameObject tollary;
    private Vector3 position = Vector3.zero;
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

    public CharacterController person;//人称控制器改名为person

	private Vector3 moveDirection = Vector3.zero;

    //不要撞墙
    public int Limit = 50;
    private List<Vector3> HistoryPos;
    private List<Quaternion> HistoryRot;
    private bool timebreak = false;
    public int time = 1;


    private void Start()
	{
        //controller = GetComponent<CharacterController>();//为什么这个注释掉
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
        if (timebreak == true)//1
        {
            TimeBack();

        }
        else
        {
            if (Controller.GetTouch(touchpad))//2-----------1和2可能会调下顺序
            {
                m_TurnInputValue = Controller.GetAxis().y;
                m_MovementInputValue = Controller.GetAxis().x;

                if (tollary.GetComponent<Tmove>().touch == 0)
                {
                    person.transform.Rotate(0, m_MovementInputValue * m_TurnSpeed, 0);

                    Vector3 forward = person.transform.TransformDirection(Vector3.forward);


                    float curSpeed = m_Speed * m_TurnInputValue;

                    person.SimpleMove(forward * curSpeed);

                    position = person.GetComponent<Transform>().position;
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
            person.GetComponent<Transform>().position = HistoryPos[index];
            HistoryPos.RemoveAt(index);


        }
        if (HistoryRot.Count > 0)
        {
            int index = HistoryRot.Count - time;
            //person.GetComponent<Transform>().rotation = HistoryRot[index];
            this.transform.rotation = HistoryRot[index];           //键盘控制里的情况是这样
            HistoryRot.RemoveAt(index);

        }
        timebreak = false;

    }


}



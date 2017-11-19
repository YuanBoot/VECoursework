using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerTest : MonoBehaviour {

	//A reference to the object being tracked. In this case, a controller.
	private SteamVR_TrackedObject trackedObj;

	//A Device property to provide easy access to the controller. 
	//It uses the tracked object’s index to return the controller’s input.

	public float m_Speed = 6.0F;
	public float m_TurnSpeed = 6.0F;
	public float gravity = 20.0F;

	private float m_MovementInputValue;    
	private float m_TurnInputValue;   

	public CharacterController controller;

	private Vector3 moveDirection = Vector3.zero;

	private void Start()
	{

		controller = GetComponent<CharacterController>();

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
		if (Controller.GetAxis() != Vector2.zero)
		{
			//Debug.Log(gameObject.name + Controller.GetAxis());

			m_TurnInputValue = Input.GetAxis("Horizontal");
			m_MovementInputValue = Input.GetAxis ("Vertical");

			transform.Rotate(0, Input.GetAxis("Horizontal") * m_TurnSpeed, 0);

			Vector3 forward = transform.TransformDirection(Vector3.forward);

			float curSpeed = m_Speed * Input.GetAxis("Vertical");

			controller.SimpleMove(forward * curSpeed);

		}

		/*
		// 4
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			Debug.Log(gameObject.name + " Grip Press");
		}

		// 5
		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
		{
			Debug.Log(gameObject.name + " Grip Release");
		}

		// Axis0
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Axis0))
		{
			Debug.Log(gameObject.name + " Axis0 Press");
		}


		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Axis0))
		{
			Debug.Log(gameObject.name + " Axis0 Release");
		}

		// Axis1
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Axis1))
		{
			Debug.Log(gameObject.name + " Axis1 Press");
		}


		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Axis1))
		{
			Debug.Log(gameObject.name + " Axis1 Release");
		}
		*/
			
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

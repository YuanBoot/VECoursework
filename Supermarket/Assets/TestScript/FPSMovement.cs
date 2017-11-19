using UnityEngine;
using System.Collections;

public class FPSMovement : MonoBehaviour {
	
	public float m_Speed = 6.0F;
	public float m_TurnSpeed = 6.0F;
	public float gravity = 20.0F;

	private float m_MovementInputValue;    
	private float m_TurnInputValue;   

	private CharacterController controller;

	private Vector3 moveDirection = Vector3.zero;

	private void Start()
	{

		controller = GetComponent<CharacterController>();

	}

	void Update() {
		
		m_TurnInputValue = Input.GetAxis("Horizontal");
		m_MovementInputValue = Input.GetAxis ("Vertical");

		transform.Rotate(0, Input.GetAxis("Horizontal") * m_TurnSpeed, 0);

		Vector3 forward = transform.TransformDirection(Vector3.forward);

		float curSpeed = m_Speed * Input.GetAxis("Vertical");

		controller.SimpleMove(forward * curSpeed);
	
	}

}
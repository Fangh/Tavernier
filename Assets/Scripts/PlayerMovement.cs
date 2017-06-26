using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 rotationDirection = Vector3.zero;

	public Transform LookAtTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	void Update()
	{
		CharacterController controller = GetComponent<CharacterController>();
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		controller.Move(moveDirection * Time.deltaTime);

		rotationDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		if (rotationDirection != Vector3.zero)
		{
			float angle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
			transform.GetChild(0).transform.rotation = Quaternion.Euler(0, angle, 0);
		}
	}
}

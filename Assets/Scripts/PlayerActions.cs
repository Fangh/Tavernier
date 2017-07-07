using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Collections;

public class PlayerActions : MonoBehaviour
{
	public GameObject LeftHand;
	public GameObject RightHand;

	public float slotRange = 2f;
	public Color unSelectedColor = Color.white;
	public Color selectedColor = Color.green;
	

	GameObject actionZone = null;
	SlotManager currentTargetedSlot = null;
	bool isUsingPump = false;

	// Use this for initialization
	void Start ()
	{
		
	}

	private void FixedUpdate()
	{
		if (null != currentTargetedSlot)
		{
			//Debug.Log(Vector3.Distance(transform.position, currentTargetedSlot.transform.position));
			if( Vector3.Distance(transform.position, currentTargetedSlot.target.transform.position) > slotRange )
			{
				currentTargetedSlot.UnTargetMe(unSelectedColor);
				currentTargetedSlot = null;
			}
		}

		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.GetChild(0).forward, out hit, 10f, LayerMask.GetMask("Target")))
		{
			//Debug.Log(hit.collider.tag);
			if (null != currentTargetedSlot)
			{
				if (currentTargetedSlot.gameObject.GetInstanceID() == hit.collider.transform.parent.gameObject.GetInstanceID())
					return;
				else
				{
					currentTargetedSlot.UnTargetMe(unSelectedColor);
					currentTargetedSlot = null;
				}
			}
			else
			{
				currentTargetedSlot = hit.collider.transform.parent.GetComponent<SlotManager>();
				currentTargetedSlot.TargetMe(selectedColor);
			}		
		}
	}

	// Update is called once per frame
	void Update ()
	{

		//Open Hands


		//drop in slot or on the ground
		if (Input.GetButtonUp("LeftHand") && IsHandFull("LeftHand"))
		{
			GameObject item = LeftHand.transform.GetChild(0).gameObject;
			if (currentTargetedSlot == null || currentTargetedSlot.itemInSlot != null)
			{
				Debug.Log("you have released what was in your left hand");
				item.GetComponent<Rigidbody>().useGravity = true;
				item.GetComponent<Rigidbody>().isKinematic = false;
				item.transform.SetParent(null);
				item.GetComponent<Pint>().DestroyIn(3f, false);
			}
			else
			{
				currentTargetedSlot.AddDrinkInSlot(item);
			}		
		}


		//drop in slot or on the ground
		if (Input.GetButtonUp("RightHand") && IsHandFull("RightHand"))
		{
			GameObject item = RightHand.transform.GetChild(0).gameObject;
			if (currentTargetedSlot == null || currentTargetedSlot.itemInSlot != null)
			{
				Debug.Log("you have released what was in your RightHand");
				item.GetComponent<Rigidbody>().useGravity = true;
				item.GetComponent<Rigidbody>().isKinematic = false;
				item.transform.SetParent(null);
				item.GetComponent<Pint>().DestroyIn(3f, false);
			}
			else
			{
				currentTargetedSlot.AddDrinkInSlot(item);
			}
		}

		if ( (Input.GetButtonUp("RightHand") ||Input.GetButtonUp("LeftHand")) && isUsingPump)
		{
			isUsingPump = false;
			GetComponent<PlayerMovement>().enabled = true;
		}


		//Close Hands

		if (Input.GetButtonDown("LeftHand") && !IsHandFull("LeftHand"))
		{
			//take in stockpile
			if (null != actionZone && actionZone.GetComponent<Stockpile>())
				TakeInHand(LeftHand, actionZone.GetComponent<Stockpile>().GetItem());

			//take from a slot
			if (null != currentTargetedSlot && null != currentTargetedSlot.itemInSlot)
			{
				TakeInHand(LeftHand, currentTargetedSlot.itemInSlot);
				currentTargetedSlot.Take();
			}

		}

		if (Input.GetButton("LeftHand") && !IsHandFull("LeftHand"))
		{
			//use pump
			if (null != actionZone && actionZone.GetComponent<Pump>())
			{
				isUsingPump = true;
				GetComponent<PlayerMovement>().enabled = false;
				if (IsHandFull("RightHand"))
				{
					actionZone.GetComponent<Pump>().Fill(RightHand.transform.GetChild(0).GetComponent<Pint>());
				}
			}
		}

		if (Input.GetButtonDown("RightHand") && !IsHandFull("RightHand"))
		{
			//take in stockpile
			if (null != actionZone && actionZone.GetComponent<Stockpile>())
				TakeInHand(RightHand, actionZone.GetComponent<Stockpile>().GetItem());

			//take on a slot
			if (null != currentTargetedSlot && null != currentTargetedSlot.itemInSlot)
			{
				TakeInHand(RightHand, currentTargetedSlot.itemInSlot);
				currentTargetedSlot.Take();
			}
				
		}

		if (Input.GetButton("RightHand") && !IsHandFull("RightHand"))
		{
			//use pump
			if (null != actionZone && actionZone.GetComponent<Pump>())
			{
				isUsingPump = true;
				GetComponent<PlayerMovement>().enabled = false;
				if (IsHandFull("LeftHand"))
				{
					actionZone.GetComponent<Pump>().Fill(LeftHand.transform.GetChild(0).GetComponent<Pint>());
				}
			}
		}
	}


	void TakeInHand( GameObject hand, GameObject o )
	{
		Debug.Log("you have taken a" + o.name + "in your " + hand.name);
		o.GetComponent<Rigidbody>().useGravity = false;
		o.GetComponent<Rigidbody>().isKinematic = true;
		o.transform.SetParent(hand.transform);
		o.transform.localPosition = Vector3.zero;
		o.transform.localRotation = Quaternion.identity;
	}

	private bool IsHandFull( string hand )
	{
		if (hand == "LeftHand")
			return LeftHand.transform.childCount > 0;
		if (hand == "RightHand")
			return RightHand.transform.childCount > 0;
		else
		{
			Debug.LogError("you are trying to know if there is something in a unknown Hand");
			return false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (null != actionZone)
			return;

		Debug.Log(other.name + "has entered in " + name);
		if (null != other.GetComponent<ActionZone>())
		{
			Debug.Log("you have entered a "+ other.name +" Zone");
			actionZone = other.gameObject;
			actionZone.GetComponent<ActionZone>().Enter(selectedColor);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (null != actionZone && 
		    null != other.GetComponent<ActionZone>())
		{
			Debug.Log("you have leaved a "+ other.name +" Zone");
			actionZone.GetComponent<ActionZone>().Leave(unSelectedColor);
			actionZone = null;
		}
	}
}

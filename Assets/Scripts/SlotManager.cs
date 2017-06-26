using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
	
	public GameObject target = null;
	public GameObject clientPos = null;

	public bool isTargeted = false;
	public GameObject itemInSlot = null;
	public Client client = null;
	public bool hasClient = false;	
	Material targetMat;

	string clientNeed = null;

	// Use this for initialization
	void Start ()
	{
		targetMat = transform.Find("Target").GetComponent<MeshRenderer>().material;
		target = transform.Find("Target").gameObject;
		target.SetActive(false);
		isTargeted = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (null != client && !hasClient)
		{
			ClientArrive();
		}
		if (hasClient)
		{
			if (null != itemInSlot && itemInSlot.name.Contains(client.need) && itemInSlot.GetComponent<Pint>().filling > 0)
			{
				client.IncreaseHappiness();
				itemInSlot.GetComponent<Pint>().Drink(client.drinkingSpeed);
			}
			else
			{
				client.DecreaseHappiness();
			}
		}
	}


	void ClientArrive()
	{
		hasClient = true;
		target.SetActive(true);
		client.transform.parent = clientPos.transform;
		client.transform.localPosition = Vector3.zero;
		clientNeed = client.need;
	}

	public void ClientLeave()
	{
		if (itemInSlot)
		{
			itemInSlot.GetComponent<Pint>().filling = 0f;
		}
		else
			target.SetActive(false);

		hasClient = false;
		clientNeed = null;
		ClientManager.Instance.clients.Remove(client);

		Destroy(client.gameObject);
		client = null;

	}

	public void Take()
	{
		itemInSlot = null;
		if ( !itemInSlot && !hasClient )
		{
			target.SetActive(false);
		}
		
	}

	public void TargetMe(Color c)
	{
		//Debug.Log("player is targeting me");
		isTargeted = true;
		targetMat.color = c;
	}

	public void UnTargetMe(Color c)
	{
		//Debug.Log("player is untargeting me");
		isTargeted = false;
		targetMat.color = c;
	}
}

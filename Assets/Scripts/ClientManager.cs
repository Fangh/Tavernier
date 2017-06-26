using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
	public GameObject clientPrefab = null;
	static public ClientManager Instance = null;


	public List<Client> clients = new List<Client>();

	float timeBetweenNewClients = 5f;
	float currentTimeBetweenNewClients = 0f;


	private void Awake()
	{
		Instance = this;
	}


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (clients.Count < 4)
		{
			if (currentTimeBetweenNewClients > timeBetweenNewClients)
			{
				currentTimeBetweenNewClients = 0f;
				Client c = GameObject.Instantiate(clientPrefab, new Vector3(100,100,100), Quaternion.identity ).GetComponent<Client>();
				clients.Add(c);				
				c.Init();
			}
			else
				currentTimeBetweenNewClients += Time.deltaTime;
		}


	}
}

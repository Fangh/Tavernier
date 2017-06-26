using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Enter(Color c)
	{
		Debug.Log("player is in " + name);
		GetComponent<MeshRenderer>().material.color = c;
	}

	public void Leave(Color c)
	{
		Debug.Log("player has leaved " + name);
		GetComponent<MeshRenderer>().material.color = c;
	}
}

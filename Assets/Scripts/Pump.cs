using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pump : ActionZone
{
	public float mLPerSeconds = 10f;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void Action()
	{

	}

	public void Fill( Pint pint )
	{
		if (pint.filling < pint.capacity)
			pint.filling += mLPerSeconds * Time.deltaTime;

		//Debug.Log("filling " + pint.filling);
	}
}

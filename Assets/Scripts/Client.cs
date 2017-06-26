using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
	Image happinessGauge = null;
	float happiness = 0;
	SlotManager mySlot = null;

	public string need = "Pint";
	public float drinkingSpeed = 1f;
	public float happinessSpeed = 0.05f;

	private void Awake()
	{
		happinessGauge = GetComponentInChildren<Image>();
	}

	// Use this for initialization
	void Start ()
	{
		SlotManager[] slots = GameObject.FindObjectsOfType<SlotManager>();

		foreach(SlotManager s in slots)
		{
			if (!s.hasClient)
			{
				mySlot = s;
				mySlot.client = this;
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		happinessGauge.fillAmount = happiness;
		if (happiness >= 1)
		{
			mySlot.ClientLeave();
			Debug.Log("+1 point ! yay");
		}
		if (happiness <= 0)
		{
			mySlot.ClientLeave();
			Debug.Log("OH NO :(");
		}
	}

	public void Init()
	{
		need = "Pint";
		happiness = Random.Range(0.25f, 0.50f);
		drinkingSpeed = Random.Range(0.5f, 2.5f);
		happinessSpeed = Random.Range(0.01f, 0.2f);
	}

	public void DecreaseHappiness()
	{
		if (happiness > 0f)
			happiness -= happinessSpeed * Time.deltaTime;
	}

	public void IncreaseHappiness()
	{
		if ( happiness < 1f )
			happiness += happinessSpeed * Time.deltaTime;
	}
}

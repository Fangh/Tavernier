using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
	public string need = "Pint";
	public float drinkingSpeed = 1f;
	public float happinessSpeed = 0.05f;
	public float delayBeforeThirsty = 2f;

	public float drinkingSpeedMin = 0.5f;
	public float drinkingSpeedMax = 1.5f;
	public float happinessSpeedMin = 0.01f;
	public float happinessSpeedMax = 0.1f;
	public float happinessMin = 0.25f;
	public float happinessMax = 0.50f;


	Image happinessGauge = null;
	float happiness = 0;
	SlotManager mySlot = null;


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
	void Update()
	{
		if (delayBeforeThirsty > 0)
		{ 
			delayBeforeThirsty -= Time.deltaTime;
			return;
		}

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

	public void CanDrink()
	{
		delayBeforeThirsty = 0;
	}

	public void Init()
	{
		need = "Pint";
		happiness = Random.Range(happinessMin, happinessMax);
		drinkingSpeed = Random.Range(drinkingSpeedMin, drinkingSpeedMax);
		happinessSpeed = Random.Range(happinessSpeedMin, happinessSpeedMax);

		happinessGauge.fillAmount = happiness;
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

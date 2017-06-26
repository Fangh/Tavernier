using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pint : MonoBehaviour
{
	public float filling = 0f;
	public float capacity = 50f;
	public Image fillGauge = null;

	// Use this for initialization
	void Start ()
	{
		fillGauge.fillAmount = 0f;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		fillGauge.fillAmount = (filling * 100 / capacity) / 100;
	}

	public void Drink(float speed)
	{
		filling -= speed * Time.deltaTime;
	}
}
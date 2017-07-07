using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pint : MonoBehaviour
{
	public float filling = 0f;
	public float capacity = 50f;
	public Image fillGauge = null;
	public GameObject FXPrefab = null;

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

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ground")
		{
			DestroyIn(0f);
		}
	}

	public void DestroyIn(float time = 0f, bool withFx = true)
	{
		StartCoroutine(CoroutineDestroyIn(time, withFx));
	}

	private IEnumerator CoroutineDestroyIn(float time, bool withFx)
	{
		yield return new WaitForSeconds(time);
		if (withFx)
		{
			GameObject fx = Instantiate(FXPrefab, transform.position, Quaternion.identity) as GameObject;
			Destroy(fx, 3f);
		}
		Destroy(gameObject);

	}
}
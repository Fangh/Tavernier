using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile : ActionZone
{
	public GameObject ItemPrefab;
	

	public GameObject GetItem()
	{
		return GameObject.Instantiate(ItemPrefab);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
	public LootableObject item;
	public int amount;

	[Range(0f, 100f)]
	public float chanceToLoot = 100f;

	public ObjectHolder GetLoot()
	{
		return IsSuccesfullLoot() ? new ObjectHolder(item, amount) : null;
	}

	private bool IsSuccesfullLoot()
	{
		return !(Random.Range(0, 100) - chanceToLoot >= 0);
	}

}

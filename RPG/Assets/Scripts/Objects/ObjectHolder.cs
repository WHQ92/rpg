using UnityEngine;

[System.Serializable]
public class ObjectHolder
{
	public LootableObject item;
	public int amount;

	public ObjectHolder(LootableObject item, int amount)
	{
		this.item = item;
		this.amount = amount;
	}

	public ObjectHolder()
	{
	}

	public bool IsMaxStack()
	{
		return amount >= item.stack;
	}

	public void AddItems(int numberOfItems, out int amountLeft)
	{
		amount += numberOfItems;

		if (amount > item.stack)
		{
			amountLeft = amount - item.stack;
			amount = item.stack;
		}
		else
		{
			amountLeft = 0;
		}
	}

}

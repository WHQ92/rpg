using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
	public static InventoryController instance;

	public GameObject inventorySlotPrefab;
	public GameObject inventorySlotsHolder;
	public GameObject inventoryHolder;

	public int maxAmountOfItems = 24;
	public List<ObjectHolder> items = new List<ObjectHolder>();

	public bool isInventoryActive;

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}
	}

	private void Start()
	{
		isInventoryActive = false;
		this.HideInventory();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			this.ToggleInventory();
		}
	}

	public void AddItems(List<ObjectHolder> newItems)
	{
		foreach (ObjectHolder item in items.Where(item => !item.IsMaxStack()).ToList())
		{
			foreach (ObjectHolder newItem in newItems.Where(newItem => newItem.item.name == item.item.name).ToList())
			{
				int amountLeft = 0;
				item.AddItems(newItem.amount, out amountLeft);

				if (amountLeft > 0)
				{
					newItems.Add(new ObjectHolder(newItem.item, amountLeft));
				}

				newItems.Remove(newItem);
			}
		}

		items.AddRange(newItems);
		BuildInventoryUi();
	}

	private void BuildInventoryUi()
	{
		EmptyInventoryUi();

		foreach (ObjectHolder objectHolder in items)
		{
			GameObject slot = Instantiate(inventorySlotPrefab, inventorySlotsHolder.transform);
			slot.GetComponentInChildren<Text>().text = objectHolder.amount.ToString();
			slot.GetComponentInChildren<Image>().sprite = objectHolder.item.icon;
		}
	}

	private void EmptyInventoryUi()
	{
		foreach (Transform t in inventorySlotsHolder.transform)
		{
			Destroy(t.gameObject);
		}
	}

	public void ToggleUi()
	{
		BuildInventoryUi();
		this.ToggleInventory();
	}
}

public static class InventoryExtensions
{
	public static void ShowInventory(this InventoryController inventory)
	{
		inventory.inventoryHolder.SetActive(true);
	}

	public static void HideInventory(this InventoryController inventory)
	{
		inventory.inventoryHolder.SetActive(false);
	}

	public static void ToggleInventory(this InventoryController inventory)
	{
		inventory.isInventoryActive = !inventory.isInventoryActive;

		if (inventory.isInventoryActive)
		{
			inventory.ShowInventory();
		}
		else
		{
			inventory.HideInventory();
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HarvestableBehaviour : InteractableBehaviour
{
	public AudioClip harvestSound;
	public int health = 3;

	private bool interactable = true;

	public override void Interact()
	{
		if (!interactable)
		{
			return;
		}

		interactable = false;

		MovementController.instance.DoAttack();

		Invoke("PlayAudio", 0.2f);

		health--;
		if (health == 0)
		{
			HandleInteract();
		}

		Invoke("EnableInteraction", 0.8f);
	}

	private void EnableInteraction()
	{
		interactable = true;
	}

	private void HandleInteract()
	{
		DropLoot();
		NotificationManager.instance.HideCurrentNotification();
		Destroy(gameObject, 0.5f);
	}

	private void PlayAudio()
	{
		if (GetComponent<AudioSource>() != null)
		{
			GetComponent<AudioSource>().Play();
		}
	}

	private void DropLoot()
	{
		InventoryController.instance.AddItems(GetComponents<Loot>()
			.Select(loot => loot.GetLoot())
			.Where(temp => temp != null)
			.ToList()
		);
	}
}

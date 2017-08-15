using UnityEngine;

public class InteractableBehaviour : MonoBehaviour
{
	public string interactionMessage = "Press 'E' to interact";

	public virtual void TriggerInteractionMessage()
	{
		NotificationManager.instance.ShowNotification(interactionMessage, this);
	}

	public virtual void Interact()
	{
		PopupManager.instance.ShowPopup("Debug");
	}
}

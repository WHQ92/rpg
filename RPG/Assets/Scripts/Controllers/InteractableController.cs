using UnityEngine;

public class InteractableController : MonoBehaviour
{
	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.IsInteractable())
		{
			col.gameObject.GetInteractableBehaviour().TriggerInteractionMessage();
		}
	}

	private void OnTriggerStay(Collider col)
	{
		if (Input.GetKeyDown(KeyCode.E) && col.gameObject.IsInteractable())
		{
			col.gameObject.GetInteractableBehaviour().Interact(MovementController.instance);
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.IsInteractable() && col.gameObject.GetInteractableBehaviour().IsCurrentInteractable())
		{
			NotificationManager.instance.HideCurrentNotification();
		}
	}
}

public static class Extensions
{
	private static string INTERACTABLE_TAG = "Interactable";

	public static bool IsCurrentInteractable(this InteractableBehaviour behaviour)
	{
		return behaviour == NotificationManager.instance.currentInteractedBehaviour;
	}

	public static bool IsInteractable(this GameObject obj)
	{
		return obj.tag == INTERACTABLE_TAG;
	}

	public static InteractableBehaviour GetInteractableBehaviour(this GameObject obj)
	{
		return obj.GetComponent<InteractableBehaviour>();
	}
}

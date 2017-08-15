using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NotificationManager : MonoBehaviour
{
	private const float FADE_TIME = 0.8f;

	public static NotificationManager instance;

	public GameObject notificationHolder;
	public Text txtMessage;
	public float displayTime = 2f;

	public InteractableBehaviour currentInteractedBehaviour;

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
		ResetNotification();
	}

	public void ShowNotification(string text, InteractableBehaviour interactableBehaviour)
	{
		currentInteractedBehaviour = interactableBehaviour;
		txtMessage.text = text;
		notificationHolder.SetActive(true);
		notificationHolder.GetComponent<RectTransform>().DOLocalMoveX(696f, FADE_TIME);
	}

	public void HideCurrentNotification()
	{
		notificationHolder.GetComponent<RectTransform>().DOLocalMoveX(1200f, FADE_TIME).OnComplete(ResetNotification);
	}

	private void ResetNotification()
	{
		txtMessage.text = string.Empty;
		notificationHolder.SetActive(false);
	}
}

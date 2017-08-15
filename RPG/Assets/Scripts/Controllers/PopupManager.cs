using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
	public static PopupManager instance;

	public GameObject popupHolder;
	public Text txtMessage;

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
		ResetPopup();
	}

	public void ShowPopup(string text)
	{
		txtMessage.text = text;
		popupHolder.SetActive(true);
	}

	public void HideCurrentPopup()
	{
		ResetPopup();
	}

	private void ResetPopup()
	{
		txtMessage.text = string.Empty;
		popupHolder.SetActive(false);
	}
}

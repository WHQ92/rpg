using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public static CameraController instance;

	private float vertical;
	private float spinningVelocity = 4.0f;

	private bool cameraLocked;
	private Vector3 oldPosition;
	private Vector3 oldRotation;

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
		vertical = transform.eulerAngles.x;
	}

	private void Update()
	{
		if (!cameraLocked && Input.GetKey(KeyCode.Mouse1))
		{
			var mouseVertical = Input.GetAxis("Mouse Y");
			vertical = (vertical - spinningVelocity * mouseVertical) % 360f;
			vertical = Mathf.Clamp(vertical, -30, 60);
			transform.localRotation = Quaternion.AngleAxis(vertical, Vector3.right);
		}
	}

	public void TweenPosition(Transform target)
	{
		cameraLocked = true;

		oldPosition = transform.position;
		oldRotation = transform.eulerAngles;

		transform.DOMove(target.position, 1);
		transform.DORotate(target.eulerAngles, 1);
	}

	public void DontTweenPosition()
	{
		cameraLocked = false;

		transform.DOMove(oldPosition, 1);
		transform.DORotate(oldRotation, 1).OnComplete(() =>
		{
			MovementController.instance.canMove = true;
		});
	}
}

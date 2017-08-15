using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour
{
	public static MovementController instance;

	public float runSpeed;
	public float turningSpeed;

	public bool canMove = true;

	private Animator animator;
	float horizontal;

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
		animator = GetComponentInChildren<Animator>();
	}

	private void Update()
	{
		if (canMove && Input.GetKey(KeyCode.Mouse1))
		{
			HandleRotation();
		}
		animator.SetBool("Strafing", (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)));

		if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
		{
			DoAttack();
		}

		HandleMovement();

	}

	public void DoAttack()
	{
		animator.SetTrigger("Attack3Trigger");
		StartCoroutine(LockMovement());
	}

	private void HandleMovement()
	{
		Vector3 translation = Vector3.zero;

		if (Input.GetKey(KeyCode.W) && canMove)
		{
			translation.z = 1f * Time.deltaTime * runSpeed * 0.3f;
		}

		if (Input.GetKey(KeyCode.A) && canMove)
		{
			translation.x = -1f * Time.deltaTime * runSpeed * 0.2f;
		}

		if (Input.GetKey(KeyCode.S) && canMove)
		{
			translation.z = -1f * Time.deltaTime * runSpeed * 0.3f;
		}

		if (Input.GetKey(KeyCode.D) && canMove)
		{
			translation.x = 1f * Time.deltaTime * runSpeed * 0.2f;
		}

		if (Input.GetKey(KeyCode.LeftShift) && canMove)
		{
			translation.z *= 8f;
		}

		ApplyAnimation(translation);

		transform.Translate(translation);
	}

	private void ApplyAnimation(Vector3 movement)
	{
		animator.SetFloat("Velocity X", Mathf.Clamp(movement.x / runSpeed * 50f, -0.15f, 0.15f));
		animator.SetFloat("Velocity Z", Mathf.Clamp(movement.z / runSpeed * 50f, -0.8f, 0.8f));

		if (movement.x != 0f || movement.z != 0f)
		{
			animator.SetBool("Moving", true);
		}
		else
		{
			animator.SetBool("Moving", false);
		}
	}

	private void HandleRotation()
	{
		float mouseHorizontal = Input.GetAxis("Mouse X");
		horizontal = (horizontal + turningSpeed * mouseHorizontal) % 360f;

		transform.rotation = Quaternion.AngleAxis(horizontal, Vector3.up);
	}

	public IEnumerator LockMovement()
	{
		float lockTime = 1.4f;
		canMove = false;
		animator.SetBool("Moving", false);
		yield return new WaitForSeconds(lockTime);
		canMove = true;
	}

	//Animation Events
	void Hit()
	{

	}

	void FootL()
	{

	}

	void FootR()
	{

	}

	void Jump()
	{

	}

	void Land()
	{

	}
}

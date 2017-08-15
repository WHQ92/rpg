using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PatrolBehaviour : MonoBehaviour
{
	public Transform waypointHolder;
	public float animationSpeedModifier = 1f;
	public int startPatrolAtIndex = 0;

	private Transform[] waypoints;

	private int destination;
	private NavMeshAgent agent;
	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		agent.autoBraking = false;
		agent.speed = 1f;

		waypoints = waypointHolder.GetComponentsInChildren<Transform>().Where((t) => t != waypointHolder).ToArray();

		if (waypoints.Length == 0)
		{
			throw new UnityException("No waypoints were setup for: " + gameObject.name);
		}

		destination = (startPatrolAtIndex < waypoints.Length) ? startPatrolAtIndex : 0;
		MoveToNextDestination();

	}

	private void Update()
	{
		if (!agent.pathPending && agent.remainingDistance < 1f)
		{
			MoveToNextDestination();
		}
	}

	private void OnDrawGizmos()
	{
		if (agent != null && agent.destination != null)
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(transform.position, agent.destination);
		}
	}

	private void FixedUpdate()
	{
		HandleAnimation();
	}

	private void HandleAnimation()
	{
		Vector3 movement = agent.velocity;

		animator.SetFloat("Velocity X", movement.y * animationSpeedModifier);
		animator.SetFloat("Velocity Z", movement.z * animationSpeedModifier);
		animator.SetBool("Moving", (movement.x != 0f || movement.z != 0f));
	}

	private void MoveToNextDestination()
	{
		if (waypoints.Length == 0)
		{
			return;
		}

		agent.destination = waypoints[destination].position;
		destination = (destination + 1) % waypoints.Length;
	}

}

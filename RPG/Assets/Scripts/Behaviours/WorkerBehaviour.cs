using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WorkerBehaviour : MonoBehaviour, IAnimatable
{
    public Transform startPosition;
    public HarvestableBehaviour harvestableObject;
    public float animationSpeedModifier = 1f;

    public int maxAmountOfItems = 5;
    public List<ObjectHolder> items = new List<ObjectHolder>();

    private NavMeshAgent agent;
    private Animator animator;
    public WorkerBehaviourState currentState;
    private bool isStoring = false;
    private bool canInteract = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = true;
        agent.speed = 1f;

        MoveToHarvestableObject();
    }

    private void Update()
    {
        UpdateState();
        HandleState();
    }

    private void UpdateState()
    {
        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            if (Vector3.Distance(agent.destination, harvestableObject.transform.position) < 1f)
            {
                currentState = WorkerBehaviourState.Harvesting;
            }
            else
            {
                currentState = WorkerBehaviourState.Storing;
            }
        }
        else
        {
            currentState = WorkerBehaviourState.Walking;
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

    private void HandleState()
    {
        switch (currentState)
        {
            case WorkerBehaviourState.Storing:
                HandleStoring();
                break;
            case WorkerBehaviourState.Harvesting:
                HandleHarvesting();
                break;
        }
    }

    private void HandleHarvesting()
    {
        if (items.Count == maxAmountOfItems)
        {
            // The workers inventory is full, return
            MoveToStartPosition();
        }
        else if (items.Count < maxAmountOfItems && canInteract)
        {
            canInteract = false;
            harvestableObject.Interact(this);
            Invoke("EnableInteraction", 1f);
        }
    }

    private void EnableInteraction()
    {
        canInteract = true;
    }

    private void HandleStoring()
    {
        if (!isStoring)
        {
            StartCoroutine(StoreInventoryItems());
        }
    }

    private void HandleAnimation()
    {
        Vector3 movement = agent.velocity;

        animator.SetFloat("Velocity X", movement.y * animationSpeedModifier);
        animator.SetFloat("Velocity Z", movement.z * animationSpeedModifier);
        animator.SetBool("Moving", (movement.x != 0f || movement.z != 0f));
    }

    private void MoveToHarvestableObject()
    {
        agent.destination = harvestableObject.transform.position;
    }

    IEnumerator StoreInventoryItems()
    {
        isStoring = true;
        // TODO: Add to storage
        items.Clear();

        yield return new WaitForSeconds(5);

        isStoring = false;
        MoveToHarvestableObject();
    }

    private void MoveToStartPosition()
    {
        agent.destination = startPosition.position;
    }

    public void DoAttack()
    {
        animator.SetTrigger("Attack3Trigger");
    }
}

public enum WorkerBehaviourState
{
    Harvesting, Walking, Storing
}
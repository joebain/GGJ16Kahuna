using UnityEngine;

public class NPC : MonoBehaviour
{
    private NavMeshAgent navAgent;

    public bool Walking = false;

    private Vector3 startPoint;

    public void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        startPoint = transform.position;
    }

    public void Update()
    {
        if (Walking)
        {
            if (navAgent.isOnNavMesh && navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                navAgent.destination = startPoint + Random.insideUnitSphere * 20f;
            }
        }
    }
}

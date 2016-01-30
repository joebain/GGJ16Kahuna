﻿using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public Inventory Inventory = new Inventory();

    private NavMeshAgent agent;

    private GameObject targetIndicator;

	public SuccessfulActionLog log;
	public List<SuccessfulActionLog> logs;

    void Start()
    {
		log = gameObject.AddComponent<SuccessfulActionLog>();
        agent = GetComponent<NavMeshAgent>();

        targetIndicator = transform.Find("Target").gameObject;
        targetIndicator.SetActive(false);
	}

	public void RefreshLogs()
	{
		logs = new List<SuccessfulActionLog>();
		logs.AddRange(GameObject.FindObjectsOfType<SuccessfulActionLog>());
		logs.Remove(log);
    }

    public void GoTo(Vector3 position)
    {
        if (float.IsInfinity(position.x)) { return; }
        agent.destination = position;
        targetIndicator.transform.position = position;
        targetIndicator.SetActive(true);
    }

    void Update()
    {
        if (float.IsInfinity(agent.destination.x))
        {
            agent.destination = agent.transform.position;
        }

        if (agent.isOnNavMesh && agent.remainingDistance <= agent.stoppingDistance)
        {
            targetIndicator.SetActive(false);
        } else
        {
            targetIndicator.transform.position = agent.destination;
        }
    }

	void OnTriggerEnter(Collider other)
	{
		SuccessfulActionLog otherLog = other.gameObject.GetComponent<SuccessfulActionLog>();
		if (otherLog != null && !otherLog.done)
		{
			if (otherLog.TestAgainst(log))
			{
				log.actions.Add(otherLog.actionOnMatch);
				otherLog.done = true;
			}
		}
	}
}

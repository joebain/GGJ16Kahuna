using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public Inventory Inventory = new Inventory();

    private NavMeshAgent agent;

    private GameObject targetIndicator;

	public SuccessfulActionLog log;
	public List<SuccessfulActionLog> logs;

	GameObject textBox;
	Text t;

    SFX sfx;

    void Start()
    {
		log = gameObject.AddComponent<SuccessfulActionLog>();
        agent = GetComponent<NavMeshAgent>();

        targetIndicator = transform.Find("Target").gameObject;
        targetIndicator.SetActive(false);

        sfx = transform.Find("SFX").GetComponent<SFX>();

        textBox = GameObject.Find("textbox");
		t = GameObject.Find("report").GetComponent<Text>();
		textBox.SetActive(false);
	}

	public void RefreshLogs()
	{
		logs = new List<SuccessfulActionLog>();
		logs.AddRange(GameObject.FindObjectsOfType<SuccessfulActionLog>());
		logs.Remove(log);
    }

    public void GoTo(Vector3 position)
    {
		if (textBox.activeSelf) return;
        if (float.IsInfinity(position.x)) { return; }

        agent.destination = position;
        targetIndicator.transform.position = position;
        targetIndicator.SetActive(true);

        sfx.PlayHit();
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
				StartCoroutine(TextBox(otherLog.textOnMatch));
			}
			else if (otherLog.textOnFail != null && otherLog.textOnFail.Length > 0)
			{
				StartCoroutine(TextBox(otherLog.textOnFail));
			}
		}
	}

	IEnumerator TextBox(string report)
	{
		t.text = report;

		textBox.SetActive(true);

		while (!Input.anyKeyDown)
		{
			yield return null; 
		}

		textBox.SetActive(false);

		yield return null;
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;
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

	float timeBox;

	GameObject textBox;
	Text t;

    public SFX sfx;
	string instruction;

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
				instruction = otherLog.instruction; Debug.Log("set instruction to " + instruction);
				bool skip = false;
				if (instruction == "skip if torches doused" && Torch.phase == 3) skip = true;
				sfx.PlayPositive();
				if (!skip)
					StartCoroutine(TextBox(otherLog.textOnMatch));
			}
			else if (otherLog.textOnFail != null && otherLog.textOnFail.Length > 0)
			{
				StartCoroutine(TextBox(otherLog.textOnFail));
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		SuccessfulActionLog otherLog = other.gameObject.GetComponent<SuccessfulActionLog>();
		if (otherLog != null && !otherLog.done && !textBox.activeSelf)
		{
			if (otherLog.TestAgainst(log))
			{
				log.actions.Add(otherLog.actionOnMatch);
				otherLog.done = true;
				instruction = otherLog.instruction; Debug.Log("set instruction to " + instruction);
				bool skip = false;
				if (instruction == "skip if torches doused" && Torch.phase == 3) skip = true;
				sfx.PlayPositive();
				if (!skip)
					StartCoroutine(TextBox(otherLog.textOnMatch));
			}
			else if (otherLog.textOnFail != null && otherLog.textOnFail.Length > 0)
			{
				StartCoroutine(TextBox(otherLog.textOnFail));
			}
		}
	}

	public void ShowTextBox(string report, bool negative = false)
	{
		Debug.Log("start cor");
        if (negative)
        {
            sfx.PlayDiscord();
        } else
        {
            sfx.PlayPositive();
        }
		StartCoroutine(TextBox(report));
	}

	IEnumerator TextBox(string report)
	{
		t.text = report;
		timeBox = Time.timeSinceLevelLoad;

		textBox.SetActive(true);
		yield return null; 

		while (!Input.anyKeyDown || Time.timeSinceLevelLoad < timeBox + 0.5f)
		{
			yield return null; 
		}

		textBox.SetActive(false);

		switch (instruction)
		{
		default: break;
		case "open door":
			{
				Debug.Log("opening the door");
				GameObject.Find("door_collision").SetActive(false);
			} break;
		case "villagers_come":
			{
				// send all villagers to player
				NPC[] agents = GameObject.FindObjectsOfType<NPC>();
				foreach (NPC agent in agents)
				{
					agent.WalkToPlayer();
				}
			} break;
		case "send_ziggurat":
			{
				// send all villagers to player
				NPC[] agents = GameObject.FindObjectsOfType<NPC>();
				foreach (NPC agent in agents)
				{
					agent.GoToTemple();
				}
			} break;
		case "game over":
			{
				SceneManager.LoadScene("splash");
			} break;
		}

		yield return null;
	}
}

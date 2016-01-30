using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Action : ScriptableObject
{
	public bool result;
	public string objectName;
}

[ExecuteInEditMode()]
public class SuccessfulActionLog : MonoBehaviour {

	public List<Action> actions = new List<Action>();

	public SuccessfulActionLog other;
	public bool match = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (other != null)
		{
			match = TestAgainst(other);
		}	
	}

	public bool TestAgainst(SuccessfulActionLog other)
	{
		int theirIndex = 0;
		// go through each of our actions, and find an equivalent in the other
		for (int i = 0; i < actions.Count; i++) {
			bool foundThisAction = false;
			while (theirIndex < other.actions.Count)
			{
				if (other.actions[theirIndex].objectName == actions[i].objectName && 
					other.actions[theirIndex].result == actions[i].result)
				{
					foundThisAction = true;
				}
				theirIndex++;
				if (foundThisAction) break;
			}
			if (!foundThisAction) return false;
		}
		return true;
	}
}

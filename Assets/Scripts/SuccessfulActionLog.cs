using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode()]
public class SuccessfulActionLog : MonoBehaviour {

	public List<string> actions = new List<string>();
	public string actionOnMatch;

	public SuccessfulActionLog other;
	public bool match = false;
	public bool done = false;

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
				if (other.actions[theirIndex] == actions[i])
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

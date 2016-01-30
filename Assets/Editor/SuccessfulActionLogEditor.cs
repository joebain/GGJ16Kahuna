using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SuccessfulActionLog))]
public class SuccessfulActionLogEditor : Editor {

	public override void OnInspectorGUI ()
	{
		//DrawDefaultInspector();
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.TextArea("Action list");
		SuccessfulActionLog log = (SuccessfulActionLog) target;
		List<Action> toRemove = new List<Action>();
		foreach(Action a in log.actions)
		{
			if (a != null) 				
			{
				EditorGUILayout.BeginHorizontal();
				a.objectName = EditorGUILayout.TextField("name", a.objectName);
				a.result = EditorGUILayout.Toggle("true?", a.result);
				if (GUILayout.Button("-")) toRemove.Add(a);
				EditorGUILayout.EndHorizontal();
			}
			else
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.TextField("empty");
				EditorGUILayout.EndHorizontal();
			}
		}

		foreach(Action a in toRemove) log.actions.Remove(a);

		if (GUILayout.Button("Add"))
		{
			log.actions.Add(ScriptableObject.CreateInstance<Action>());
		}

		log.other = (SuccessfulActionLog)EditorGUILayout.ObjectField(log.other, typeof(SuccessfulActionLog), true, null);
		EditorGUILayout.Toggle(log.match);
		if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(target);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

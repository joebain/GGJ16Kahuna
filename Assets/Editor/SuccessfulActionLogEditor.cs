using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SuccessfulActionLog))]
public class SuccessfulActionLogEditor : Editor {

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();
//		EditorGUI.BeginChangeCheck();
//		EditorGUILayout.TextArea("Action list");
//		SuccessfulActionLog log = (SuccessfulActionLog) target;
//		List<string> toRemove = new List<string>();
//
//		foreach(string a in log.actions)
//		{
//			if (a != null) 				
//			{
//				EditorGUILayout.BeginHorizontal();
//				a = EditorGUILayout.TextField("name", a);
//				if (GUILayout.Button("-")) toRemove.Add(a);
//				EditorGUILayout.EndHorizontal();
//			}
//			else
//			{
//				EditorGUILayout.BeginHorizontal();
//				EditorGUILayout.TextField("empty");
//				EditorGUILayout.EndHorizontal();
//			}
//		}
//
//		foreach(string a in toRemove) log.actions.Remove(a);
//
//		if (GUILayout.Button("Add"))
//		{
//			log.actions.Add(ScriptableObject.CreateInstance<Action>());
//		}
//
//		log.other = (SuccessfulActionLog)EditorGUILayout.ObjectField(log.other, typeof(SuccessfulActionLog), true, null);
//		EditorGUILayout.Toggle(log.match);
//		EditorGUILayout.TextArea("Action");
//		if (log.actionOnMatch == null) log.actionOnMatch = new Action();//ScriptableObject.CreateInstance<Action>();
//		EditorGUILayout.BeginHorizontal();
//		log.actionOnMatch = EditorGUILayout.TextField("name", log.actionOnMatch);
//		EditorGUILayout.EndHorizontal();
//
//		if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(target);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

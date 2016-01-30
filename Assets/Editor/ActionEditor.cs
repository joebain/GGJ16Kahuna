using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(Action))]
public class ActionEditor : Editor {

	public override void OnInspectorGUI()
	{
		base.DrawDefaultInspector();
		EditorGUILayout.TextArea("Testing!");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

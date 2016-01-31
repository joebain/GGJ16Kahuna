using UnityEngine;
using System.Collections;

public class DisableOnEnter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.isEditor && Input.GetKeyDown(KeyCode.Return)) gameObject.SetActive(false);
	}
}

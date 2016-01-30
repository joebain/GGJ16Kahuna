using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blink : MonoBehaviour {

	Image i;
	float t;

	// Use this for initialization
	void Start () {
		i = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		// blink you fucker
		t = Mathf.Repeat(t + Time.deltaTime, 1.0f);
		Color c = i.color;
		c.a = (t < 0.2f) ? 1.0f : 0.0f;
		i.color = c;
	}
}

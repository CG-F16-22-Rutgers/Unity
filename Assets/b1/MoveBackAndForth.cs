using UnityEngine;
using System.Collections;

public class MoveBackAndForth : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public float time;
	public bool right = false;
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 3.4) {
			time = 0;
			right = !right;
		}
		if (right) {
			transform.Translate(0, 0, Time.deltaTime*5);
		} else {
			transform.Translate(0, 0, -Time.deltaTime*5);
		}
	
	}
}

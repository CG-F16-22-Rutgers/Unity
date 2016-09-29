using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
    public bool isActive = false;
    public Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

    void Update() {
		if (isActive) {
			Vector3 dir = new Vector3();
			if (Input.GetKey(KeyCode.RightArrow)) {
				dir += Vector3.right;
			} 
			if (Input.GetKey(KeyCode.LeftArrow)) {
				dir += Vector3.left;
			}
			if (Input.GetKey(KeyCode.UpArrow)) {
				dir += Vector3.forward;
			}
			if (Input.GetKey(KeyCode.DownArrow)) {
				dir += Vector3.back;
			}

			Vector3 movement = Quaternion.Euler(0, Camera.main.transform.localEulerAngles.y, 0) * dir;
			
			rb.AddForce(movement * 10);
		}
    }


}

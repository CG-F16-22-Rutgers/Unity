using UnityEngine;
using System.Collections;

public class Erika : MonoBehaviour {
	public float dampX;
	public float dampY;

    private Animator animator;
	private float x;
	private float y;
	private float velocityX = 0;
	private float goalX;

	private float velocityY = 0;
	private float goalY;

	void Start() {
		animator = GetComponent<Animator>();
	}

	void Update() {
		goalX = Input.GetAxis("Horizontal");
		x = Mathf.SmoothDamp(x, goalX, ref velocityX, dampX);
		animator.SetFloat("WalkingX", x);

		goalY = (Input.GetKey(KeyCode.LeftShift)) ? Input.GetAxis("Vertical") : Input.GetAxis("Vertical")/2;
		y = Mathf.SmoothDamp(y, goalY, ref velocityY, dampY);
		animator.SetFloat("WalkingY", y);
	}
}

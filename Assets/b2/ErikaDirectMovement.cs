using UnityEngine;
using System.Collections;

public class ErikaDirectMovement : MonoBehaviour {
	public float dampX;
	public float dampY;

    private Animator animator;
	private float x;
	private float y;
	private float velocityX = 0;
	private float goalX;

	private float velocityY = 0;
	private float goalY;

    private float strafe;
    private float strafeX;
    private float velocityStrafe =0;
    public float straveDamp;


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


        strafe=0;
        if (Input.GetKey(KeyCode.J))
        {
            strafe = -1;
        }else if (Input.GetKey(KeyCode.L))
        {
            strafe = 1;
        }
        strafeX = Mathf.SmoothDamp(strafeX, strafe, ref velocityStrafe, straveDamp);
        animator.SetFloat("Strafe", strafeX);

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("Jump", true);
        }else
        {
            animator.SetBool("Jump", false);
        }

    }
}

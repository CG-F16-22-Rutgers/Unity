using UnityEngine;
using System.Collections;

public class switchCamera : MonoBehaviour {

    public GameObject freeCamera;
    public GameObject attachedCamera;
    public GameObject agent;
     Animator animator;
    public GameObject speedText;

    bool freeActive;
    bool attachedActive;
    bool agentDirectActive;
    bool speedTextActive;

    void Start()
    {
        freeActive = false;
        attachedActive = true;
        agentDirectActive = true;
        animator = agent.GetComponent<Animator>();
        speedTextActive = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            freeActive = !freeActive;
            attachedActive = !attachedActive;
            agentDirectActive = !agentDirectActive;
            speedTextActive = !speedTextActive;
            animator.SetFloat("WalkingX", 0);
            animator.SetFloat("WalkingY", 0);
            animator.SetFloat("Strafe", 0);
            animator.SetBool("Jump", false);
        }

        freeCamera.SetActive(freeActive);
        agent.GetComponent<ErikaDirectMovement>().enabled= agentDirectActive;
        agent.GetComponent<ErikaNavigation>().enabled = !agentDirectActive;
        attachedCamera.SetActive(attachedActive);
        speedText.SetActive(speedTextActive);
    }
}

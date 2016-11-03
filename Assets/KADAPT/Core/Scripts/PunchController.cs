using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))] 

public class PunchController : MonoBehaviour {
    
    protected Animator animator;
    
    private bool active = false;
    private Transform rightHandObj = null;
    private Transform lookObj = null;

    void Start () 
    {
        animator = GetComponent<Animator>();
    }

	public void punch(Transform rightHandObj, Transform lookObj) {
		this.rightHandObj = rightHandObj;
		this.lookObj = lookObj;
        active = true;
        Debug.Log("punch");
	}

    public void unpunch() {
        active = false;
		rightHandObj = null;
		lookObj = null;
	}
    
    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if(animator) {
            
            //if the IK is active, set the position and rotation directly to the goal. 
            if(active) {

                // Set the look target position, if one has been assigned
                if(lookObj != null) {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }    

                // Set the right hand target position and rotation, if one has been assigned
                if(rightHandObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
                    animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
                }        
                
            }
            
            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {          
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0); 
                animator.SetLookAtWeight(0);
            }
        }
    }    
}



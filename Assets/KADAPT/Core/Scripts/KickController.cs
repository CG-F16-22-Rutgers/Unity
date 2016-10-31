using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))] 

public class KickController : MonoBehaviour {
    
    protected Animator animator;
    
    private bool ikActive = false;
    private Transform rightHandObj = null;
    private Transform lookObj = null;

    void Start () 
    {
        animator = GetComponent<Animator>();
    }

	public void kick(Transform rightHandObj, Transform lookObj) {
		this.rightHandObj = rightHandObj;
		this.lookObj = lookObj;
		ikActive = true;
	}

    public void unkick() {
		ikActive = false;
		rightHandObj = null;
		lookObj = null;
	}
    
    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if(animator) {
            
            //if the IK is active, set the position and rotation directly to the goal. 
            if(ikActive) {

                // Set the look target position, if one has been assigned
                if(lookObj != null) {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }    

                // Set the right hand target position and rotation, if one has been assigned
                if(rightHandObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,1);  
                    animator.SetIKPosition(AvatarIKGoal.RightFoot,rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot,rightHandObj.rotation);
                }        
                
            }
            
            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {          
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,0); 
                animator.SetLookAtWeight(0);
            }
        }
    }    
}


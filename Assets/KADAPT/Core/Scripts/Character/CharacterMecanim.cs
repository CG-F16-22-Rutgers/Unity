using UnityEngine;
using TreeSharpPlus;
using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine.UI;

public class CharacterMecanim : MonoBehaviour
{
    public const float MAX_REACHING_DISTANCE = 1.1f;
    public const float MAX_REACHING_HEIGHT = 2.0f;
    public const float MAX_REACHING_ANGLE = 100;

    private Dictionary<FullBodyBipedEffector, bool> triggers;
    private Dictionary<FullBodyBipedEffector, bool> finish;

    [HideInInspector]
    public BodyMecanim Body = null;

    void Awake() { this.Initialize(); }

    /// <summary>
    /// Searches for and binds a reference to the Body interface
    /// </summary>
    public void Initialize()
    {
        this.Body = this.GetComponent<BodyMecanim>();
        this.Body.InteractionTrigger += this.OnInteractionTrigger;
        this.Body.InteractionStop += this.OnInteractionFinish;
    }

    private void OnInteractionTrigger(
        FullBodyBipedEffector effector, 
        InteractionObject obj)
    {
        if (this.triggers == null)
            this.triggers = new Dictionary<FullBodyBipedEffector, bool>();
        if (this.triggers.ContainsKey(effector))
            this.triggers[effector] = true;
    }

    private void OnInteractionFinish(
        FullBodyBipedEffector effector,
        InteractionObject obj)
    {
        if (this.finish == null)
            this.finish = new Dictionary<FullBodyBipedEffector, bool>();
        if (this.finish.ContainsKey(effector))
            this.finish[effector] = true;
    }

    #region Smart Object Specific Commands
    public virtual RunStatus WithinDistance(Vector3 target, float distance)
    {
        if ((transform.position - target).magnitude < distance)
            return RunStatus.Success;
        return RunStatus.Failure;
    }

    public virtual RunStatus Approach(Vector3 target, float distance)
    {
        Vector3 delta = target - transform.position;
        Vector3 offset = delta.normalized * distance;
        return this.NavGoTo(target - offset);
    }
    #endregion

    #region Navigation Commands
    /// <summary>
    /// Turns to face a desired target point
    /// </summary>
    public virtual RunStatus NavTurn(Val<Vector3> target)
    {
        Debug.Log("We are at:" + this.gameObject.transform.position);
        Debug.Log("Want to turn to :" + target.Value);
        if (target.Value.y == 0)
        {
            Debug.Log("our y value is 0. so we made the desired location:" + new Vector3(target.Value.x, 2, target.Value.z));
            Debug.Log("We are at:" + this.gameObject.transform.position);
            this.Body.NavSetOrientationBehavior(OrientationBehavior.None);
            this.Body.NavSetDesiredOrientation(new Vector3(target.Value.x, 2, target.Value.z));
            if (this.Body.NavIsFacingDesired() == true)
            {
                this.Body.NavSetOrientationBehavior(
                    OrientationBehavior.LookForward);
                return RunStatus.Success;
            }
        }
        else
        {
            this.Body.NavSetOrientationBehavior(OrientationBehavior.None);
            this.Body.NavSetDesiredOrientation(target.Value);
            if (this.Body.NavIsFacingDesired() == true)
            {
                this.Body.NavSetOrientationBehavior(
                    OrientationBehavior.LookForward);
                return RunStatus.Success;
            }
        }
        return RunStatus.Running;
    }

    /// <summary>
    /// Turns to face a desired orientation
    /// </summary>
    public virtual RunStatus NavTurn(Val<Quaternion> target)
    {
        this.Body.NavSetOrientationBehavior(OrientationBehavior.None);
        this.Body.NavSetDesiredOrientation(target.Value);
        if (this.Body.NavIsFacingDesired() == true)
        {
            this.Body.NavFacingSnap();
            this.Body.NavSetOrientationBehavior(
                OrientationBehavior.LookForward);
            return RunStatus.Success;
        }
        return RunStatus.Running;
    }

    /// <summary>
    /// Sets a custom orientation behavior
    /// </summary>
    public virtual RunStatus NavOrientBehavior(
        Val<OrientationBehavior> behavior)
    {
        this.Body.NavSetOrientationBehavior(behavior.Value);
        return RunStatus.Success;
    }

    /// <summary>
    /// Sets a new navigation target. Will fail immediately if the
    /// point is unreachable. Blocks until the agent arrives.
    /// </summary>
    public virtual RunStatus NavGoTo(Val<Vector3> target)
    {
        if (this.Body.NavCanReach(target.Value) == false)
        {
            return RunStatus.Failure;
        }
        // TODO: I previously had this if statement here to prevent spam:
        //     if (this.Interface.NavTarget() != target)
        // It's good for limiting the amount of SetDestination() calls we
        // make internally, but sometimes it causes the character1 to stand
        // still when we re-activate a tree after it's been terminated. Look
        // into a better way to make this smarter without false positives. - AS
        this.Body.NavGoTo(target.Value);
        if (this.Body.NavHasArrived() == true)
        {
            if (this.gameObject.name == "Robber")
            {
                Debug.Log("we finished getting");
            }
            this.Body.NavStop();
            return RunStatus.Success;
        }

        

        return RunStatus.Running;
        // TODO: Timeout? - AS
    }

    /// <summary>
    /// Lerps the character towards a target. Use for precise adjustments.
    /// </summary>
    public virtual RunStatus NavNudgeTo(Val<Vector3> target)
    {
        bool? result = this.Body.NavDoneNudge();
        if (result == null)
        {
            this.Body.NavNudge(target.Value, 0.3f);
        }
        else if (result == true)
        {
            this.Body.NavNudgeStop();
            return RunStatus.Success;
        }
        return RunStatus.Running;
    }

    private IEnumerator<RunStatus> snapper;

    private RunStatus TickSnap(
        Vector3 position,
        Vector3 target,
        float time = 1.0f)
    {
        if (this.snapper == null)
            this.snapper = 
                SnapToTarget(position, target, time).GetEnumerator();
        if (this.snapper.MoveNext() == false)
        {
            this.snapper = null;
            return RunStatus.Success;
        }
        return snapper.Current;
    }

    private IEnumerable<RunStatus> SnapToTarget(
        Vector3 position,
        Vector3 target,
        float time)
    {
        Interpolator<Vector3> interp =
            new Interpolator<Vector3>(
                position,
                target,
                Vector3.Lerp);
        interp.ForceMin();
        interp.ToMax(time);

        while (interp.State != InterpolationState.Max)
        {
            transform.position = interp.Value;
            yield return RunStatus.Running;
        }
        yield return RunStatus.Success;
        yield break;
    }

	/// <summary>
	/// Stops the Navigation system. Blocks until the agent is stopped.
	/// </summary>
	public virtual RunStatus NavStop()
    {
        this.Body.NavStop();
        if (this.Body.NavIsStopped() == true)
        {
            return RunStatus.Success;
        }
        return RunStatus.Running;
        // TODO: Timeout? - AS
    }
    #endregion

    #region Interaction Commands
    public virtual RunStatus WaitForTrigger(
        Val<FullBodyBipedEffector> effector)
    {
        if (this.triggers == null)
            this.triggers = new Dictionary<FullBodyBipedEffector, bool>();
        if (this.triggers.ContainsKey(effector.Value) == false)
            this.triggers.Add(effector.Value, false);
        if (this.triggers[effector.Value] == true)
        {
            this.triggers.Remove(effector.Value);
            return RunStatus.Success;
        }
        return RunStatus.Running;
    }

    public virtual RunStatus WaitForFinish(
        Val<FullBodyBipedEffector> effector)
    {
        if (this.finish == null)
            this.finish = new Dictionary<FullBodyBipedEffector, bool>();
        if (this.finish.ContainsKey(effector.Value) == false)
            this.finish.Add(effector.Value, false);
        if (this.finish[effector.Value] == true)
        {
            this.finish.Remove(effector.Value);
            return RunStatus.Success;
        }
        return RunStatus.Running;
    }

    public virtual RunStatus StartInteraction(
        Val<FullBodyBipedEffector> effector, 
        Val<InteractionObject> obj)
    {
        this.Body.StartInteraction(effector, obj);
        return RunStatus.Success;
    }

    public virtual RunStatus ResumeInteraction(
        Val<FullBodyBipedEffector> effector)
    {
        this.Body.ResumeInteraction(effector);
        return RunStatus.Success;
    }

    public virtual RunStatus StopInteraction(Val<FullBodyBipedEffector> effector)
    {
        this.Body.StopInteraction(effector);
        return RunStatus.Success;
    }	
    #endregion

    #region HeadLook Commands
    public virtual RunStatus HeadLookAt(Val<Vector3> target)
    {
        this.Body.HeadLookAt(target);
        return RunStatus.Success;
    }

    public virtual RunStatus HeadLookStop()
    {
        this.Body.HeadLookStop();
		return RunStatus.Success;
	}
    #endregion

    #region Animation Commands
    public virtual RunStatus FaceAnimation(
        Val<string> gestureName, Val<bool> isActive)
    {
        this.Body.FaceAnimation(gestureName.Value, isActive.Value);
		return RunStatus.Success;
	}
	
	public virtual RunStatus HandAnimation(
        Val<string> gestureName, Val<bool> isActive)
    {
        this.Body.HandAnimation(gestureName.Value, isActive.Value);
		return RunStatus.Success;
	}

	public virtual RunStatus BodyAnimation(Val<string> gestureName, Val<bool> isActive)
	{
		this.Body.BodyAnimation(gestureName.Value, isActive.Value);
		return RunStatus.Success;
	}

	public RunStatus ResetAnimation()
    {
        this.Body.ResetAnimation();
        return RunStatus.Success;
    }
    #endregion

    #region Sitting Commands
    /// <summary>
    /// Sits the character down
    /// </summary>
    public virtual RunStatus SitDown()
    {
        if (this.Body.IsSitting() == true)
            return RunStatus.Success;
        this.Body.SitDown();
        return RunStatus.Running;
    }

    /// <summary>
    /// Stands the character up
    /// </summary>
    public virtual RunStatus StandUp()
    {
        if (this.Body.IsStanding() == true)
            return RunStatus.Success;
        this.Body.StandUp();
        return RunStatus.Running;
    }
    #endregion

    public virtual RunStatus startConversation(Val<GameObject> guy1, Val<GameObject> guy2, Val<bool> isActive)
    {
        this.Body.startConverse(guy1.Value, guy2.Value, isActive.Value);
        return RunStatus.Success;
    }
    public virtual RunStatus progressConversation(Val<bool> isActive, Val<object> conversationP, Val<object> conversationN, Val<Text> conversationText)
    {
        int conversationNumber = (int)conversationN.Value;
        int conversationPart = (int)conversationP.Value;
        switch (conversationNumber)
        {
            case 0:
                if(conversation0(conversationPart, conversationText.Value) == 0)
                {
                    return RunStatus.Success;
                }
                break;
            case 1:
                if (conversation1(conversationPart, conversationText.Value) == 0)
                {
                    return RunStatus.Success;
                }
                break;
            case 2:
                if (conversation2(conversationPart, conversationText.Value) == 0)
                {
                    return RunStatus.Success;
                }
                break;
            default :
                return RunStatus.Success;
        }
        return RunStatus.Running;
    }

    public int conversation0(int conversationPart, Text conversationText)
    {
        switch (conversationPart)
        {
            case 0:
                conversationText.text = "King: You have to go out and find my son's killer!";
                break;
            case 1:
                conversationText.text = "Knight: I will do as you say, sir.";
                break;
            default:
                conversationText.text = "";
                return 0;
        }
        return 1;

    }


    public int conversation1(int conversationPart, Text conversationText)
    {
        switch (conversationPart)
        {
            case 0:
                conversationText.text = "King: You have failed the mission!";
                break;
            case 1:
                conversationText.text = "Knight: But will I get another chance?";
                break;
           case 2:
                conversationText.text = "King: NO!";
                break;
            default:
                conversationText.text = "";
                return 0;
        }
        return 1;
    }

    public int conversation2(int conversationPart, Text conversationText)
    {
        switch (conversationPart)
        {
            case 0:
                conversationText.text = "King: Congradulations on completing your mission!";
                break;
            case 1:
                conversationText.text = "King: You now have two option: either execute the robber or show mercy?";
                break;
            case 2:
                conversationText.text = "King: Press Y if you want to execute and N if you want to show mercy";
                break;
            //execute
            case 3:
                conversationText.text = "";
                return 0;

            //show mercy
            case 4:
                conversationText.text = "King: He is getting away...";
                return 0;

            default:
                conversationText.text = "";
                return 0;
        }
        return 1;

    }


    public virtual RunStatus endConversation(Val<bool> isActive)
    {
        this.Body.endConverse(isActive.Value);
        return RunStatus.Success;
    }

    public virtual RunStatus sees(Val<GameObject> seer, Val<GameObject> seen, Val<object> isActive, Val<float>dist, Val<float> distForward)
    {
        if(this.Body.sees(seer.Value, seen.Value, dist.Value)|| this.Body.seesForward(seer.Value, seen.Value, distForward.Value))
        {
            Debug.Log("We see the guy");
            return RunStatus.Success;
        }
        if ((bool)(isActive.Value) == false)
        {
            return RunStatus.Failure;
        }else
        {
            return RunStatus.Failure;
        }
    }

    public virtual RunStatus punch(Val<GameObject> character, Val<Transform> body)
    {
            PunchController p = character.Value.GetComponent<PunchController>();
            p.punch(body.Value.transform, body.Value);
        
            return RunStatus.Success;
        

    }

    public virtual RunStatus kick(Val<GameObject> character, Val<Transform> body)
    {
        KickController k = character.Value.GetComponent<KickController>();



        k.kick(body.Value.transform, body.Value);

        return RunStatus.Success;


    }
    public virtual RunStatus unkick(Val<GameObject> character)
    {
        KickController k = character.Value.GetComponent<KickController>();



        k.unkick();

        return RunStatus.Success;


    }
    public virtual RunStatus unpunch(Val<GameObject> character)
    {
        PunchController k = character.Value.GetComponent<PunchController>();



        k.unpunch();

        return RunStatus.Success;


    }

    public virtual RunStatus destroy(Val<GameObject> character)
    {
        //Check if camera and if there is place it on top...
        if(character.Value.transform.Find("Main Camera")){
            Camera.main.transform.parent = character.Value.transform.parent;
            Camera.main.transform.position = character.Value.transform.position+new Vector3(0,1,0);
            Camera.main.GetComponent<mainCameraControls>().enabled = true;
            Camera.main.GetComponent<MouseLook>().enabled = true;


        }


        Destroy(character.Value);

        return RunStatus.Success;


    }

    public virtual RunStatus changeCam(Val<GameObject> character)
    {
        //Check if camera and if there is place it on top...
        if (character.Value.transform.Find("Main Camera"))
        {
            //Camera.main.transform.parent = character.Value.transform.parent;
            Camera.main.transform.position = character.Value.transform.position + new Vector3(0, 5, 0)-10*character.Value.transform.forward;
            Camera.main.GetComponent<mainCameraControls>().enabled = true;
            Camera.main.GetComponent<MouseLook>().enabled = true;


        }

        return RunStatus.Success;


    }



    public virtual RunStatus findFrontCharacter(Val<GameObject> character, Val<GameObject>director)
    {
        //first find the front person...
        //cast ray from character forward. we'll see if player in direct front
        RaycastHit hit;
        GameObject stealFrom;
        stealFrom = character.Value;
        Debug.Log("character.Value.transform.position:" + character.Value.transform.position);
        //Debug.DrawLine(character.Value.transform.forward);
        if (Physics.Raycast(character.Value.transform.position + new Vector3(0, 1.5f, 0), character.Value.transform.forward, out hit, 1))
        {
            Debug.DrawLine(character.Value.transform.position + new Vector3(0, 1.5f, 0), hit.point, Color.cyan, 1);
            Debug.Log("We hit something");
            Debug.Log("name: "+hit.transform.name);

            if (hit.transform.name.Length >=6&&hit.transform.name.Substring(0,6)== "Person")
            {
                stealFrom = hit.transform.gameObject;
            }
        }

        //Val<GameObject> stealF = Val.V(() => stealFrom);
        this.gameObject.GetComponent<BehaviorMecanim>().stealChar = stealFrom;
        

        if (stealFrom != character.Value)
        {
            Debug.Log("we found character to steal from");
            return RunStatus.Success;
        }else
        {
            Debug.Log("We did not find any character to steal from");

            return RunStatus.Failure;
        }


    }

    public virtual RunStatus increaseMoney(Val<Text> text)
    {
        string moneyString = text.Value.text;
        int money = Convert.ToInt32(moneyString);
        money = money + UnityEngine.Random.Range(0, 10);
        text.Value.text = "" + money;
        return RunStatus.Success;
    }

    public virtual RunStatus turnMovement(Val<bool> isActive, Val<GameObject> director)
    {
        if (isActive.Value)
        {
            director.Value.GetComponent<BehaviorCoordinatorb4>().movementOn = true;
            director.Value.GetComponent<BehaviorCoordinatorb4Phase1>().movementOn = true;
            //Debug.Log("movement is ON");
        }
        else
        {
            director.Value.GetComponent<BehaviorCoordinatorb4>().movementOn = false;
            director.Value.GetComponent<BehaviorCoordinatorb4Phase1>().movementOn = false;
            // Debug.Log("Movement is OFF");
        }
        return RunStatus.Success;

    }

    public virtual RunStatus changeArcName(Val<string> newArc, Val<GameObject> director)
    {
        
            director.Value.GetComponent<BehaviorCoordinatorb4>().currentArc = newArc.Value;
        director.Value.GetComponent<BehaviorCoordinatorb4Phase1>().currentArc = newArc.Value;

        return RunStatus.Success;
    }

    public virtual RunStatus winGame(Val<Text> text, Val<Text> conversationText)
    {
        string moneyString = text.Value.text;
        int money = Convert.ToInt32(moneyString);
        money = money -20;
        text.Value.text = "" + money;
        conversationText.Value.text="You bought from a vender a magic item that kills everyone";
        return RunStatus.Success;
    }

    public virtual RunStatus convertSpeeds(Val<GameObject> chased,  Val<GameObject> chaser1, Val<GameObject> chaser2, Val<float> factorSpeed)
    {
        chaser1.Value.GetComponent<UnitySteeringController>().maxSpeed = chased.Value.GetComponent<UnitySteeringController>().maxSpeed * factorSpeed.Value;
        chaser2.Value.GetComponent<UnitySteeringController>().maxSpeed = chased.Value.GetComponent<UnitySteeringController>().maxSpeed * factorSpeed.Value;
        return RunStatus.Success;
    }
}

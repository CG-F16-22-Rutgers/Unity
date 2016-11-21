using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;
using UnityEngine.UI;
using RootMotion.FinalIK;

public enum AnimationLayer
{
    Hand,
    Body,
    Face,
}

public class BehaviorMecanim : MonoBehaviour
{
    public GameObject stealChar;

    [HideInInspector]
    public CharacterMecanim Character = null;

    void Awake() { this.Initialize(); }

    protected void Initialize()
    {
        this.Character = this.GetComponent<CharacterMecanim>();
    }

    protected void StartTree(
        Node root,
        BehaviorObject.StatusChangedEventHandler statusChanged = null)
    {
    }

    #region Helper Nodes

    #region Navigation
    /// <summary>
    /// Approaches a target
    /// </summary>
    public Node Node_GoTo(Val<Vector3> targ)
    {
        return new LeafInvoke(
            () => this.Character.NavGoTo(targ),
            () => this.Character.NavStop());
    }

    public Node Node_NudgeTo(Val<Vector3> targ)
    {
        return new LeafInvoke(
            () => this.Character.NavNudgeTo(targ),
            () => this.Character.NavStop());
    }

    //public Node Node_GoAlongPoints(Val<Vector3>[] targ)
    //{
    //    return new LeafInvoke(
    //        () => this.Character.NavAlongPoints(targ),
    //        () => this.Character.NavStop());
    //}

    // TODO: No speed support yet! - AS 5/4/14
    ///// <summary>
    ///// Approaches a target with a certain speed
    ///// </summary>
    //public Node Node_GoTo(Val<Vector3> targ, Val<float> speed)
    //{
    //    this.Character.SetSpeed(speed.Value);
    //    return new LeafInvoke(
    //        () => this.Character.NavGoTo(targ),
    //        () => this.Character.NavStop());
    //}

    /// <summary>
    /// Orient towards a target position
    /// </summary>
    /// <param name="targ"></param>
    /// <returns></returns>
    public Node Node_OrientTowards(Val<Vector3> targ)
    {
        return new LeafInvoke(
            () => this.Character.NavTurn(targ),
            () => this.Character.NavOrientBehavior(
                OrientationBehavior.LookForward));
    }

    /// <summary>
    /// Orient towards a target position
    /// </summary>
    /// <param name="targ"></param>
    /// <returns></returns>
    public Node Node_Orient(Val<Quaternion> direction)
    {
        return new LeafInvoke(
            () => this.Character.NavTurn(direction),
            () => this.Character.NavOrientBehavior(
                OrientationBehavior.LookForward));
    }

    /// <summary>
    /// Approaches a target at a given radius
    /// </summary>
    public Node Node_GoToUpToRadius(Val<Vector3> targ, Val<float> dist)
    {
        Func<RunStatus> GoUpToRadius =
            delegate()
            {
                Vector3 targPos = targ.Value;
                Vector3 curPos = this.transform.position;
                if ((targPos - curPos).magnitude < dist.Value)
                {
                    this.Character.NavStop();
                    return RunStatus.Success;
                }
                return this.Character.NavGoTo(targ);
            };

        return new LeafInvoke(
            GoUpToRadius,
            () => this.Character.NavStop());
    }
    #endregion

    #region Reach
    /// <summary>
    /// Tries to reach target with right or left hand.
    /// </summary>
    public Node Node_StartInteraction(Val<FullBodyBipedEffector> effector, Val<InteractionObject> obj)
    {
        return new LeafInvoke(
            () => this.Character.StartInteraction(effector, obj),
            () => this.Character.StopInteraction(effector));
    }

    /// <summary>
    /// Tries to reach target with right or left hand.
    /// </summary>
    public Node Node_ResumeInteraction(Val<FullBodyBipedEffector> effector)
    {
        return new LeafInvoke(
            () => this.Character.ResumeInteraction(effector),
            () => this.Character.StopInteraction(effector));
    }

    /// <summary>
    /// Stops reaching the target for the specified hand.
    /// </summary>
    public Node Node_StopInteraction(Val<FullBodyBipedEffector> effector)
    {
        return new LeafInvoke(
            () => this.Character.StopInteraction(effector));
    }

    /// <summary>
    /// Waits for a trigger on a specific effector
    /// </summary>
    public Node Node_WaitForTrigger(Val<FullBodyBipedEffector> effector)
    {
        return new LeafInvoke(
            () => this.Character.WaitForTrigger(effector));
    }

    /// <summary>
    /// Waits for a specific effector to finish its interaction
    /// </summary>
    public Node Node_WaitForFinish(Val<FullBodyBipedEffector> effector)
    {
        return new LeafInvoke(
            () => this.Character.WaitForFinish(effector));
    }
    #endregion

    #region HeadLook
    public Node Node_HeadLook(Val<Vector3> targ)
    {
        return new LeafInvoke(
                () => this.Character.HeadLookAt(targ),
                () => this.Character.HeadLookStop());
    }

    public Node Node_HeadLookTurnFirst(Val<Vector3> targ)
    {
        return new Sequence(
            new LeafInvoke(() => this.Character.NavTurn(targ)),
            Node_HeadLook(targ));
    }

    public Node Node_HeadLookStop()
    {
        return new LeafInvoke(
            () => this.Character.HeadLookStop());
    }

    #endregion

    #region Animation
    /// <summary>
    /// A Hand animation is started if the bool is true, the hand animation 
    /// is stopped if the bool is false
    /// </summary>
    public Node Node_HandAnimation(Val<string> gestureName, Val<bool> start)
    {

        return new LeafInvoke(
            () => this.Character.HandAnimation(gestureName, start),
            () => this.Character.HandAnimation(gestureName, false));
    }

    /// <summary>
    /// A Face animation is started if the bool is true, the face animation 
    /// is stopped if the bool is false
    /// </summary>
    public Node Node_FaceAnimation(Val<string> gestureName, Val<bool> start)
    {
        return new LeafInvoke(
            () => this.Character.FaceAnimation(gestureName, start),
            () => this.Character.FaceAnimation(gestureName, false));
    }

    public Node Node_BodyAnimation(Val<string> gestureName, Val<bool> start)
    {
        return new LeafInvoke(
            () => this.Character.BodyAnimation(gestureName, start),
            () => this.Character.BodyAnimation(gestureName, false));
    }

    


    #endregion

    #endregion

    #region Helper Subtrees

    /// <summary>
    /// Plays a gesture of a determined type for a given duration
    /// </summary>
    public Node ST_PlayGesture(
        Val<string> gestureName,
        Val<AnimationLayer> layer,
        Val<long> duration)
    {
        switch (layer.Value)
        {
            case AnimationLayer.Hand:
                return this.ST_PlayHandGesture(gestureName, duration);
            case AnimationLayer.Body:
                return this.ST_PlayBodyGesture(gestureName, duration);
            case AnimationLayer.Face:
                return this.ST_PlayFaceGesture(gestureName, duration);
        }
        return null;
    }

    /// <summary>
    /// Plays a hand gesture for a duration in miliseconds
    /// </summary>
    public Node ST_PlayHandGesture(
        Val<string> gestureName, Val<long> duration)
    {
        return new DecoratorCatch(
            () => this.Character.HandAnimation(gestureName, false),
            new Sequence(
                Node_HandAnimation(gestureName, true),
                new LeafWait(duration),
                Node_HandAnimation(gestureName, false)));
    }

    /// <summary>
    /// Plays a body gesture for a duration in miliseconds
    /// </summary>
    public Node ST_PlayBodyGesture(
        Val<string> gestureName, Val<long> duration)
    {
        return new DecoratorCatch(
            () => this.Character.BodyAnimation(gestureName, false),
            new Sequence(
            this.Node_BodyAnimation(gestureName, true),
            new LeafWait(duration),
            this.Node_BodyAnimation(gestureName, false)));
    }

    /// <summary>
    /// Plays a face gesture for a duration in miliseconds
    /// </summary>
    public Node ST_PlayFaceGesture(
        Val<string> gestureName, Val<long> duration)
    {
        return new DecoratorCatch(
            () => this.Character.FaceAnimation(gestureName, false),
            new Sequence(
                Node_FaceAnimation(gestureName, true),
                new LeafWait(duration),
                Node_FaceAnimation(gestureName, false)));
    }

    /// <summary>
    /// Turns to face a target position
    /// </summary>
    public Node ST_TurnToFace(Val<Vector3> target)
    {
        Func<RunStatus> turn =
            () => this.Character.NavTurn(target);

        Func<RunStatus> stopTurning =
            () => this.Character.NavOrientBehavior(
                OrientationBehavior.LookForward);

        return
            new Sequence(
                new LeafInvoke(turn, stopTurning));
    }
    #endregion


    public Node Node_BeginConversation(Val<GameObject> guy1, Val<GameObject> guy2, Val<bool> start)
    {
        return new LeafInvoke(
            () => this.Character.startConversation(guy1, guy2, start),
            () => this.Character.startConversation(guy1, guy2, false));
    }


    public Node Node_ConversationInProcess(Val<bool> start, Val<object> conversationPart, Val<object> conversationNumber, Val<Text> conversationText)
    {
        return new LeafInvoke(
            () => this.Character.progressConversation(start, conversationPart, conversationNumber, conversationText));
            //() => this.Character.progressConversation(false, conversationPart, conversationNumber, conversationText));
    }


    public Node Node_EndConversation(Val<bool> start)
    {
        return new LeafInvoke(
            () => this.Character.endConversation( start),
            () => this.Character.endConversation(false));
    }

    public Node Node_Sees(Val<GameObject> seer, Val<GameObject> seen, Val<object> isActive, Val<float> dist, Val<float> distForward)
    {
        return new LeafInvoke(
            () => this.Character.sees(seer, seen, isActive, dist, distForward));
    }

    public Node Node_Punch(Val<GameObject> character, Val<Transform> body)
    {
        return new LeafInvoke(
            () => this.Character.punch(character, body));
    }

    public Node Node_Kick(Val<GameObject> character, Val<Transform> body)
    {
        return new LeafInvoke(
            () => this.Character.kick(character, body));
    }
    public Node Node_unkick(Val<GameObject> character)
    {
        return new LeafInvoke(
            () => this.Character.unkick(character));
    }
    public Node Node_unpunch(Val<GameObject> character)
    {
        return new LeafInvoke(
            () => this.Character.unpunch(character));
    }
    public Node Node_destroy(Val<GameObject> character)
    {
        return new LeafInvoke(
            () => this.Character.destroy(character));
    }

   // public void setStealChar(GameObject c)
   // {
   //    stealChar=c;
   // }



    public Node ST_stealFromPlayerFront(Val<GameObject> character, Val<GameObject> director, Val<Text> text)
    {

        Val<string> newArc = "walkAround";

        //return new LeafInvoke(
        //   () => this.Character.startConversation(character, stealF, true),
        //   () => this.Character.startConversation(character, stealF, false));
        return new Sequence(
            (new LeafInvoke(
                () => this.Character.changeArcName(newArc, director))),
            new LeafInvoke(
                () => this.Character.findFrontCharacter(character, director)
            ),
            //character has been found so now, walk up to his back
            //new LeafInvoke(
            //    () => this.Character.NavGoTo(stealChar.transform.position - stealChar.transform.forward),
            //    () => this.Character.NavStop()
            //),
            // new LeafInvoke(
             //   () => this.Character.startConversation(character, stealChar, true),
            //    () => this.Character.startConversation(character, stealChar, false)),
              //now reach down in pocket
              //Node_StartInteraction(FullBodyBipedEffector.RightHand,(InteractionObject)(stealChar.transform.Find("Inter_StealWallet").GetComponent<InteractionObject>()))

              //just use hand animation
              /*new LeafInvoke(
                 () => this.Character.HandAnimation("REACHRIGHT", true),
                 () => this.Character.HandAnimation("REACHRIGHT", false))*/


              //InteractionObject
              new LeafInvoke(
                () => this.Character.StartInteraction(FullBodyBipedEffector.RightHand, stealChar.transform.parent.GetChild(4).GetChild(3).GetComponent<InteractionObject>()),
                () => this.Character.StopInteraction(FullBodyBipedEffector.RightHand)),
              new LeafWait(700),
              new LeafInvoke(
                () => this.Character.StopInteraction(FullBodyBipedEffector.RightHand)),
              new LeafInvoke(
                () => this.Character.increaseMoney(text)),
              (new LeafInvoke(
                () => stealChar.transform.parent.GetComponent<CharacterMecanim>().HandAnimation("SURPRISED", true)
                )),
                new LeafWait(5000),
                (new LeafInvoke(
                () => stealChar.transform.parent.GetComponent<CharacterMecanim>().HandAnimation("SURPRISED", false)
                ))//,
                //(new LeafInvoke(
                //() => this.Character.changeArcName(newArc, director)))

       );
    }

    public Node ST_stealFromGuy(Val<GameObject> character, Val<GameObject> stealFrom, Val<GameObject> director, Val<Text> text)
    {

        Val<string> newArc = "walkAround";

        //return new LeafInvoke(
        //   () => this.Character.startConversation(character, stealF, true),
        //   () => this.Character.startConversation(character, stealF, false));
        return new Sequence(
            (new LeafInvoke(
                () => this.Character.changeArcName(newArc, director))),
            
              //character has been found so now, walk up to his back
              //new LeafInvoke(
              //    () => this.Character.NavGoTo(stealChar.transform.position - stealChar.transform.forward),
              //    () => this.Character.NavStop()
              //),
              // new LeafInvoke(
              //   () => this.Character.startConversation(character, stealChar, true),
              //    () => this.Character.startConversation(character, stealChar, false)),
              //now reach down in pocket
              //Node_StartInteraction(FullBodyBipedEffector.RightHand,(InteractionObject)(stealChar.transform.Find("Inter_StealWallet").GetComponent<InteractionObject>()))

              //just use hand animation
              /*new LeafInvoke(
                 () => this.Character.HandAnimation("REACHRIGHT", true),
                 () => this.Character.HandAnimation("REACHRIGHT", false))*/


              //InteractionObject
              new LeafInvoke(
                () => this.Character.StartInteraction(FullBodyBipedEffector.RightHand, stealFrom.Value.transform.GetChild(4).GetChild(3).GetComponent<InteractionObject>()),
                () => this.Character.StopInteraction(FullBodyBipedEffector.RightHand)),
              new LeafWait(700),
              new LeafInvoke(
                () => this.Character.StopInteraction(FullBodyBipedEffector.RightHand)),
              new LeafInvoke(
                () => this.Character.increaseMoney(text)),
              (new LeafInvoke(
                () => stealFrom.Value.transform.GetComponent<CharacterMecanim>().HandAnimation("SURPRISED", true)
                )),
                new LeafWait(5000),
                (new LeafInvoke(
                () => stealFrom.Value.transform.GetComponent<CharacterMecanim>().HandAnimation("SURPRISED", false)
                ))//,
                  //(new LeafInvoke(
                  //() => this.Character.changeArcName(newArc, director)))

       );
    }

    public Node ST_buyFromVender(Val<GameObject> character, Val<GameObject> salesman, Val<GameObject> director, Val<string> newArc)
    {
        //first he faces vendor
        Func<RunStatus> turn =
            () => this.Character.NavTurn(salesman.Value.transform.position);

        Func<RunStatus> stopTurning =
            () => this.Character.NavOrientBehavior(
                OrientationBehavior.LookForward);

       // return
        //    new Sequence(
         //       new LeafInvoke(turn, stopTurning));


        return new Sequence(


            //         new LeafInvoke(
            //)
            // ,        
            //new LeafInvoke(turn, stopTurning),
            Node_HeadLook(salesman.Value.transform.position)
            ,

            (new LeafInvoke(
                () => this.Character.turnMovement(false,director))),

            (new LeafInvoke(
                () => this.Character.HandAnimation("CALLOVER", true))),
            new LeafWait(950),
            (new LeafInvoke(
                () => this.Character.HandAnimation("CALLOVER", false))),
            (new LeafInvoke(
                () => this.Character.startConversation(character, salesman, true),
                () => this.Character.startConversation(character, salesman, false)
            )),
            new LeafWait(100),
            (new LeafInvoke(
                () => this.Character.HandAnimation("THINK", true)
            )),
            new LeafWait(3000),
            (new LeafInvoke(
                () => this.Character.HandAnimation("THINK", false)
            )),
            new LeafWait(900),
            (new LeafInvoke(
                () => this.Character.HandAnimation("POINTING", true)
            )),
            new LeafWait(5000),
            (new LeafInvoke(
                () => this.Character.HandAnimation("POINTING", false)
            )),
            (new LeafInvoke(
                () => salesman.Value.GetComponent<CharacterMecanim>().FaceAnimation("HEADNOD", true)
            )),
            new LeafWait(3000),
            (new LeafInvoke(
                () => salesman.Value.GetComponent<CharacterMecanim>().FaceAnimation("HEADNOD", false)
            )),
            (new LeafInvoke(
                () => Character.BodyAnimation("PICKUPRIGHT", true)
            )),
             new LeafWait(3000),
             (new LeafInvoke(
                () => Character.BodyAnimation("PICKUPRIGHT", false)
            )),
            (new LeafInvoke(
                () => salesman.Value.GetComponent<CharacterMecanim>().HandAnimation("WAVE", true)
            )),

            new LeafWait(7000),
            (new LeafInvoke(
                () => salesman.Value.GetComponent<CharacterMecanim>().HandAnimation("WAVE", false)
            )),
            (new LeafInvoke(
                () => this.Character.turnMovement(true, director))),
            (new LeafInvoke(
                () => this.Character.changeArcName(newArc,director))),
             (new LeafInvoke(
                () => this.Character.endConversation(true)))





       );
    }


    public Node winGameMessages(Val<Text> t, Val<Text> dt)
    {  
        return new Sequence(
            new LeafInvoke(
                () => this.Character.winGame(t, dt)
            )
     
       );
    }

    public Node changeArcName(Val<string> t, Val<GameObject> dir)
    {
        return (new LeafInvoke(
                () => this.Character.changeArcName(t, dir)));
    }

    public Node changeCam(Val<GameObject>character)
    {
        return (new LeafInvoke(
                () => this.Character.changeCam(character)));
    }

    //Behavior 3 chase
    public Node chase(Val<GameObject> chased, Val<Vector3> chasedPosition, Val<float> radius, Val<GameObject> chaser1, Val<GameObject> chaser2, Val<float> factorSpeed, Val<GameObject> director )
    {
        return new Sequence(
             new LeafInvoke(
                      () => Character.convertSpeeds(chased, chaser1, chaser2, factorSpeed)
             ),
            (new LeafInvoke(
                () => this.Character.turnMovement(false, director))),
            new SelectorParallel(
                chaser1.Value.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasedPosition, radius),
                chaser2.Value.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasedPosition, radius),
                /*new LeafInvoke(
                    () => chaser1.Value.GetComponent<CharacterMecanim>().NavGoTo(chasePoint),
                    () => chaser1.Value.GetComponent<CharacterMecanim>().NavStop()),
                new LeafInvoke(
                    () => chaser2.Value.GetComponent<CharacterMecanim>().NavGoTo(chasePoint),
                    () => chaser2.Value.GetComponent<CharacterMecanim>().NavStop()),*/
                new LeafInvoke(
                    () => chased.Value.GetComponent<CharacterMecanim>().NavGoTo(chased.Value.transform.position + 2 * (chaser1.Value.transform.forward + chaser2.Value.transform.forward)),
                    () => chased.Value.GetComponent<CharacterMecanim>().NavStop())

            ),
            new LeafInvoke(
                () => Character.HandAnimation("CRY", true)),
            chaser1.Value.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasedPosition, radius),
            chaser2.Value.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasedPosition, radius)
                    

        //(new Selector
        //     (this.ST_Approach(kingFrontPosition, knight))),
        //if we see robber, return success
        //     (new Selector
        //    (this.ST_Approach(knight.transform, robber)))
        //when king stops thinking, return success
        //10000//random...
        );
    }

}

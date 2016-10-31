using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;
using UnityEngine.UI;

public class BehaviorCoordinator : MonoBehaviour
{
    public Transform wander1;
    public Transform wander2;
    public Transform wander3;
    public Transform wander4;
    public Transform wander5;
    public Transform wander6;
    public Transform wander7;
    public Transform wander8;
    public Transform wander9;
    public GameObject participant;
    public Text conversationText;

    public GameObject king;
    public GameObject knight;
    public GameObject robber;
    public Transform robberFace;
    public Transform robberChest;

    private BehaviorAgent behaviorAgent;
    // Use this for initialization
    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();

        conversationPart = (object)(0);
        conversationNumber = (object)(0);
        conversationPart2 = (object)(-2);
        conversationNumber2 = (object)(1);
        conversationPart3 = (object)(-2);
        conversationNumber3 = (object)(2);

    }


    //if press Enter, update conversation
    public object conversationPart;
    public object conversationNumber;
    public object conversationPart2;
    public object conversationNumber2;
    public object conversationPart3;
    public object conversationNumber3;
    public object isActive1;
    public object isActive2;

 

    bool yesWasPressed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            conversationPart = (object)(((int)conversationPart) + 1);
            Debug.Log("conversatonPart: " + (int)conversationPart);
            conversationPart2 = (object)(((int)conversationPart2) + 1);
            conversationPart3 = (object)(((int)conversationPart3) + 1);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            yesWasPressed = true;
            conversationPart3 = (object)(((int)conversationPart3) + 1);
        }else if (Input.GetKeyDown(KeyCode.N))
        {
            yesWasPressed = false;
            conversationPart3 = (object)(((int)conversationPart3) + 2);
        }
    }

    public bool wasYesPressed()
    {
        return yesWasPressed;
    }

    protected Node ST_beginConversation(GameObject guy1, GameObject guy2)
    {
        Debug.Log("We begin talk");
        conversationPart = (object)(0);
        conversationNumber = (object)(0);
        conversationPart2 = (object)(-2);
        conversationNumber2 = (object)(1);
        //Val<Vector3> position = Val.V(() => target.position);
        Val<GameObject> guy1Val = Val.V(() => guy1);
        Val<GameObject> guy2Val = Val.V(() => guy2);
        return new Sequence(knight.GetComponent<BehaviorMecanim>().Node_BeginConversation(guy1, guy2, true), new LeafWait(10));
    }

    protected Node ST_processConversation(object convNum, object convPart)
    {
        Val<object> convP = Val.V(() => (object)conversationPart);
        Val<object> convN = Val.V(() => (object)conversationNumber);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(knight.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }
    protected Node ST_processConversation2(object convNum, object convPart)
    {
        Val<object> convP = Val.V(() => (object)conversationPart2);
        Val<object> convN = Val.V(() => (object)conversationNumber2);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(knight.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }
    protected Node ST_processConversation3(object convNum, object convPart)
    {
        Val<object> convP = Val.V(() => (object)conversationPart3);
        Val<object> convN = Val.V(() => (object)conversationNumber3);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(knight.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }
    protected Node ST_endConversation()
    {
        conversationText.text = "";
        return new Sequence(knight.GetComponent<BehaviorMecanim>().Node_EndConversation(true));
    }

    protected Node ST_Approach(Transform target, GameObject character)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(character.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(10));
    }



    protected Node ST_TurnTo(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(knight.GetComponent<BehaviorMecanim>().ST_TurnToFace(position));
    }

    //   protected Node ST_talk(GameObject guy1, GameObject guy2);
    //   {
    //       Val<Vector3> guy1Val = Val.V(() => guy1);
    //   Val<Vector3> guy2Val = Val.V(() => guy2);
    //
    //        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_Conversation(guy1Val, guy2Val));
    //    }

    protected Node ST_ApproachAndWait(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(knight.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node ST_IdleKing(string gestureName, long duration)
    {
        Val<string> ges = Val.V(() => gestureName);
        Val<long> dur = Val.V(() => duration);
        return new Sequence(king.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(ges, dur));
    }

    protected Node ST_playFaceAnimation(string gestureName, bool start, GameObject character)
    {
        Val<string> ges = Val.V(() => gestureName);
        Val<bool> s = Val.V(() => start);
        return new Sequence(character.GetComponent<BehaviorMecanim>().Node_FaceAnimation(ges, s));
    }

    protected Node ST_Sees(GameObject king, GameObject robber)
    {
        bool b = true;
        isActive1 = (object)b;
        Val<GameObject> ges = Val.V(() => king);
        Val<GameObject> dur = Val.V(() => robber);
        Val<object> isActive = Val.V(() => isActive1);
        return new Sequence(knight.GetComponent<BehaviorMecanim>().Node_Sees(ges, dur, isActive));
    }
    protected Node ST_Sees2(GameObject king, GameObject robber)
    {
        bool b = false;
        isActive2 = (object)b;
        Val<GameObject> ges = Val.V(() => king);
        Val<GameObject> dur = Val.V(() => robber);
        Val<object> isActive = Val.V(() => isActive2);
        return new Sequence(knight.GetComponent<BehaviorMecanim>().Node_Sees(ges, dur, isActive));
    }
    protected Node ST_punch(GameObject character, Transform body)
    {
        Val<GameObject> c = Val.V(() => character);
        Val<Transform> b = Val.V(() => body);
        return new Sequence(character.GetComponent<BehaviorMecanim>().Node_Punch(c, b));
    }
    protected Node ST_kick(GameObject character, Transform body)
    {
        Val<GameObject> c = Val.V(() => character);
        Val<Transform> b = Val.V(() => body);
        return new Sequence(character.GetComponent<BehaviorMecanim>().Node_Punch(c, b));
    }
    protected Node ST_unkick()
    {
        Val<GameObject> c = Val.V(() => knight);
        return new Sequence(knight.GetComponent<BehaviorMecanim>().Node_unkick(c));
    }
    protected Node ST_unpunch()
    {
        Val<GameObject> c = Val.V(() => knight);
        return new Sequence(knight.GetComponent<BehaviorMecanim>().Node_unpunch(c));
    }

    public Transform kingFrontPosition;

    protected Node BuildTreeRoot()
    {
        Node roaming =
        
            new Sequence(
                #region Knight talks to king
                (new Sequence(
                    //knight walks to king
                    new DecoratorInvert(
                        (new DecoratorLoop(
                            new DecoratorInvert(
                                new Sequence(
                                        (this.ST_Approach(kingFrontPosition, knight))
                                )
                            )
                        ))
                    ),
                    //knight turns to king
                    this.ST_TurnTo(king.transform),
                    //knight starts talking
                    this.ST_beginConversation(king, knight),
                    //knight and king talk...
                    new DecoratorInvert(
                        (new DecoratorLoop
                            (new Sequence(
                                new DecoratorInvert(
                                    (this.ST_processConversation(conversationNumber, conversationPart))
                               )
                            ))
                        )
                    ),
                    //knight ends talking
                    this.ST_endConversation()
                )),
        #endregion

                //#region find the robber and bring him back if found
                new Sequence(
                    #region find the robber unless time runs out
                    (new DecoratorInvert
                        (new DecoratorLoop (
                            (new DecoratorInvert
                                (new SelectorParallel(
                                     //see if either the king is done thinking or if the knight sees the robber, we return success
                                    
                                    //if we see robber, return success
                                    (this.ST_Sees(knight, robber)),
                                    //when king stops thinking, return success
                                    (this.ST_IdleKing("THINK", 90000)),//10000//random...
                                    //walk through all the rooms
                                    (new SequenceShuffle(
                                        (this.ST_ApproachAndWait(wander1)),
                                        (this.ST_ApproachAndWait(wander2)),
                                        (this.ST_ApproachAndWait(wander3)),
                                        (this.ST_ApproachAndWait(wander4)),
                                        (this.ST_ApproachAndWait(wander5)),
                                        (this.ST_ApproachAndWait(wander6)),
                                        (this.ST_ApproachAndWait(wander7)),
                                        (this.ST_ApproachAndWait(wander8)),
                                        (this.ST_ApproachAndWait(wander9))
                                    ))      
                                ))
                            )
                        ))
                    ),
                    #endregion
                    //now we have either found the robber or not
                    //we have to check whether or not we found him
                    (new DecoratorInvert
                        (new Sequence(
                            //if dont see the knight, go on to sad state
                            (new DecoratorInvert
                                (new Sequence(
                                    //if see robber, we go on and take him to the king
                                    (this.ST_Sees2(knight, robber)),
                                    (this.ST_playFaceAnimation("ROAR",true, robber)),
                                    //(this.ST_playFaceAnimation("ACKNOWLEDGE", true, knight)),
                                    //move the knight to the king
                                    //move the robber to the knight
                                    (new DecoratorInvert
                                        (new DecoratorLoop(
                                            new DecoratorInvert(
                                                new SelectorParallel(
                                                    (new Selector
                                                    (this.ST_Approach(kingFrontPosition, knight))),
                                                    //if we see robber, return success
                                                    (new Selector
                                                    (this.ST_Approach(knight.transform, robber)))
                                                //when king stops thinking, return success
                                                //10000//random...
                                                )
                                            )
                                        ))
                                    ),
                                    this.ST_TurnTo(king.transform),
                                    //knight starts talking
                                    this.ST_beginConversation(king, knight),
                                    //knight and king talk...
                                    new DecoratorInvert(
                                        (new DecoratorLoop
                                            (new Sequence(
                                                new DecoratorInvert(
                                                    (this.ST_processConversation3(conversationNumber3, conversationPart3))
                                                )
                                            ))
                                        )
                                    ),
                                this.ST_endConversation(),
                                (this.ST_playFaceAnimation("ROAR", false, robber)),
                                (new Sequence(
                                    (new DecoratorInvert
                                        (new Sequence (
                                            (new LeafAssert(wasYesPressed)),
                                            //execute
                                            //DO EXECUTION----------------------------------------------------------------------------------
                                            (new TreeSharpPlus.RandomNode(
                                                
                                                (new Sequence (this.ST_kick(knight, robberChest), this.ST_unkick())),

                                                (new Sequence(this.ST_punch(king, robberFace))), this.ST_unpunch())),
                                            //-----------------------------------------------------------------------------------------------
                                            //and then stand there proudly
                                            //this.ST_beginConversation(king, robber),
                                            (new DecoratorLoop(ST_playFaceAnimation("HEADNOD", true, knight)))
                                        ))
                                    ),
                                   
                                        ( new SelectorParallel (
                                            //this.ST_beginConversation(knight, robber),
                                            (new DecoratorLoop(
                                            (ST_playFaceAnimation("SAD", true, knight)))),
                                            (new DecoratorLoop(
                                                (new TreeSharpPlus.RandomNode (
                                                    (this.ST_Approach(wander1, robber)),
                                                    (this.ST_Approach(wander1, robber)),
                                                    (this.ST_Approach(wander1, robber)),
                                                    (this.ST_Approach(wander1, robber)),
                                                    (this.ST_Approach(wander1, robber)),
                                                    (this.ST_Approach(wander1, robber)),
                                                    (this.ST_Approach(wander1, robber))
                                                ))
                                            ))
                                       ))
                                   
                                   //(ST_playFaceAnimation("SAD", true, knight))
                                ))
                                


                                ))
                            ),
                            #region approach king to tell him the bad news
                            (new Sequence(
                                //knight walks to king
                                new DecoratorInvert(
                                    (new DecoratorLoop(
                                        new DecoratorInvert(
                                            new Sequence(
                                                (this.ST_Approach(kingFrontPosition,knight))
                                            )
                                        )
                                    ))
                                ),
                                this.ST_TurnTo(king.transform),
                                //knight starts talking
                                this.ST_beginConversation(king, knight),
                                //knight and king talk...
                                new DecoratorInvert(
                                    (new DecoratorLoop
                                        (new Sequence(
                                            new DecoratorInvert(
                                                (this.ST_processConversation2(conversationNumber2, conversationPart2))
                                            )
                                        ))
                                    )
                                ),
                                this.ST_endConversation(),
                                //now knight is very sad for the rest of time
                                (new DecoratorLoop(ST_playFaceAnimation("SAD",true,knight))),
                                (new DecoratorLoop(ST_playFaceAnimation("LOOKAWAY", true, king)))
                            ))
                            #endregion
                        ))
                    )//,
                    //this.ST_beginConversation(king, robber)
            )




);

        return roaming;
    }
}

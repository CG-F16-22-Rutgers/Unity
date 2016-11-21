using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;
using UnityEngine.UI;

public class BehaviorCoordinatorb4 : MonoBehaviour
{
    //stores
    public Transform wander1;
    public Transform wander2;
    public Transform wander3;
    public Transform wander4;
    public Transform wander5;
    public Transform wander6;

    public GameObject participant;
    public Text conversationText;
    public Text text;

    //public agents
    public GameObject robber;
    public GameObject policeman;
    public GameObject policeman2;
    public GameObject person1;
    public GameObject person2;
    public GameObject person3;
    public GameObject person4;
    public GameObject person5;
    public GameObject person6;
    public GameObject person7;
    public GameObject person8;
    public GameObject person9;
    public GameObject person10;

    public GameObject sales1;
    public GameObject sales2;
    public GameObject sales3;
    public GameObject sales4;
    public GameObject sales5;
    public GameObject sales6;

    public Transform robberFace;
    public Transform robberChest;

    public GameObject[] policemanWaypoints;

    public bool movementOn;

    private BehaviorAgent behaviorAgent;
    // Use this for initialization

    //Locomotion
    protected LocomotionController locomotion;

    public Transform chaseBehindPoint;

    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
        movementOn = true;
        conversationPart = (object)(0);
        conversationNumber = (object)(0);
        conversationPart2 = (object)(-2);
        conversationNumber2 = (object)(1);
        conversationPart3 = (object)(-2);
        conversationNumber3 = (object)(2);

        //make locomotion to robber
        Animator animator = robber.GetComponent<Animator>();
        locomotion=new LocomotionController(animator);
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
        if (movementOn)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("old current arc: " + currentArc);
                if (currentArc == "walkAround")
                {
                    currentArc = "stealFromClose";
                }
               

            }

            float speed = 0;

            //running
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    speed = 10;
                    locomotion.Do(10, 0);

                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    speed = -10;
                    locomotion.Do(-10, 0);

                }

            }

            //walking
            if (Input.GetKey(KeyCode.W))
            {
                speed = 5;
                locomotion.Do(5, 0);

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                speed = -5;
                locomotion.Do(-5, 0);

            }
            //stay on spot
            else
            {
                speed = .5f;
            }

            //turning
            if (Input.GetKey(KeyCode.A))
            {
                locomotion.Do(speed, -25);

            }
            if (Input.GetKey(KeyCode.D))
            {
                locomotion.Do(speed, 25);

            }
        }
    }

    public bool wasYesPressed()
    {
        return yesWasPressed;
    }

    //old behaviors: ------------------------------------------------------------------------------------------------
    protected Node ST_beginConversation(GameObject guy1, GameObject guy2)
    {
        //Debug.Log("We begin talk");
        conversationPart = (object)(0);
        conversationNumber = (object)(0);
        conversationPart2 = (object)(-2);
        conversationNumber2 = (object)(1);
        //Val<Vector3> position = Val.V(() => target.position);
        Val<GameObject> guy1Val = Val.V(() => guy1);
        Val<GameObject> guy2Val = Val.V(() => guy2);
        return new Sequence(guy1.GetComponent<BehaviorMecanim>().Node_BeginConversation(guy1, guy2, true), new LeafWait(10));
    }

    protected Node ST_processConversation(object convNum, object convPart)
    {
        Val<object> convP = Val.V(() => (object)conversationPart);
        Val<object> convN = Val.V(() => (object)conversationNumber);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }
    protected Node ST_processConversation2(object convNum, object convPart)
    {
        Val<object> convP = Val.V(() => (object)conversationPart2);
        Val<object> convN = Val.V(() => (object)conversationNumber2);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }
    protected Node ST_processConversation3(object convNum, object convPart)
    {
        Val<object> convP = Val.V(() => (object)conversationPart3);
        Val<object> convN = Val.V(() => (object)conversationNumber3);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }
    protected Node ST_endConversation()
    {
        conversationText.text = "";
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_EndConversation(true));
    }

    protected Node ST_Approach(Transform target, GameObject character)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(character.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(10));
    }



    protected Node ST_TurnTo(Transform target, GameObject character)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(character.GetComponent<BehaviorMecanim>().ST_TurnToFace(position));
    }

    protected Node ST_ApproachAndWait(Transform target, GameObject character)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(character.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(UnityEngine.Random.Range(900,4000)));
    }

    protected Node ST_IdleKing(string gestureName, long duration)
    {
        Val<string> ges = Val.V(() => gestureName);
        Val<long> dur = Val.V(() => duration);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(ges, dur));
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
        Val<float> d = Val.V(() => 50f);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_Sees(ges, dur, isActive, d,d));
    }
    protected Node ST_Sees2(GameObject king, GameObject robber)
    {
        bool b = false;
        isActive2 = (object)b;
        Val<GameObject> ges = Val.V(() => king);
        Val<GameObject> dur = Val.V(() => robber);
        Val<object> isActive = Val.V(() => isActive2);
        Val<float> d = Val.V(() => 50f);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_Sees(ges, dur, isActive,d,d));
    }
    protected Node ST_punch(GameObject character, Transform body)
    {
        Val<GameObject> c = Val.V(() => character);
        Val<Transform> b = Val.V(() => body);
        return new Sequence(character.GetComponent<BehaviorMecanim>().Node_Punch(c, b), new LeafWait(100));
    }
    protected Node ST_kick(GameObject character, Transform body)
    {
        Val<GameObject> c = Val.V(() => character);
        Val<Transform> b = Val.V(() => body);
        return new Sequence(character.GetComponent<BehaviorMecanim>().Node_Kick(c, b), new LeafWait(100));
    }
    protected Node ST_unkick()
    {
        Val<GameObject> c = Val.V(() => policeman);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_unkick(c));
    }
    protected Node ST_unpunch()
    {
        Val<GameObject> c = Val.V(() => policeman);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_unpunch(c));
    }

    protected Node ST_destroyGuy(GameObject character)
    {
        Val<GameObject> c= Val.V(() => character);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_destroy(character));
    }

    public Transform kingFrontPosition;
    public Transform robberFrontPositionKick;
    public Transform robberFrontPositionPunch;

    //----------------------------------------------------------------------------------------------------------------------

    public string currentArc;
    public string changedArc;
    public string currentStory;
    public bool stealMarker;
    //new behaviors:

    //if s was pressed make it so that the stealMarker=true
    //public Node

    public bool isStillCurrentArcWalkAround()
    {
        
        if ("walkAround" == currentArc || "stealFromClose" == currentArc)
        {
            
            return true;
           
        }
        else
        {
            
            return false;
        }
    }

    public bool isStillCurrentArcSteal()
    {

        if ("stealFromClose" == currentArc)
        {
           
            return true;

        }
        else
        {
            
            return false;
        }
    }

    public bool hasWon()
    {

        if ("winGame" == currentArc)
        {
            return true;

        }
        else
        {
            return false;
        }
    }
    public bool hasLost()
    {

        if ("loseGame" == currentArc)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    public bool alwaysReturnTrue()
    {
        return true;
    }

    public bool hasMaximumMoney()
    {
        if (Convert.ToInt32(text.text)>20)//20)
        {
            currentArc = "walkAround";
            return true;
        }else
        {
            return false;
        }
    }

    public Node changeCamera(GameObject character)
    {
        Val<GameObject> c = Val.V(() => character);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().changeCam(c));
    }

    public Node runAwayAsEveryoneStares()
    {
        //at the same time everyone looks at you as you run away to one of your random hiding spots

        //the policeman catches up and you are screwed

        //the policeman kills you and everyone goes about their regular day except for you :)


        Val<GameObject> r = Val.V(() => robber);
        Val<GameObject> p = Val.V(() => policeman);
        Val<GameObject> p2 = Val.V(() => policeman2);
        Val<GameObject> dir = Val.V(() => this.gameObject);
        Val<float> factorSpeed = Val.V(() => 9f);
        Val<Vector3> chasePosition = Val.V(() => robber.transform.position);
        Val<float> rad = Val.V(() => 3f);
        Val<float> Peoplerad = Val.V(() => 10f);
        return new Sequence(
            this.changeCamera(robber),
            new SequenceParallel(
                //robber runs away and raises hand
                //chase whome, chase behind point, who is chasing(2 people), and by how much is the guy chasing faster
                robber.GetComponent<BehaviorMecanim>().chase(r, chasePosition, rad, p, p2, factorSpeed,dir),
                //everyone else moves closer to you to see you get executed
                
                person1.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                person2.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                person3.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                person4.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                person5.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                person6.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                person7.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                person8.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                person9.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                person10.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                sales1.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                sales2.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                sales3.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                sales4.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                sales5.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad),
                sales6.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, Peoplerad)


            //when king stops thinking, return success
            //10000//random...d
            ),
            /*
            person1.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            person2.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            person3.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            person4.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            person5.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            person6.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            person7.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            person8.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            person9.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            person10.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            sales1.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            sales2.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            sales3.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            sales4.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            sales5.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),
            sales6.GetComponent<BehaviorMecanim>().ST_TurnToFace(chasePosition),*/
            (new TreeSharpPlus.RandomNode(
                (new Sequence(
                    new DecoratorInvert(
                        (new DecoratorLoop(
                            new DecoratorInvert(
                                new Sequence(
                                    (this.ST_Approach(robberFrontPositionKick.transform, policeman))
                                )
                            )
                        ))
                    ),
                    this.ST_kick(policeman, robberChest),
                    this.ST_unkick(),
                    ST_destroyGuy(robber))),//this.ST_unkick()

                (new Sequence(
                    new DecoratorInvert(
                        (new DecoratorLoop(
                            new DecoratorInvert(
                                new Sequence(
                                    (this.ST_Approach(robberFrontPositionPunch.transform, policeman))
                                )
                            )
                        ))
                    ),
                    this.ST_punch(policeman, robberFace),
                    this.ST_unpunch(),
                    ST_destroyGuy(robber))))),//this.ST_unpunch()
                                                //-----------------------------------------------------------------------------------------------
                                                //and then stand there proudly
                                                //this.ST_beginConversation(king, robber),
            (new DecoratorLoop(ST_playFaceAnimation("HEADNOD", true, policeman)))

       //then policeman kills him


       );
    }

    public Node walkAround()
    {
        Val<GameObject> r = Val.V(() => robber);
        Val<GameObject> dir = Val.V(() => this.gameObject);
        Val<Text> t = Val.V(() => text);
        Val<string> regArc = Val.V(() => "walkAround");
        Val<string> newArc = Val.V(() => "winGame");
        Val<string> newCaughtArc = Val.V(() => "loseGame");
        Val<float> d = Val.V(() => 5f);
        Val<float> dForward = Val.V(() => 50f);
        Val<GameObject> p1 = Val.V(() => policeman);
        bool trueValue=true;
        Val<object> isActive = Val.V(() => (object)trueValue);
        // Val<Transform> w1 = Val.V(() => wander1);
        // Val<Transform> w2 = Val.V(() => wander2);
        // Val<Transform> w3 = Val.V(() => wander3);
        // Val<Transform> w4 = Val.V(() => wander4);
        // Val<Transform> w5 = Val.V(() => wander5);
        // Val<Transform> w6 = Val.V(() => wander6);
        return (new SequenceParallel(
             //policeman
             (new DecoratorLoop(new SequenceShuffle(

                 (this.ST_Approach(policemanWaypoints[0].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[1].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[2].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[3].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[4].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[5].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[6].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[7].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[8].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[9].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[10].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[11].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[12].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[13].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[14].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[15].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[16].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[17].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[18].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[19].transform, policeman)),
                 (this.ST_Approach(policemanWaypoints[20].transform, policeman))
             ))),
             (new DecoratorLoop(new SequenceShuffle(

                 (this.ST_Approach(policemanWaypoints[0].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[1].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[2].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[3].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[4].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[5].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[6].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[7].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[8].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[9].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[10].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[11].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[12].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[13].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[14].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[15].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[16].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[17].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[18].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[19].transform, policeman2)),
                 (this.ST_Approach(policemanWaypoints[20].transform, policeman2))
             ))),
            (new DecoratorLoop(new SequenceShuffle(
                (this.ST_ApproachAndWait(wander1, person1)),
                (this.ST_ApproachAndWait(wander2, person1)),
                (this.ST_ApproachAndWait(wander3, person1)),
                (this.ST_ApproachAndWait(wander4, person1)),
                (this.ST_ApproachAndWait(wander5, person1)),
                (this.ST_ApproachAndWait(wander6, person1))
            ))),
            (new DecoratorLoop(new SequenceShuffle(
                (this.ST_ApproachAndWait(wander1, person2)),
                (this.ST_ApproachAndWait(wander2, person2)),
                (this.ST_ApproachAndWait(wander3, person2)),
                (this.ST_ApproachAndWait(wander4, person2)),
                (this.ST_ApproachAndWait(wander5, person2)),
                (this.ST_ApproachAndWait(wander6, person2))
            ))),
            (new DecoratorLoop(new SequenceShuffle(
                (this.ST_ApproachAndWait(wander1, person3)),
                (this.ST_ApproachAndWait(wander2, person3)),
                (this.ST_ApproachAndWait(wander3, person3)),
                (this.ST_ApproachAndWait(wander4, person3)),
                (this.ST_ApproachAndWait(wander5, person3)),
                (this.ST_ApproachAndWait(wander6, person3))
            ))),
            (new DecoratorLoop(new SequenceShuffle(
                (this.ST_ApproachAndWait(wander1, person4)),
                (this.ST_ApproachAndWait(wander2, person4)),
                (this.ST_ApproachAndWait(wander3, person4)),
                (this.ST_ApproachAndWait(wander4, person4)),
                (this.ST_ApproachAndWait(wander5, person4)),
                (this.ST_ApproachAndWait(wander6, person4))
            ))),
            (new DecoratorLoop(new SequenceShuffle(
                (this.ST_ApproachAndWait(wander1, person5)),
                (this.ST_ApproachAndWait(wander2, person5)),
                (this.ST_ApproachAndWait(wander3, person5)),
                (this.ST_ApproachAndWait(wander4, person5)),
                (this.ST_ApproachAndWait(wander5, person5)),
                (this.ST_ApproachAndWait(wander6, person5))
            ))),
            (new DecoratorLoop(new SequenceShuffle(
                (this.ST_ApproachAndWait(wander1, person6)),
                (this.ST_ApproachAndWait(wander2, person6)),
                (this.ST_ApproachAndWait(wander3, person6)),
                (this.ST_ApproachAndWait(wander4, person6)),
                (this.ST_ApproachAndWait(wander5, person6)),
                (this.ST_ApproachAndWait(wander6, person6))
            ))),
            (new DecoratorLoop(new SequenceShuffle(
                (this.ST_ApproachAndWait(wander1, person7)),
                (this.ST_ApproachAndWait(wander2, person7)),
                (this.ST_ApproachAndWait(wander3, person7)),
                (this.ST_ApproachAndWait(wander4, person7)),
                (this.ST_ApproachAndWait(wander5, person7)),
                (this.ST_ApproachAndWait(wander6, person7))
            ))),
            (new DecoratorLoop(new SequenceShuffle(
                (this.ST_ApproachAndWait(wander1, person8)),
                (this.ST_ApproachAndWait(wander2, person8)),
                (this.ST_ApproachAndWait(wander3, person8)),
                (this.ST_ApproachAndWait(wander4, person8)),
                (this.ST_ApproachAndWait(wander5, person8)),
                (this.ST_ApproachAndWait(wander6, person8))
            ))),
            (new DecoratorLoop(new SequenceShuffle(
                (this.ST_ApproachAndWait(wander1, person9)),
                (this.ST_ApproachAndWait(wander2, person9)),
                (this.ST_ApproachAndWait(wander3, person9)),
                (this.ST_ApproachAndWait(wander4, person9)),
                (this.ST_ApproachAndWait(wander5, person9)),
                (this.ST_ApproachAndWait(wander6, person9))
            ))),
            (new DecoratorLoop(new SequenceShuffle(
                (this.ST_ApproachAndWait(wander1, person10)),
                (this.ST_ApproachAndWait(wander2, person10)),
                (this.ST_ApproachAndWait(wander3, person10)),
                (this.ST_ApproachAndWait(wander4, person10)),
                (this.ST_ApproachAndWait(wander5, person10)),
                (this.ST_ApproachAndWait(wander6, person10))
            ))),
            //agent can steal
            new DecoratorLoop(
                new Selector(
                    new Sequence(
                        new LeafAssert(isStillCurrentArcSteal),
                        new DecoratorInvert(new LeafAssert(hasMaximumMoney)),
                        //(person1.GetComponent<BehaviorMecanim>().Node_BeginConversation(robber, person3, true)),
                        (robber.GetComponent<BehaviorMecanim>().ST_stealFromPlayerFront(r, dir, t)),
                        robber.GetComponent<BehaviorMecanim>().changeArcName(regArc, dir)
                        //now check if policeman sees
                        ,
                        new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_Sees(p1, r, isActive, d, dForward)
                        , 
                        policeman.GetComponent<BehaviorMecanim>().changeArcName(newCaughtArc,dir)
                    )


                    ),
                    new LeafAssert(alwaysReturnTrue)
                )
            ),
            //agent talks to vendor and buys something
            new DecoratorLoop(
                new Selector(
                    new Sequence(
                        new LeafAssert(hasMaximumMoney),
                        //(person1.GetComponent<BehaviorMecanim>().Node_BeginConversation(robber, person3, true)),
                        (new SelectorShuffle(
                            (new Sequence(
                                (this.ST_ApproachAndWait(wander1, robber)),
                                (robber.GetComponent<BehaviorMecanim>().ST_buyFromVender(robber, sales1,dir, newArc))
                            )),
                            (new Sequence(
                                (this.ST_ApproachAndWait(wander2, robber)),
                                (robber.GetComponent<BehaviorMecanim>().ST_buyFromVender(robber, sales2, dir, newArc))
                            )),
                            (new Sequence(
                                (this.ST_ApproachAndWait(wander3, robber)),
                                (robber.GetComponent<BehaviorMecanim>().ST_buyFromVender(robber, sales3, dir, newArc))
                            )),
                             (new Sequence(
                                (this.ST_ApproachAndWait(wander4, robber)),
                                (robber.GetComponent<BehaviorMecanim>().ST_buyFromVender(robber, sales4, dir, newArc))
                            )),
                              (new Sequence(
                                (this.ST_ApproachAndWait(wander5, robber)),
                                (robber.GetComponent<BehaviorMecanim>().ST_buyFromVender(robber, sales5, dir, newArc))
                            )),
                              (new Sequence(
                                (this.ST_Approach(wander6, robber)),
                                (robber.GetComponent<BehaviorMecanim>().ST_buyFromVender(robber, sales6, dir, newArc))
                            ))


                        ))
                        
                    ),
                    new DecoratorInvert(new LeafAssert(hasMaximumMoney))
                )
            )

        ));
    }

    public Node selectStory(string storyName, GameObject agent1, GameObject agent2, GameObject agent3, GameObject agent4)
    {

        Val<GameObject> p1 = Val.V(() => person1);
        Val<GameObject> p2 = Val.V(() => person2);
        Val<GameObject> p3 = Val.V(() => person3);
        Val<GameObject> p4 = Val.V(() => person4);
        Val<Text> t = Val.V(() => text);
        Val<Text> dT = Val.V(() => conversationText);

        currentStory = storyName;
        if (storyName == "walkAround")
        {
         
            return new SelectorParallel(
                 (
                    (new DecoratorInvert(new DecoratorLoop (new LeafAssert(isStillCurrentArcWalkAround))))
                ),
                (new Sequence(
                    walkAround()
                ))
            );
        }
        else if (storyName == "winGame")
        {
            return new SelectorParallel(
                     (new DecoratorInvert(new DecoratorLoop(new LeafAssert(hasWon))))
               ,
                (new Sequence(
                    //first lose 20 money

                    //display message that you haveWonGame
                    agent2.GetComponent<BehaviorMecanim>().winGameMessages(t, dT),
                    ST_destroyGuy(person1),
                    ST_destroyGuy(person2),
                    ST_destroyGuy(person3),
                    ST_destroyGuy(person4),
                    ST_destroyGuy(person5),
                    ST_destroyGuy(person6),
                    ST_destroyGuy(person7),
                    ST_destroyGuy(person8),
                    ST_destroyGuy(person9),
                    ST_destroyGuy(person10),
                    ST_destroyGuy(sales1),
                    ST_destroyGuy(sales2),
                    ST_destroyGuy(sales3),
                    ST_destroyGuy(sales4),
                    ST_destroyGuy(sales5),
                    ST_destroyGuy(sales6),
                     ST_destroyGuy(policeman),
                    ST_destroyGuy(policeman2),

                    new DecoratorLoop(new LeafWait(1))
                    
                    
                ))
            );
        }
        else if (storyName == "loseGame")
        {

            return new SelectorParallel(

                    (new DecoratorInvert(new DecoratorLoop(new LeafAssert(hasLost))))
                ,
                (new Sequence(
                    runAwayAsEveryoneStares(),
              

                    new DecoratorLoop(new LeafWait(1))


                ))
            );
        }
        return new Sequence(agent2.GetComponent<BehaviorMecanim>().Node_BeginConversation(p1, p2, true));
    }


    
    protected Node BuildTreeRoot()
    {
        currentArc = "walkAround";
        Node roaming =
        new SelectorParallel(
        #region Story
           (new DecoratorLoop  
                (new Sequence(
                    //select story 1: everyone just walking around, cops looking for anything suspicious...
                    selectStory("walkAround", robber, policeman, person1, person2),
                    selectStory("winGame", robber, policeman, person1, person2),
                    selectStory("loseGame", robber, policeman, person1, person2)
                //robber caught by police, goes to get executed
                //ST_beginConversation(robber, policeman)
                //robber robbing a person

                //
                )
               
            )
           
           
           )
           
           
            //,
           //walkAround()
           /*,
           #endregion
           #region MonitorUI
           (new DecoratorLoop
                (new Sequence(
                    //check whether steal s was pressed and if it is make story stealFromClose
                    checkStealPressed()
                )

            )),*/
        #endregion
        #region Monitor Story State
       /*(new DecoratorLoop
            (new Selector
                ()


            )



       )*/
        #endregion
       );

        return roaming;
    }
}

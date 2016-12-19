using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;
using UnityEngine.UI;

public class BehaviorCoordinatorb5 : MonoBehaviour
{

    //useful scene objects for b5
    public GameObject villager1;
    public GameObject villager2;
    public GameObject villager3;
    public GameObject villager4;
    public GameObject villager5;

    public Transform[] villagerWanderPoints;
    public Transform workHay;
    public Transform chillInHouse1;
    public Transform chillInHouse2;
    public Transform talkToVendor1;
    public Transform talkToVendor2; 

    //contains elder and blacksmith
    public GameObject[] questCharacters;

    //useful variables for b5
    public bool quest1Done = false;
    public bool quest2Done = false;
    public bool talking;

    //0: no quest. 1: quest 1, 2: quest 2
    public int currentQuest = 0;


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
        currentArc = "intro";

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
        conversationPart4 = (object)(0);
        conversationNumber4 = (object)(3);

        conversationNumber5 = (object)(4);
        conversationNumber6 = (object)(5);
        conversationNumber7 = (object)(6);
        conversationNumber8 = (object)(7);
        conversationNumber9 = (object)(8);
        conversationNumber10 = (object)(9);
        conversationNumber11 = (object)(10);
        conversationNumber12 = (object)(11);
        conversationNumber13 = (object)(12);
        conversationNumber14 = (object)(13);
        conversationNumber15 = (object)(14);
        conversationNumber16 = (object)(15);
        conversationNumberDefault = (object)(-1);

        //make locomotion to robber
        Animator animator = robber.GetComponent<Animator>();
        locomotion = new LocomotionController(animator);


        //b5 initiialization
        quest1Done = false;
       quest2Done = false;
        talking = false;

}



    //if press Enter, update conversation
    public object conversationPart;
    public object conversationNumber;
    public object conversationPart2;
    public object conversationNumber2;
    public object conversationPart3;
    public object conversationNumber3;
    public object conversationPart4;
    public object conversationNumber4;
    public object conversationNumber5;
    public object conversationNumber6;
    public object conversationNumber7;
    public object conversationNumber8;
    public object conversationNumber9;
    public object conversationNumber10;
    public object conversationNumber11;
    public object conversationNumber12;
    public object conversationNumber13;
    public object conversationNumber14;
    public object conversationNumber15;
    public object conversationNumber16;
    public object conversationNumberDefault;
    public object isActive1;
    public object isActive2;



    bool yesWasPressed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            makeLetter = "A";
        }else if (Input.GetKey(KeyCode.B))
        {
            makeLetter = "B";
        }
        else if (Input.GetKey(KeyCode.C))
        {
            makeLetter = "C";
        }
        else if (Input.GetKey(KeyCode.D))
        {
            makeLetter = "D";
        }
        else if (Input.GetKey(KeyCode.E))
        {
            makeLetter = "E";
        }
        else if (Input.GetKey(KeyCode.F))
        {
            makeLetter = "F";
        }
        else if (Input.GetKey(KeyCode.G))
        {
            makeLetter = "G";
        }
        else if (Input.GetKey(KeyCode.H))
        {
            makeLetter = "H";
        }
        else if (Input.GetKey(KeyCode.I))
        {
            makeLetter = "I";
        }
        else if (Input.GetKey(KeyCode.J))
        {
            makeLetter = "J";
        }
        else if (Input.GetKey(KeyCode.K))
        {
            makeLetter = "K";
        }
        else if (Input.GetKey(KeyCode.L))
        {
            makeLetter = "L";
        }
        else if (Input.GetKey(KeyCode.M))
        {
            makeLetter = "M";
        }
        else if (Input.GetKey(KeyCode.N))
        {
            makeLetter = "N";
        }
        else if (Input.GetKey(KeyCode.O))
        {
            makeLetter = "O";
        }
        else if (Input.GetKey(KeyCode.P))
        {
            makeLetter = "P";
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            makeLetter = "Q";
        }
        else if (Input.GetKey(KeyCode.R))
        {
            makeLetter = "R";
        }
        else if (Input.GetKey(KeyCode.S))
        {
            makeLetter = "S";
        }
        else if (Input.GetKey(KeyCode.T))
        {
            makeLetter = "T";
        }
        else if (Input.GetKey(KeyCode.U))
        {
            makeLetter = "U";
        }
        else if (Input.GetKey(KeyCode.V))
        {
            makeLetter = "V";
        }
        else if (Input.GetKey(KeyCode.W))
        {
            makeLetter = "W";
        }
        else if (Input.GetKey(KeyCode.X))
        {
            makeLetter = "X";
        }
        else if (Input.GetKey(KeyCode.Y))
        {
            makeLetter = "Y";
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            makeLetter = "Z";
        }
        if (movementOn)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("old current arc: " + currentArc);
                //if (currentArc == "storyPart1")
                //{
                    talking = true;
                //}


            }

            float speed = 0;

            //running
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    speed = 100;
                    locomotion.Do(100, 0);

                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    speed = -100;
                    locomotion.Do(-100, 0);

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
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }
    protected Node ST_processConversation2(object convNum, object convPart)
    {
        Val<object> convP = Val.V(() => (object)conversationPart2);
        Val<object> convN = Val.V(() => (object)conversationNumber2);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }
    protected Node ST_processConversation3(object convNum, object convPart)
    {
        Val<object> convP = Val.V(() => (object)conversationPart3);
        Val<object> convN = Val.V(() => (object)conversationNumber3);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }

    protected Node ST_processConversation4()
    {
        Val<object> convP = Val.V(() => (object)conversationPart4);
        Val<object> convN = Val.V(() => (object)conversationNumber4);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }

    protected Node ST_processConversation5()
    {
        Val<object> convP = Val.V(() => (object)conversationPart4);
        Val<object> convN = Val.V(() => (object)conversationNumber5);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }

    protected Node ST_processConversation6()
    {
        Val<object> convP = Val.V(() => (object)conversationPart4);
        Val<object> convN = Val.V(() => (object)conversationNumber6);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }

    protected Node ST_processConversation7()
    {
        Val<object> convP = Val.V(() => (object)conversationPart4);
        Val<object> convN = Val.V(() => (object)conversationNumber7);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }

    protected Node ST_processConversation8()
    {
        Val<object> convP = Val.V(() => (object)conversationPart4);
        Val<object> convN = Val.V(() => (object)conversationNumber8);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }

    protected Node ST_processConversation9()
    {
        Val<object> convP = Val.V(() => (object)conversationPart4);
        Val<object> convN = Val.V(() => (object)conversationNumber9);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }

    protected Node ST_processConversation10()
    {
        Val<object> convP = Val.V(() => (object)conversationPart4);
        Val<object> convN = Val.V(() => (object)conversationNumber10);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }

    protected Node ST_processConversation11()
    {
        Val<object> convP = Val.V(() => (object)conversationPart4);
        Val<object> convN = Val.V(() => (object)conversationNumber11);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }

    protected Node ST_processConversation12()
    {
        Val<object> convP = Val.V(() => (object)conversationPart4);
        Val<object> convN = Val.V(() => (object)conversationNumber12);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }
    protected Node ST_processConversation13()
    {
        Val<object> convP = Val.V(() => (object)conversationPart4);
        Val<object> convN = Val.V(() => (object)conversationNumber13);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }
    protected Node ST_processConversation14()
    {
        Val<object> convP = Val.V(() => (object)conversationPart4);
        Val<object> convN = Val.V(() => (object)conversationNumber14);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }
    protected Node ST_processConversation15()
    {
        Val<object> convP = Val.V(() => (object)conversationPart4);
        Val<object> convN = Val.V(() => (object)conversationNumber15);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }
    protected Node ST_processConversation16()
    {
        Val<object> convP = Val.V(() => (object)conversationPart4);
        Val<object> convN = Val.V(() => (object)conversationNumber16);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }

    protected Node ST_processConversationDefault()
    {
        Val<object> convP = Val.V(() => (object)conversationPart4);
        Val<object> convN = Val.V(() => (object)conversationNumberDefault);
        Val<Text> convText = Val.V(() => conversationText);
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_ConversationInProcess(true, convP, convN, convText), new LeafWait(10));
    }

    protected Node ST_endConversation()
    {
        conversationText.text = "";
        return new Sequence(robber.GetComponent<BehaviorMecanim>().Node_EndConversation(true));
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
        return new Sequence(character.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(UnityEngine.Random.Range(900, 4000)));
    }

    protected Node ST_ApproachAndWaitCertainTime(Transform target, GameObject character, int timeMin, int timeMax)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(character.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(UnityEngine.Random.Range(timeMin, timeMax)));
    }

    protected Node ST_IdleKing(string gestureName, long duration, GameObject character)
    {
        Val<string> ges = Val.V(() => gestureName);
        Val<long> dur = Val.V(() => duration);
        return new Sequence(character.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(ges, dur));
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
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_Sees(ges, dur, isActive, d, d));
    }
    protected Node ST_Sees2(GameObject king, GameObject robber)
    {
        bool b = false;
        isActive2 = (object)b;
        Val<GameObject> ges = Val.V(() => king);
        Val<GameObject> dur = Val.V(() => robber);
        Val<object> isActive = Val.V(() => isActive2);
        Val<float> d = Val.V(() => 50f);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_Sees(ges, dur, isActive, d, d));
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
        Val<GameObject> c = Val.V(() => character);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_destroy(character));
    }

    public Transform kingFrontPosition;
    public Transform robberFrontPositionKick;
    public Transform robberFrontPositionPunch;


    public Transform frontCastle;

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

    public bool isStillCurrentArcIntro()
    {

        if ("intro" == currentArc)
        {

            return true;

        }
        else
        {

            return false;
        }
    }

    public bool isStillCurrentArcPart1()
    {
        if ("storyPart1" == currentArc)
        {

            return true;

        }
        else
        {
            return false;
        }
    }

    public bool isStillCurrentArcPart2()
    {
        if (quest1Done==true&&quest2Done==true)
        {
            currentArc = "storyPart2";
            return true;

        }
        else
        {
            return false;
        }
    }

    public bool isStillCurrentArcQuest1()
    {

        if ("quest1" == currentArc)
        {

            return true;

        }
        else
        {

            return false;
        }
    }
    public bool isStillCurrentArcQuest2()
    {
        if ("quest2" == currentArc)
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
        if (Convert.ToInt32(text.text) > 20)//20)
        {
            currentArc = "walkAround";
            return true;
        }
        else
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
                robber.GetComponent<BehaviorMecanim>().chase(r, chasePosition, rad, p, p2, factorSpeed, dir),
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

 //helper methods
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
        Val<GameObject> p = Val.V(() => policeman2);
        Val<GameObject> p1 = Val.V(() => policeman);
        bool trueValue = true;
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
            )))//,
            //agent can steal
            /*(new DecoratorLoop
                (new Selector(
                    (new Sequence(
                        new LeafAssert(isStillCurrentArcSteal),
                        new DecoratorInvert(new LeafAssert(hasMaximumMoney)),
                        //(person1.GetComponent<BehaviorMecanim>().Node_BeginConversation(robber, person3, true)),
                        (robber.GetComponent<BehaviorMecanim>().ST_stealFromPlayerFront(r, dir, t)),
                        robber.GetComponent<BehaviorMecanim>().changeArcName(regArc, dir)
                        //now check if policeman sees
                        ,
                        new Selector(policeman.GetComponent<BehaviorMecanim>().Node_Sees(p1, r, isActive, d, dForward))
                        ,
                        policeman.GetComponent<BehaviorMecanim>().changeArcName(newCaughtArc, dir)
                   )),
                    new LeafAssert(alwaysReturnTrue)
                ))
            ),*/
            //agent talks to vendor and buys something
            /*new DecoratorLoop(
                new Selector(
                    new Sequence(
                        new LeafAssert(hasMaximumMoney),
                        //(person1.GetComponent<BehaviorMecanim>().Node_BeginConversation(robber, person3, true)),
                        (new SelectorShuffle(
                            (new Sequence(
                                (this.ST_ApproachAndWait(wander1, robber)),
                                (robber.GetComponent<BehaviorMecanim>().ST_buyFromVender(robber, sales1, dir, newArc))
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
            */
        ));
    }

    public Node talkToVendor(GameObject character1, GameObject vendor, Transform talkToVendor1) 
    {
        Val<GameObject> dir = Val.V(() => this.gameObject);
        return new Sequence(
            (this.ST_ApproachAndWaitCertainTime(talkToVendor1, character1, 900, 2000)),
            (character1.GetComponent<BehaviorMecanim>().ST_buyFromVenderNoArc(character1, vendor, dir))


        );
    }

    public Node talkToPerson(GameObject character1, GameObject character2, Transform talkToPersonLoc)
    {
        Val<GameObject> dir = Val.V(() => this.gameObject);
        return new Sequence(
            (this.ST_ApproachAndWaitCertainTime(talkToVendor1, character1, 900, 2000)),
            (character1.GetComponent<BehaviorMecanim>().ST_buyFromVenderNoArc(character1, character2, dir))


        );
    }

    public bool isTalking()
    {

        if (talking==true)
        {

            return true;

        }
        else
        {

            return false;
        }
    }

    public bool wasKeyPressed()
    {

        if (makeLetter != "")
        {

            return true;

        }
        else
        {

            return false;
        }
    }


    public bool isThisNotNumber20()
    {
        //Debug.Log("numKeys: " + numKeys);
        //Debug.Log("keyRight: " + keyRight);
        if (numKeys == 0)
        {
            percentCorrect.text = "correct: 0%";
        }
        else
        {
            percentCorrect.text = "correct: " + (int)(100f * keyRight / numKeys) + "%";
        }

        if (numKeys<20|| numKeys==0||(1.0f*keyRight/numKeys<.80))
        {

            return true;

        }
        else
        {
            percentCorrect.text = "";
            return false;
        }
    }


    //code for one villager walking around village
    public Node oneVillagerChill(GameObject character)
    {
        return (new DecoratorLoop(new SequenceShuffle(

                 (this.ST_ApproachAndWaitCertainTime(villagerWanderPoints[0], character, 400, 1000)),
                 (this.ST_ApproachAndWaitCertainTime(villagerWanderPoints[1], character, 400, 1000)),
                 (this.ST_ApproachAndWaitCertainTime(villagerWanderPoints[2], character, 400, 1000)),
                 (this.ST_ApproachAndWaitCertainTime(villagerWanderPoints[3], character, 400, 1000)),
                 (this.ST_ApproachAndWaitCertainTime(villagerWanderPoints[4], character, 400, 1000)),
                 (this.ST_ApproachAndWaitCertainTime(villagerWanderPoints[5], character, 400, 1000)),
                 (this.ST_ApproachAndWaitCertainTime(villagerWanderPoints[6], character, 400, 1000)),
                 (this.ST_ApproachAndWaitCertainTime(workHay, character, 900, 2000)),
                 (this.ST_ApproachAndWaitCertainTime(chillInHouse1, character, 900, 2000)),
                 (this.ST_ApproachAndWaitCertainTime(chillInHouse2, character, 900, 2000)),
                 (this.ST_ApproachAndWaitCertainTime(talkToVendor1, character, 900, 2000)),
                 (this.ST_ApproachAndWaitCertainTime(talkToVendor2, character, 900, 2000))
                 //,
                //(this.talkToVendor(character, questCharacters[0], talkToVendor1)),
                //(this.talkToVendor(character, questCharacters[1], talkToVendor2))
             )));
    }

    //code for a lt of villagers walking around village
    public Node villagersWalkAround()
    {
        return (new SequenceParallel(
             //villager1 
             oneVillagerChill(villager1),
            oneVillagerChill(villager2),
             oneVillagerChill(villager3),
              oneVillagerChill(villager4),
               oneVillagerChill(villager5)
        ));
    }


    //story arcs
    public Node introSequence()
    {
        Val<GameObject> dir = Val.V(() => this.gameObject);
        Val<string> regArc = Val.V(() => "storyPart1");

        return (
            new Sequence(   
                //temporary erase for testing
                
                (new SequenceParallel(
                    new Sequence(
                        this.ST_Approach(wander4, robber),
                        new LeafWait(1000),
                        this.ST_Approach(wander3, robber)
                     ),
                    
                    new Sequence(
                        this.ST_processConversation4(),
                        new LeafWait(9000),
                        this.ST_processConversation5()
                     )
                )), 
                this.ST_beginConversation(robber, sales3),
                 this.ST_processConversation6(),
                 new LeafWait(7000),
                 this.ST_processConversation7(),
                 new LeafWait(7000),
                this.ST_endConversation(),
                this.ST_processConversation8(),
                ST_ApproachAndWait(frontCastle, robber),
                 new LeafWait(10),
                this.ST_processConversationDefault(),
                robber.GetComponent<BehaviorMecanim>().changeArcName(regArc, dir)
            )
        );

    }

    public Node part1()
    {
        Val<GameObject> hero = Val.V(() => robber);
        Val<GameObject> dir = Val.V(() => this.gameObject);
        Val<GameObject[]> questchars = Val.V(() => questCharacters);
        Val<Text> t = Val.V(() => text);
        return (
            new SelectorParallel(
                villagersWalkAround(),
                (new DecoratorLoop(
                       (new Selector(
                           //write logic for talking to quest person
                           (new Sequence(
                                new LeafAssert(isTalking),
                                robber.GetComponent<BehaviorMecanim>().ST_talkToNpc(hero, dir, questchars)
                                //robber.GetComponent<BehaviorMecanim>().changeArcName(newArc, dir)
                                //now check if policeman sees
                                //,
                                //new Selector(policeman.GetComponent<BehaviorMecanim>().Node_Sees(p1, r, isActive, d, dForward))
                                //,
                                //policeman.GetComponent<BehaviorMecanim>().changeArcName(newCaughtArc, dir)
                           )),
                           new LeafAssert(alwaysReturnTrue)
                        )) 
                ))
            ) 
        );
    }

    public Node part2()
    {
        Val<GameObject> hero = Val.V(() => robber);
        Val<GameObject> dir = Val.V(() => this.gameObject);
        Val<GameObject[]> questchars = Val.V(() => questCharacters);
        Val<Text> t = Val.V(() => text);
        return
            new Sequence(
                this.ST_processConversation16(),
                (new DecoratorLoop(
                    new SequenceParallel(
                        villagersWalkAround(),
                        walkAround()
                    )
                )
            )
        );
    }


    public int keyRight;
    public int numKeys;
    public string assignLetter;
    public string makeLetter;
    public Text assignedLetter;
    public Text madeLetter;
    public Text percentCorrect;


    public GameObject quest1Visibles;
    public GameObject quest2Visibles;

    public Node dance()
    {
        Val<string> type1 = Val.V(() => "BREAKDANCE");

        return robber.GetComponent<BehaviorMecanim>().Node_BodyAnimation(type1, true);
    }



    public Node quest1()
    {

        keyRight = 0;
        string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        Val<GameObject> dir = Val.V(() => this.gameObject);
        Val<string[]> let = Val.V(() => letters);
        Val<Text> assignedl = Val.V(() => assignedLetter);
        Val<Text> madel = Val.V(() => madeLetter);
        Val<string> newArc = Val.V(() => "storyPart1");

        Val<Vector3> chasePosition = Val.V(() => robber.transform.position);
        //Val<Transform> heroTransform = Val.V(() => robber.transform);
        Val<float> rad = Val.V(() => 5f);

        //people gather?
        return new Sequence(




            //then go to middle of village as everyone else gathers around you
            new SelectorParallel(
                new Sequence(
                     //first talk to the guy....
                     this.ST_beginConversation(robber, questCharacters[0]),
                    this.ST_processConversation13(),
                    new LeafWait(7000),
                    this.ST_processConversation14(),
                    new LeafWait(7000),
                    this.ST_processConversation11(),
                    this.ST_endConversation(),
                    (this.ST_ApproachAndWaitCertainTime(villageCenter, robber, 0, 1)),
                    this.ST_processConversationDefault()
                ),
                villagersWalkAround()
            ),
            new SequenceParallel(
                //all villagers gather around
                new Sequence(villager1.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, rad), ST_IdleKing("CLAP", 10000, villager1)),
                new Sequence(villager2.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, rad), ST_IdleKing("CLAP", 10000, villager2)),
                new Sequence(villager3.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, rad), ST_IdleKing("CLAP", 10000, villager3)),
                new Sequence(villager4.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, rad), ST_IdleKing("CLAP", 10000, villager4)),
                new Sequence(villager5.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, rad), ST_IdleKing("CLAP", 10000, villager5)),
                new Sequence(questCharacters[0].GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, rad), ST_IdleKing("CLAP", 10000, questCharacters[0])),



                new Sequence(
                    new LeafWait(1000),

                    //everyone keeps gathering around you as you perform


                    robber.GetComponent<BehaviorMecanim>().initializeLetters(dir, let, assignedl, madel),
                    //loop 20 times
                    new DecoratorInvert(
                        new DecoratorLoop(
                            new Sequence(
                                //1st check that havent done 20
                                new LeafAssert(isThisNotNumber20),

                                //1st pick an update randomletter
                                new Sequence(
                                    robber.GetComponent<BehaviorMecanim>().ST_pickRandomLetter(dir, let, assignedl, madel)//,
                                                                                                                          //new LeafWait(1000)
                                )


                                ,
                                //at the same time, wait and also check if key was pressed
                                new SelectorParallel(
                                    //while waiting
                                    //at the same time do leaf wait and dance
                                    new SequenceParallel(
                                        new LeafWait(2000),
                                        dance()
                                    ),
                                    //check if key was pressed
                                    new DecoratorLoop(
                                        new DecoratorInvert(
                                            new Sequence(
                                               new LeafAssert(wasKeyPressed),
                                               //change the keypressed stuff...
                                               robber.GetComponent<BehaviorMecanim>().updatePickedLetter(dir, let, assignedl, madel)
                                            )
                                        )
                                    )



                                )
                             )
                        )
                    ),
                    //check how many got right. If did not get enough right, restart
                    robber.GetComponent<BehaviorMecanim>().initializeLetters(dir, let, assignedl, madel),
                    robber.GetComponent<BehaviorMecanim>().Node_BodyAnimation("BREAKDANCE", false),
                     //we have to turn on movement, congradulate on completing the quest, etc.
                     robber.GetComponent<BehaviorMecanim>().Node_endQuest(dir, quest1Visibles, quest2Visibles),
                     this.ST_processConversation15(),
                     new SequenceParallel(
                         ST_IdleKing("CLAP", 7000, villager1),
                         ST_IdleKing("CLAP", 7000, villager2),
                         ST_IdleKing("CLAP", 7000, villager3),
                         ST_IdleKing("CLAP", 7000, villager4),
                         ST_IdleKing("CLAP", 7000, villager5),
                         ST_IdleKing("CLAP", 7000, questCharacters[0]),
                         ST_IdleKing("CLAP", 7000, questCharacters[1]),
                         new LeafWait(7000)
                     ),
                     this.ST_processConversationDefault(),
                    robber.GetComponent<BehaviorMecanim>().changeArcName(newArc, dir)
                //in part 1 check if both quests done
                )
            )
        );
    }

    public Transform villageCenter;

    public Node quest2()
    {
        keyRight =0;
        string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        Val<GameObject> dir = Val.V(() => this.gameObject);
        Val<string[]> let = Val.V(() => letters);
        Val<Text> assignedl = Val.V(() => assignedLetter);
        Val<Text> madel = Val.V(() => madeLetter);
        Val<string> newArc = Val.V(() => "storyPart1");

        Val<Vector3> chasePosition = Val.V(() => robber.transform.position);
        //Val<Transform> heroTransform = Val.V(() => robber.transform);
        Val<float> rad = Val.V(() => 5f);

        //what we want to do first is to talk to the guy. 
        //Then after he gives the assigment, we go out into the middle of the village and initiate our dance :)


        //people gather?

        return new Sequence(
            



            //then go to middle of village as everyone else gathers around you
            new SelectorParallel(
                new Sequence(
                     //first talk to the guy....
                     this.ST_beginConversation(robber, questCharacters[1]),
                    this.ST_processConversation9(),
                    new LeafWait(7000),
                    this.ST_processConversation10(),
                    new LeafWait(7000),
                    this.ST_processConversation11(),
                    this.ST_endConversation(),
                    (this.ST_ApproachAndWaitCertainTime(villageCenter, robber, 0, 1)),
                    this.ST_processConversationDefault()
                ),
                villagersWalkAround()
            ),
            new SequenceParallel(
                //all villagers gather around
                new Sequence(villager1.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, rad), ST_IdleKing("CLAP", 10000, villager1)),
                new Sequence(villager2.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, rad), ST_IdleKing("CLAP", 10000, villager2)),
                new Sequence(villager3.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, rad), ST_IdleKing("CLAP", 10000, villager3)),
                new Sequence(villager4.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, rad), ST_IdleKing("CLAP", 10000, villager4)),
                new Sequence(villager5.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, rad), ST_IdleKing("CLAP", 10000, villager5)),
                new Sequence(questCharacters[1].GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chasePosition, rad), ST_IdleKing("CLAP", 10000, questCharacters[1])),

               

                new Sequence(
                    new LeafWait(1000),

                    //everyone keeps gathering around you as you perform


                    robber.GetComponent<BehaviorMecanim>().initializeLetters(dir, let, assignedl, madel),
                    //loop 20 times
                    new DecoratorInvert( 
                        new DecoratorLoop(
                            new Sequence(
                                //1st check that havent done 20
                                new LeafAssert(isThisNotNumber20),

                                //1st pick an update randomletter
                                new Sequence(
                                    robber.GetComponent<BehaviorMecanim>().ST_pickRandomLetter(dir, let, assignedl, madel)//,
                                    //new LeafWait(1000)
                                )
            
            
                                ,
                                //at the same time, wait and also check if key was pressed
                                new SelectorParallel(
                                    //while waiting
                                    //at the same time do leaf wait and dance
                                    new SequenceParallel(
                                        new LeafWait(2000),
                                        dance()
                                    ),
                                    //check if key was pressed
                                    new DecoratorLoop(
                                        new DecoratorInvert(
                                            new Sequence(
                                               new LeafAssert(wasKeyPressed),
                                               //change the keypressed stuff...
                                               robber.GetComponent<BehaviorMecanim>().updatePickedLetter(dir, let, assignedl, madel)
                                            )
                                        )
                                    )
                
                
                
                                )
                             )
                        )
                    ),
                    //check how many got right. If did not get enough right, restart
                    robber.GetComponent<BehaviorMecanim>().initializeLetters(dir, let, assignedl, madel),
                    robber.GetComponent<BehaviorMecanim>().Node_BodyAnimation("BREAKDANCE",false),
                     //we have to turn on movement, congradulate on completing the quest, etc.
                     robber.GetComponent<BehaviorMecanim>().Node_endQuest(dir,quest1Visibles, quest2Visibles),
                     this.ST_processConversation12(),
                     new SequenceParallel(
                         ST_IdleKing("CLAP", 7000, villager1),
                         ST_IdleKing("CLAP", 7000, villager2),
                         ST_IdleKing("CLAP", 7000, villager3),
                         ST_IdleKing("CLAP", 7000, villager4),
                         ST_IdleKing("CLAP", 7000, villager5),
                         ST_IdleKing("CLAP", 7000, questCharacters[0]),
                         ST_IdleKing("CLAP", 7000, questCharacters[1]),
                         new LeafWait(7000)
                     ),
                     this.ST_processConversationDefault(),
                    robber.GetComponent<BehaviorMecanim>().changeArcName(newArc, dir)
                    //in part 1 check if both quests done
                )
            )
        );
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
        if (storyName == "intro")
        {

            return new SelectorParallel(
                 (
                    (new DecoratorInvert(new DecoratorLoop(new LeafAssert(isStillCurrentArcIntro))))
                ),
                introSequence()

            );
        }else if (storyName == "storyPart1")
        {
            return new SelectorParallel(
                 (
                    (new DecoratorInvert(new DecoratorLoop(new LeafAssert(isStillCurrentArcPart1))))
                ),
                part1()
                
            );
        }
        else if (storyName == "quest1")
        {
            return new SelectorParallel(
                 (
                    (new DecoratorInvert(new DecoratorLoop(new LeafAssert(isStillCurrentArcQuest1))))
                ),
                quest1()
            );
        }
        else if (storyName == "quest2")
        {
            return new SelectorParallel(
                 (
                    (new DecoratorInvert(new DecoratorLoop(new LeafAssert(isStillCurrentArcQuest2))))
                ),
                quest2()

            );
        }
        else if (storyName == "storyPart2")
        {
            return new SelectorParallel(
                 (
                    (new DecoratorInvert(new DecoratorLoop(new LeafAssert(isStillCurrentArcPart2))))
                ),
                part2()

            );
        }

        return new DecoratorLoop(new LeafWait(10000));
    }



    protected Node BuildTreeRoot()
    {
        //currentArc = "intro";
        Node roaming =
        new SelectorParallel(
            walkAround(),
           (new DecoratorLoop
                (new Sequence(
                    //select story 1: everyone just walking around, cops looking for anything suspicious...
                    selectStory("intro", robber, policeman, person1, person2),
                    //selectStory("nothing", robber, policeman, robber, person2),
                    selectStory("storyPart1", robber, policeman, person1, person2),
                    selectStory("quest1", robber, policeman, person1, person2),
                    selectStory("quest2", robber, policeman, person1, person2),
                    selectStory("storyPart2", robber, policeman, person1, person2)
                //selectStory("loseGame", robber, policeman, person1, person2)
                //robber caught by police, goes to get executed
                //ST_beginConversation(robber, policeman)
                //robber robbing a person

                //
                )

            )


           )

       );

        return roaming;
    }
}

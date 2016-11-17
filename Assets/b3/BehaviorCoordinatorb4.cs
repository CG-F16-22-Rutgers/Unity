using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;
using UnityEngine.UI;

public class BehaviorCoordinatorb4 : MonoBehaviour
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


    //public agents
    public GameObject robber;
    public GameObject policeman;
    public GameObject person1;
    public GameObject person2;

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

    //old behaviors: ------------------------------------------------------------------------------------------------
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



    protected Node ST_TurnTo(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().ST_TurnToFace(position));
    }

    protected Node ST_ApproachAndWait(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
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
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_Sees(ges, dur, isActive));
    }
    protected Node ST_Sees2(GameObject king, GameObject robber)
    {
        bool b = false;
        isActive2 = (object)b;
        Val<GameObject> ges = Val.V(() => king);
        Val<GameObject> dur = Val.V(() => robber);
        Val<object> isActive = Val.V(() => isActive2);
        return new Sequence(policeman.GetComponent<BehaviorMecanim>().Node_Sees(ges, dur, isActive));
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
    public string currentStory;
    //new behaviors:

    public bool isStillCurrentArc()
    {
        if (currentStory == currentArc)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Node selectStory(string storyName, GameObject agent1, GameObject agent2, GameObject agent3, GameObject agent4)
    {
        Val<GameObject> p1 = Val.V(() => agent1);
        Val<GameObject> p2 = Val.V(() => agent2);
        Val<GameObject> p3 = Val.V(() => agent3);
        Val<GameObject> p4 = Val.V(() => agent4);
        currentStory = storyName;
        if (storyName == "walkAround")
        {
            return new SelectorParallel(
                (new DecoratorLoop (new LeafAssert(isStillCurrentArc))),
                (agent1.GetComponent<BehaviorMecanim>().Node_BeginConversation(p1, p2, true))

            );
        }
        return (agent1.GetComponent<BehaviorMecanim>().Node_BeginConversation(p3, p4, true));
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
                    selectStory("walkAround", robber, policeman, person1, person2)
                //robber caught by police, goes to get executed

                //robber robbing a person
                        
                //
                )
               
            )
           
           
           )//,
           #endregion
           #region MonitorUI
           //(),
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

using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class BehaviorCoordinator : MonoBehaviour
{
    public Transform wander1;
    public Transform wander2;
    public Transform wander3;
    public GameObject participant;


    public GameObject king;
    public GameObject knight;
    //public GameObject robber;

    private BehaviorAgent behaviorAgent;
    // Use this for initialization
    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected Node ST_talk( GameObject guy1, GameObject guy2)
    {
        //Val<Vector3> position = Val.V(() => target.position);
        Val<GameObject> guy1Val = Val.V(() => guy1);
        Val<GameObject> guy2Val = Val.V(() => guy2);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_Conversation(guy1,guy2,true), new LeafWait(10));
    }


    protected Node ST_Approach(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(4));
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
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

  public Transform kingFrontPosition;

protected Node BuildTreeRoot()
{
 
        Node roaming =
                        new Sequence(
                           /* (new DecoratorLoop
                                (new DecoratorInvert
                                    (new Selector
                                        (this.ST_Approach(kingFrontPosition))
                                    )
                                )
                            ),*/

                            //this.ST_ApproachAndWait(this.wander1),
                            //this.ST_ApproachAndWait(this.wander2),
                            //this.ST_ApproachAndWait(this.wander3)));
                            this.ST_talk(king, knight),
                            
                            
                            (new DecoratorLoop(new Selector
                                        (this.ST_Approach(kingFrontPosition))
                            ))



                        );
    return roaming;
}
}

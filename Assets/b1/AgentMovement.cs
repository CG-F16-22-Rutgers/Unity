using UnityEngine;
using System.Collections;

public class AgentMovement : MonoBehaviour
{
    int isMoving = 0;
    public NavMeshAgent agent;
    public bool isActive = false;
    public GameObject mainCam;
    public Vector3 destination;
    public float maxdistanceToDest = 1.5f;

    public GameObject character;

    void Start()
    {
        agent.stoppingDistance = 1f;
    }

    // Update is called once per frames
    void Update()
    {
        //if isActive, tag the object
        if (isActive)
        {
            gameObject.tag = "active";
     
            agent.acceleration = 8;
            maxdistanceToDest = 2f;
            //change color :)

        }
        else
        { 
            gameObject.tag = "inactive";
        }

        if (character.transform.position.y < -1)
        {
            character.transform.position = new Vector3(12.49f, 5.98f, 18.95f);
        }

        if (agent.hasPath && agent.remainingDistance >= maxdistanceToDest)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo))
            {

                Transform rayTemp = hitInfo.transform;
                if (rayTemp.tag == "active" || rayTemp.tag == "inactive")
                {
                    if (rayTemp.gameObject.GetComponent<NavMeshAgent>().remainingDistance <= rayTemp.gameObject.GetComponent<NavMeshAgent>().stoppingDistance)
                    {
                        //this means agent found his destination
                        agent.destination = rayTemp.position;
                        agent.stoppingDistance = 4f;
                    }
                }
            }
            agent.Resume();
            isMoving = 1;
        }
        else
        {
            isMoving = 0;
            agent.velocity = new Vector3(0, 0, 0);
            agent.Stop();
        }

        if (isActive)
        {
            agent.destination = destination;

        }

    }


}

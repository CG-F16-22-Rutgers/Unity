using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ErikaNavigation : MonoBehaviour {

    
    public Vector3 destination;
    NavMeshAgent navmesha;
    public bool destinationChange;
    List<Vector3> path;
    bool isMoving;

    //animator
    private Animator animator;

    //update currentPosition and once pass currentNextCorner
    Vector3 currentPosition;
    int currentNextCornerIndex;

    //damping
    float walkx;
    private float velocityWalkX = 0;
    float dampWalkX;

    float walky;
    private float velocityWalkY = 0;
    float dampWalkY;
    int countSamePos;

    Vector3 previousPosition;
    //change speed
    public float desiredSpeed;
    //float speed;
    public int jumping;

    public int traversingOffMeshLink;

    // Use this for initialization
    void Start () {
        currentNextCornerIndex = -1;
        navmesha = GetComponent<NavMeshAgent>();
        //destinationChange = false;
        animator = GetComponent<Animator>();
        dampWalkX = .1f;
        dampWalkY = .2f;
        jumping = 0;
        countSamePos = 0;
        traversingOffMeshLink = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumping != 0)
        {
            jumping++;
            animator.SetBool("Jump", true);
        }
        if (jumping > 20)
        {
            animator.SetBool("Jump", false);
            jumping = 0;
        }
        //check if we are at offmeshlink with this agent. if so and still has some ways to go to destination, we jump in direction of destination
        GameObject[] obj = GameObject.FindGameObjectsWithTag("offmeshLink");
        GameObject closeOffMesh = null;
        foreach (GameObject i in obj)
        {
            if(Vector3.Distance(transform.position, i.transform.position) < 1)
            {
                closeOffMesh = i;
            }
  
            //check positions, which one is 
            //i.GetComponent<ErikaNavigation>().desiredSpeed = desiredSpeed;
        }

        if (closeOffMesh != null)
        {
            if (traversingOffMeshLink > 120)
            {
                Debug.Log("We are no longer traversing offmeshlink");
                traversingOffMeshLink = 0;
            }
            //first find what this offmesh link connects to
            Vector3 endOffMesh = closeOffMesh.GetComponent<OffMeshLink>().endTransform.position;
            Vector3 destination;
            if(currentNextCornerIndex != -1 && path != null)
            {
                destination = path[currentNextCornerIndex];
                //vector to destination from current position
                Vector3 currentToDestination = destination - transform.position;

                //vector from destination to endOffMesh link from current position
                Vector3 currentToEndMesh = endOffMesh - transform.position;

                //compare the two vectors. if the angle between them is less than 10, we good to use the offmeshlink
                if(Vector3.Angle(currentToDestination, currentToEndMesh)<10)
                {
                    //first turn towards destination
                    if (Vector3.Angle(currentToEndMesh, transform.forward) > 4f)
                    {
                        animator.SetFloat("WalkingY", 0);
                        //Debug.Log("We are first turning");
                        if (Vector3.Cross(currentToEndMesh, transform.forward).y < 0)
                        {
                            walkx = Mathf.SmoothDamp(walkx, .7f, ref velocityWalkX, dampWalkX);
                            animator.SetFloat("WalkingX", walkx);
                        }
                        else
                        {
                            walkx = Mathf.SmoothDamp(walkx, -.7f, ref velocityWalkX, dampWalkX);
                            animator.SetFloat("WalkingX", walkx);
                        }
                        traversingOffMeshLink++;
                        //animator.SetFloat("WalkingY", 0f);
                        //animator.SetFloat("WalkingX", .6f);
                    }
                    else
                    {
                        if (traversingOffMeshLink > 120)
                        {
                            Debug.Log("We are no longer traversing offmeshlink");
                            traversingOffMeshLink = 0;
                        }

                        if (traversingOffMeshLink != 0)
                        {
                            Debug.Log(traversingOffMeshLink);
                            animator.SetFloat("WalkingX", 0);
                            //then run and jump
                            walky = Mathf.SmoothDamp(walky, .9f, ref velocityWalkY, .03f);
                            animator.SetFloat("WalkingY", walky);
                            animator.SetBool("Jump", true);
                            isMoving = true;
                            jumping++;
                            Debug.Log("We are jumping?");
                        }
                    }

                }


            }

        }

        

        
        

        if (traversingOffMeshLink == 0)
        {
            //if we are not moving, make sure the animator variables are reset;
            if (isMoving == false)
            {
                animator.SetFloat("WalkingY", 0);
                animator.SetFloat("WalkingX", 0);
                animator.SetFloat("Strafe", 0f);
                animator.SetBool("Jump", false);
            }
            previousPosition = currentPosition;
            currentPosition = transform.position;
            if (currentPosition != null)
            {
                if (Mathf.Abs(currentPosition.x - previousPosition.x) < 1 && Mathf.Abs(currentPosition.z - previousPosition.z) < 1)
                {
                    //Debug.Log("They are very similar");
                    countSamePos++;
                }
                else
                {
                    countSamePos = 0;
                }
                if (countSamePos > 9000 && countSamePos < 10000)
                {
                    if (currentNextCornerIndex != -1 && currentNextCornerIndex < path.Count && currentPosition.y + .05f < path[currentNextCornerIndex].y)
                    {
                        animator.SetBool("Jump", true);
                        jumping++;
                    }
                }
                if (countSamePos > 10000)
                {
                    destinationChange = true;
                }
            }
            //if destination is changed, we update the destination, variables, etc
            if (destinationChange == true)
            {
                //disable and enable to figure out path
                navmesha.enabled = true;
                NavMeshPath navMeshPath = new NavMeshPath();
                navmesha.CalculatePath(destination, navMeshPath);
                path = new List<Vector3>(navMeshPath.corners);
                navmesha.enabled = false;
                destinationChange = false;
                isMoving = true;
                currentNextCornerIndex = 1;
                walky = Mathf.SmoothDamp(walky, 0f, ref velocityWalkY, dampWalkY);
                walkx = Mathf.SmoothDamp(walkx, 0f, ref velocityWalkX, dampWalkX);
                animator.SetFloat("WalkingY", walky);
                animator.SetFloat("WalkingX", walkx);
            }

            //if there is a path
            if (path != null && currentNextCornerIndex != -1 && currentNextCornerIndex < path.Count)
            {
                //check if we reach the current next index
                if (Vector3.Distance(path[currentNextCornerIndex], currentPosition) < 2)
                {
                    currentNextCornerIndex++;
                    walky = Mathf.SmoothDamp(walky, 0f, ref velocityWalkY, dampWalkY);
                    walkx = Mathf.SmoothDamp(walkx, 0f, ref velocityWalkX, dampWalkX);
                    animator.SetFloat("WalkingY", walky);
                    animator.SetFloat("WalkingX", walkx);
                }

                //check if we reach the end destination
                if (currentNextCornerIndex == path.Count)
                {
                    isMoving = false;
                    currentNextCornerIndex = -1;
                    walky = Mathf.SmoothDamp(walky, 0f, ref velocityWalkY, .01f);
                    walkx = Mathf.SmoothDamp(walkx, 0f, ref velocityWalkX, .01f);
                    animator.SetFloat("WalkingY", 0);
                    animator.SetFloat("WalkingX", 0);
                    animator.SetFloat("Strafe", 0f);
                    animator.SetBool("Jump", false);
                    path = null;
                }

                //if we havent reached current next corner, we move towards next corner
                else
                {
                    //Debug.Log("We are going towards corner");
                    //first face the next corner:
                    //angle between transform.forward and dest-start
                    Vector3 vectorFromCurrToDest = path[currentNextCornerIndex] - currentPosition;
                    vectorFromCurrToDest.y = 0;
                    Vector3 currentForward = transform.forward;
                    currentForward.y = 0;
                    //if we have not yet started to move and angle is off:
                    if (animator.GetFloat("WalkingY") == 0f && Vector3.Angle(vectorFromCurrToDest, currentForward) > .5f)
                    {

                        //Debug.Log("We are first turning");
                        if (Vector3.Cross(vectorFromCurrToDest, currentForward).y < 0)
                        {
                            walkx = Mathf.SmoothDamp(walkx, .7f, ref velocityWalkX, dampWalkX);
                            animator.SetFloat("WalkingX", walkx);
                        }
                        else
                        {
                            walkx = Mathf.SmoothDamp(walkx, -.7f, ref velocityWalkX, dampWalkX);
                            animator.SetFloat("WalkingX", walkx);
                        }

                        //animator.SetFloat("WalkingY", 0f);
                        //animator.SetFloat("WalkingX", .6f);
                    }
                    //if already facing next corner, move in the direction
                    else
                    {
                        //if angle still doesnt match:
                        if (Vector3.Angle(vectorFromCurrToDest, currentForward) > .5f)
                        {
                            //Debug.Log("We are second turning");
                            float factor = Mathf.Sqrt((Vector3.Angle(vectorFromCurrToDest, currentForward)) / 364.6f);
                            if (Vector3.Cross(vectorFromCurrToDest, currentForward).y < 0)
                            {
                                walkx = Mathf.SmoothDamp(walkx, factor, ref velocityWalkX, dampWalkX);
                                animator.SetFloat("WalkingX", walkx);
                            }
                            else
                            {
                                walkx = Mathf.SmoothDamp(walkx, -factor, ref velocityWalkX, dampWalkX);
                                animator.SetFloat("WalkingX", walkx);
                            }
                            if ((factor > .6 || factor < .6) && (Vector3.Distance(transform.position, path[currentNextCornerIndex]) < 5))
                            {
                                desiredSpeed = .5f;
                            }
                        }
                        else
                        {
                            //if need to jump do so
                            //if (currentPosition.y+.05f < path[currentNextCornerIndex].y)
                            //{
                            //    animator.SetBool("Jump", true);
                            //}



                            walkx = Mathf.SmoothDamp(walkx, 0f, ref velocityWalkX, dampWalkX);
                            animator.SetFloat("WalkingX", walkx);
                        }

                        //Debug.Log("We are moving");
                        walky = Mathf.SmoothDamp(walky, desiredSpeed, ref velocityWalkY, dampWalkY);
                        //animator.SetFloat("WalkingY", .5f);
                        //animator.SetFloat("WalkingX", 0f);
                        //walkx = 0f;//Mathf.SmoothDamp(walkx, 0f, ref velocityWalkX, dampWalkX);
                        //animator.SetFloat("WalkingX", walkx);
                        animator.SetFloat("WalkingY", walky);


                    }
                }
            }
        }
        else
        {
            traversingOffMeshLink++;
        }

    }
    /*
    void OnAnimatorMove()
    {
        //transform.position = transform.position + transform.forward;
        if (isMoving == true)
        {
            Quaternion lookRotation = Quaternion.LookRotation(path[currentNextCornerIndex]- currentPosition);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 200 * Time.deltaTime);
        }

    }*/
}

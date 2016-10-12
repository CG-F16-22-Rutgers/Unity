using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour
{
    RaycastHit hitInfo = new RaycastHit();
    Transform temp;
    Transform individualTemp;
    public Vector3 destination;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                individualTemp = hitInfo.transform;
                if (individualTemp.tag == "active" || individualTemp.tag == "director" || individualTemp.tag == "inactive" || individualTemp.tag == "following")
                {
                    //nothing
                }
                else
                {
                    individualTemp = null;
                }

            }
        }
        
        


            if (individualTemp != null)
            {
                individualTemp.GetComponent<AgentMovement>().enabled = true;
            }
        


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                temp = hitInfo.transform;
				if (temp.tag == "Obstacle") {
					Obstacle obs = temp.GetComponent<Obstacle>();
					obs.isActive = !obs.isActive;
				} else {
					if (temp.GetComponent<NavMeshAgent>())
					{
						AgentMovement setToActive = temp.GetComponent<AgentMovement>();
						setToActive.isActive = !setToActive.isActive;
						setToActive.destination = temp.position;
					}
					else
					{
						destination = hitInfo.point;
						//put destination in active guys
						GameObject[] obj = GameObject.FindGameObjectsWithTag("active");
						foreach (GameObject i in obj)
						{

							i.GetComponent<AgentMovement>().destination = destination;
						}
					}
				}
			}
        }
    }
}

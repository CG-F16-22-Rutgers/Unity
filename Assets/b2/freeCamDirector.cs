using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class freeCamDirector : MonoBehaviour
{
    RaycastHit hitInfo = new RaycastHit();
    Transform temp;
    Transform individualTemp;
    public Vector3 destination;
    public Text speedText;

    public float desiredSpeed;

    void Start()
    {
        desiredSpeed = .5f;
    }

    void Update()
    {
        //right mouse press
        if (Input.GetMouseButtonDown(1))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                individualTemp = hitInfo.transform;
                if (individualTemp.tag == "active")
                {
                    individualTemp.tag = "inactive";
                }
                else if (individualTemp.tag == "inactive")
                {
                    individualTemp.tag = "active";
                }
                else
                {
                    individualTemp = null;
                    destination = hitInfo.point;

                    GameObject[] obj = GameObject.FindGameObjectsWithTag("active");
                    foreach (GameObject i in obj)
                    {

                        i.GetComponent<ErikaNavigation>().destination = destination;
                        i.GetComponent<ErikaNavigation>().destinationChange = true;
                        i.GetComponent<ErikaNavigation>().desiredSpeed = desiredSpeed;
                    }
                }

            }
        }

        //increase or decrease speed if keys i or k are pressed
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (desiredSpeed < .9f)
            {
                desiredSpeed = desiredSpeed + .1f;
            }
            GameObject[] obj = GameObject.FindGameObjectsWithTag("active");
            foreach (GameObject i in obj)
            {
                i.GetComponent<ErikaNavigation>().desiredSpeed = desiredSpeed;
            }
            obj = GameObject.FindGameObjectsWithTag("inactive");
            foreach (GameObject i in obj)
            {
                i.GetComponent<ErikaNavigation>().desiredSpeed = desiredSpeed;
            }
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (desiredSpeed > .1f)
            {
                desiredSpeed = desiredSpeed - .1f;
            }
            GameObject[] obj = GameObject.FindGameObjectsWithTag("active");
            foreach (GameObject i in obj)
            {
                i.GetComponent<ErikaNavigation>().desiredSpeed = desiredSpeed;
            }
            obj = GameObject.FindGameObjectsWithTag("inactive");
            foreach (GameObject i in obj)
            {
                i.GetComponent<ErikaNavigation>().desiredSpeed = desiredSpeed;
            }
        }

        //update textfield with speed on screen
        speedText.text = "speed: " + desiredSpeed;

    }
}

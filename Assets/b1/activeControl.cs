using UnityEngine;
using System.Collections;

public class activeControl : MonoBehaviour
{

    MeshRenderer temp;

    // Use this for initialization
    void Start()
    {
        temp = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.tag == "active")
        {
            temp.enabled = true;
        }
        else
        {
            temp.enabled = false;
        }
    }
}

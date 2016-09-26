using UnityEngine;
using System.Collections;

//Moves the main camera when right buttons pressed
public class mainCameraControls : MonoBehaviour
{

    public float speed = 50.0f; //max speed of camera

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement of Camera
        Vector3 dir = new Vector3(); //create (0,0,0)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            dir.z = dir.z + 1.0f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            dir.z = dir.z - 1.0f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            dir.x = dir.x - 1.0f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            dir.x = dir.z + 1.0f;
        }

        dir.Normalize();

        transform.Translate(dir * speed * Time.deltaTime);
    }
}

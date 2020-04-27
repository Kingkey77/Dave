using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dave : MonoBehaviour
{
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up);
        }
        else if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A)&& Input.GetKey(KeyCode.D))
            {
                print("Nope");
            }
            else if (Input.GetKey(KeyCode.A))
            {
                print("Rotate Left");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                print("Rotate Right");
            }
            
        }
    }
}

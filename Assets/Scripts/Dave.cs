using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dave : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;
    bool audioClip;
    bool isPlaying;
    [SerializeField]float thrustMultiplier = 1000f;
    [SerializeField]float rcsThrust = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //fartPlay = true;
        //playToggle = true;
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotation();
    }
    protected void LateUpdate()         //Added to stop wierd rotations in x and y
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
    }

    private void Thrust()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            float thrustThisFrame;
            thrustThisFrame = thrustMultiplier * Time.deltaTime;
            rb.AddRelativeForce(Vector3.up * thrustThisFrame);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotation()
    {
        
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        rb.freezeRotation = true;       //take maunal control of rotation 
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {            
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                print("Nope");
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward * rotationThisFrame);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward * rotationThisFrame);
            }
        }       
        rb.freezeRotation = false;      //resume physics conrol of rigidbody  
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            default:
                print("dead");
                break;
        }
                
    }
}

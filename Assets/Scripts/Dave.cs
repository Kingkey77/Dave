using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dave : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource davesAudioSource;
    bool playDaveClip;
    bool isPlaying;
    public float thrustMultiplier; 
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        davesAudioSource = GetComponent<AudioSource>();
        //fartPlay = true;
        //playToggle = true;
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
        Thrust();

    }

    private void Rotation()
    {
        rb.freezeRotation = true;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                print("Nope");
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward);
            }
        }
       
        rb.freezeRotation = false;
        
    }

    private void Thrust()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustMultiplier);
            if (!davesAudioSource.isPlaying)
            {
                davesAudioSource.Play();
            }
        }
        else
        {
            davesAudioSource.Stop();
        }
    }
}

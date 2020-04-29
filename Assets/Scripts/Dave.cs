using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dave : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource davesAudioSource;
    bool fartPlay;
    bool playToggle;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        davesAudioSource = GetComponent<AudioSource>();
        fartPlay = true;
        playToggle = true;
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
            rb.AddRelativeForce(Vector3.up);
            PlayClip();
        }
        else if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A)&& Input.GetKey(KeyCode.D))
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
    }

    private void PlayClip()
    {
        //Check to see if you just set the toggle to positive
        if (fartPlay == true && playToggle == true)
        {
            //Play the audio you attach to the AudioSource component
            davesAudioSource.Play();
            //Ensure audio doesn’t play more than once
            playToggle = false;
        }
        //Check if you just set the toggle to false
        //if (m_Play == false && m_ToggleChange == true)
        //{
            //Stop the audio
            //m_MyAudioSource.Stop();
            //Ensure audio doesn’t play more than once
            //m_ToggleChange = false;
        //}
    }
}

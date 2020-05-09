using UnityEngine;
using UnityEngine.SceneManagement;

public class Dave : MonoBehaviour
{
    [SerializeField] float thrustMultiplier = 1000f;
    [SerializeField] float rcsThrust = 10f;

    [SerializeField] AudioClip daveThrustSound;
    [SerializeField] AudioClip daveOuch;
    [SerializeField] AudioClip levelCompleteChime;
    [SerializeField] AudioClip daveOhNo;

    [SerializeField] ParticleSystem daveThrustParticles;
    [SerializeField] ParticleSystem daveSuccessParticles;
    [SerializeField] ParticleSystem daveExplodesParticles;

    private Rigidbody rb;
    private AudioSource audioSource;

    bool audioClip;
    bool isPlaying;
    
    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput ();
            Rotation();
        }
    }
    protected void LateUpdate()         //Added to stop wierd rotations in x and y
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
    }

    private void RespondToThrustInput()
    {
            if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
            {
                audioSource.Stop();
                daveThrustParticles.Stop();
            }
        
    }

    private void ApplyThrust()
    {
        float thrustThisFrame = thrustMultiplier * Time.deltaTime;
        rb.AddRelativeForce(Vector3.up * thrustThisFrame);
        daveThrustParticles.Play();
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(daveThrustSound);
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
        if(state != State.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":

                StartBumpSequence();
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();                
                break;
        }
                
    }

    private void StartBumpSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(daveOuch);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        daveExplodesParticles.Play();
        if (audioSource.isPlaying)
        { audioSource.Stop(); }
        audioSource.PlayOneShot(daveOhNo);
        Invoke("LoadFirstLevel", 1f);
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(levelCompleteChime);
        daveSuccessParticles.Play();
        Invoke("LoadNextLevel", 1f);        //Parameterise this time
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustForce = 100;
    [SerializeField] float rotateForce = 1;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem sideThrusterRight;
    [SerializeField] ParticleSystem sideThrusterLeft;
    [SerializeField] ParticleSystem rocketThrusters;

    bool isTransitioning = false;

    ParticleSystem rocketParticles;

    Rigidbody rb;

    AudioSource audioSource;
   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();

        rocketParticles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);

            if (isTransitioning)
            {
                return;
            }
            else
            {
                rocketParticles.Stop();
                rocketParticles.Play(rocketThrusters);

                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(mainEngine);
                }
            }
           
           // Debug.Log("Thrusters ON");
        }
        else
        {
            audioSource.Stop();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateForce);
            rocketParticles.Stop();
            rocketParticles.Play(sideThrusterLeft);
            Debug.Log("Pressed A - Rotate left");
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateForce);
            rocketParticles.Stop();
            rocketParticles.Play(sideThrusterRight);
            Debug.Log("Pressed D - Rotate right");
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;

        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        

        rb.freezeRotation = false;
    }
}

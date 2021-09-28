using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustForce = 100;
    [SerializeField] float rotateForce = 1;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            Debug.Log("Thrusters ON");
        }
        
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateForce);
            Debug.Log("Pressed A - Rotate left");
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateForce);
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

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;


    private Rigidbody rb;
    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        transform.Rotate(Vector3.forward * -rotationInput * rotationStrength * Time.fixedDeltaTime);

        if (rotationInput == 0)
            rb.freezeRotation = false;
        else
            rb.freezeRotation = true;

        if (rotationInput > 0)
        {
            if (!leftBooster.isPlaying)
                leftBooster.Play();
        }
        else if (rotationInput < 0)
        {
            if (!rightBooster.isPlaying)
                rightBooster.Play();
        }
        else
        {
            leftBooster.Stop();
            rightBooster.Stop();
        }

    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }

            if (!mainBooster.isPlaying)
                mainBooster.Play();
        }
        else
        {
            audioSource.Stop();
            mainBooster.Stop();
        }
    }
}

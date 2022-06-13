using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField]float mainThrust = 100f;
    [SerializeField]float rotationForce = 1f;
    [SerializeField]AudioClip mainEngine;
    AudioSource audioSource;
    [SerializeField] ParticleSystem mainParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem RightParticles;
    bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        StartThrusting();
    }

    private void StartThrusting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); // rigidBody.AddRelativeForce(0,1,0);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainParticles.isPlaying)
            {
                mainParticles.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainParticles.Stop();
        }
    }

    void ProcessRotation(){
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotate();
        }
    }

    private void StopRotate()
    {
        leftParticles.Stop();
        RightParticles.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationForce);
        if (!RightParticles.isPlaying)
        {
            RightParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationForce);
        if (!leftParticles.isPlaying)
        {
            leftParticles.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rigidBody.freezeRotation = true;    //freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);  // transform.Rotate(0,0,1);
        rigidBody.freezeRotation = false;    // unfreeze rotation so physics system can can take over
        
    }
}

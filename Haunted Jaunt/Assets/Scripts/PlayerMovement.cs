using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f; //sets rotation speed

    Animator m_Animator; //calls animator to move
    Rigidbody m_Rigidbody; //sets character as able to move
    AudioSource m_AudioSource; //sets footsteps audio source
    Vector3 m_Movement; //enables movement
    Quaternion m_Rotation = Quaternion.identity; //enables rotation
    public GameObject bullet; //calls bullet
    public Transform shotSpawn; //creates bullets

    // Start is called before the first frame update
    void Start()
    {
        //set animator reference
        m_Animator = GetComponent<Animator>();

        //set rigidbody reference
        m_Rigidbody = GetComponent<Rigidbody>();

        //set audio source reference
        m_AudioSource = GetComponent<AudioSource>();
    }

    // fixed update != update
    void FixedUpdate()
    {
        //find the values of each axis
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //sets movement to the values of the axis
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize(); //makes speed going diagonally
                                //the same as horizontally or vertically

        //determines if there is input
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        //if input bool to be set as true/false
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        //set the animator parameter
        m_Animator.SetBool("IsWalking", isWalking);

        //set the audio to play if walking is active
        if (isWalking)
        {
            //if the audio isn't playing, set to play
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }

        //sets the audio to play if walking is inactive
        else
        {
            m_AudioSource.Stop();
        }

        //calculate character's forward vector
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);

        //rotation set
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        //executes movement
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);

        //executes rotation
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    //shoot method
    void shoot()
    {
        GameObject g = Instantiate(bullet, shotSpawn.position, shotSpawn.rotation) as GameObject;
        Destroy(g, 1.5f);
    }

    //update is run once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            shoot();
        }
    }

}

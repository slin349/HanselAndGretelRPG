using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    Vector3 _velocity;
    bool isGrounded;

    private Animator animator;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        GetInput();
        Movement();
    }

    

    void Movement()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");

        if (_xMov != 0 || _zMov != 0)
        {
            // animator.SetBool("IsWalking", true);
            animator.SetInteger("condition", 1);
        }
        else
        {
            // animator.SetBool("IsWalking", false);
            animator.SetInteger("condition", 0);

        }

        // Lateral movement
        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;
        Vector3 move = (_movHorizontal + _movVertical).normalized * speed;
        controller.Move(move * Time.deltaTime);
        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("in_air", true);
            Invoke("Jump", 0.3f);
        }
        else
        {
            animator.SetBool("in_air", false);
        }

        _velocity.y += gravity * Time.deltaTime;

        controller.Move(_velocity * Time.deltaTime);



    }

    void GetInput()
    {
  
     



    }

    void Jump()
    {
        _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

    }


    
}

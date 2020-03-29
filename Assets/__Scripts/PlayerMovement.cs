using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float defaultSpeed = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    Vector3 _velocity;
    bool isGrounded;

    private float speed;
    private float jumpRate = 1f;
    private float nextJump;
    private Animator animator;

    public Health health;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        speed = defaultSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        if (!health.isDead)
        {
            // Player is not dead
            Movement();
        }
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

        // Walking animations
        if ((_xMov != 0 || _zMov != 0) && Input.GetKey(KeyCode.LeftShift))
        {
            // Running
            speed = defaultSpeed * 2;
            animator.SetInteger("condition", 2);
        }
        else if ((_xMov != 0 || _zMov != 0) && !Input.GetKey(KeyCode.LeftShift))
        {
            // Walking
            speed = defaultSpeed;
            animator.SetInteger("condition", 1);

        }
        else
        {
            // Idling
            animator.SetInteger("condition", 0);
        }

 

        // Lateral movement
        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;
        Vector3 move = (_movHorizontal + _movVertical).normalized * speed;
        controller.Move(move * Time.deltaTime);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded && Time.time > nextJump) 
        {
            nextJump = Time.time + jumpRate;    // Prevent input buffering of the jump
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

    void Jump()
    {
        _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

    }


    
}

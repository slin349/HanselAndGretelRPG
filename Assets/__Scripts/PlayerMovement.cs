using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    Vector3 velocity;
    bool isGrounded;

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");
        
        if (_xMov != 0 || _zMov != 0)
        {
            animator.SetBool("IsWalking", true);
        } else
        {
            animator.SetBool("IsWalking", false);
        }

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        controller.Move(_velocity * Time.deltaTime);

        // Vector3 move = transform.right * x + transform.forward * z;
        // controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("OnJump");
            Invoke("Jump", 0.3f);
        }

        velocity.y += 2 * gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool deceleratingN;
    bool deceleratingP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        if (x != 0)
        {
            velocity = move * speed * 100;
        } 
        else
        {
            if (velocity.x == (-1f * speed * 100))
            {
                velocity.x += speed * 20;
                deceleratingN = true;
            }
            else if (velocity.x == speed * 100) 
            {
                velocity.x -= speed * 20;
                deceleratingP = true;
            }
        }
        if (z != 0)
        {
            velocity.z = speed * 100 * z;
        } 
        else
        {
            if (velocity.z == (-1f * speed * 100))
            {
                velocity.z += speed * 20;
                deceleratingN = true;
            }
            else if (velocity.z == speed * 100)
            {
                velocity.z -= speed * 20;
                deceleratingP = true;
            }
        }
        
        if (deceleratingN && (velocity.x < 0 || velocity.z < 0))
        {
            deceleratingN = false;
        }

        if (deceleratingP && (velocity.x > 0 || velocity.z > 0))
        {
            deceleratingP = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

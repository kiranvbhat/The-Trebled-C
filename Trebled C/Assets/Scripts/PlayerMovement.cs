using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    float horizontalMove = 0f;
    public float runSpeed = 10f;
    public bool jump = false;
    public float jumpForce = 600f;
    // Update is called once per frame
    void Update()
    {
        // horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // if (Input.GetButtonDown("Jump"))
        // {
        //     jump = true;
        // }
    }


    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump, jumpForce);
        jump = false;
    }

    public void Jump(float force) {
        jump = true;
        jumpForce = force;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : EntityMovement
{

    // Use this for initialization


    protected override void jump()
    {
        if (jumpCounter > 0)
        {

            Debug.Log("jumpcounter = " + jumpCounter);
            if (Input.GetButtonDown("Jump"))
            {
                
                moveDirection.y = jumpSpeed;
                jumpCounter--;

                status.jumping = true;
                Debug.Log("Hi");
            }
        }

        if (jumpCounter == maxJumps && !controller.isGrounded)
        {
            jumpCounter = maxJumps - 1;
        }
    }

    protected override void getFinalSpeed()
    {
        finalSpeed += Input.GetAxis("Horizontal") * inputSpeed;
    }

    protected override void gravityAndLadder()
    {
        if (status.grounded)
        {
            //Stopping gravity
            moveDirection.y = 0F;
        }

        else
            if (status.onLadder)
        {
            moveDirection.y = Input.GetAxis("Vertical") * climbSpeed;

            controller.stepOffset = 0.0F;
        }

        else
        {
            moveDirection.y -= gravity * Time.deltaTime;

            controller.stepOffset = 0.6F;
        }
    }
}

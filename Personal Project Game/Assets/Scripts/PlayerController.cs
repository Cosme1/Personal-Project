using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 2;
    public float runSpeed = 6;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //gets input into a 2d Vector
        Vector2 inputDir = input.normalized;
        
        if (inputDir != Vector2.zero) {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg; //rotates player (some trigenometry involved)
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        bool running = Input.GetKey (KeyCode.LeftShift); //makes the player run when the shiftkey is pressed
        float targetspeed = ((running)?runSpeed:walkSpeed) * inputDir.magnitude; //this makes the player change between walk and runspeed
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetspeed, ref speedSmoothVelocity, speedSmoothTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World); //makes the player move

        float animationSpeedPercent = ((running)?1:.5f) * inputDir.magnitude; //checks again if character is running but then saves it into the animationSpeedPercent value for the Animator
        animator.SetFloat ("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime); //gets the speedPercent value from the animator and sets it to the animationSpeedPercent
    }

    
}

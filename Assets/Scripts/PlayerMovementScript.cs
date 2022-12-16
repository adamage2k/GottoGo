using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;

    [SerializeField] float runSpeed = 10;
    [SerializeField] float jumpForce = 5;
    bool isTouchingPlatform = true;

    Animator myAnimator;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value) 
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) 
    {
        
        isTouchingPlatform = myRigidbody.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (!isTouchingPlatform) 
        {
            return;
        }
        else if (value.isPressed) 
        {
            myRigidbody.velocity += new Vector2(0f, jumpForce);
        }
    }

    void Run() 
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite() 
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}

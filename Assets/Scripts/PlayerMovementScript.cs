using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myBoxCollider;
    SpriteRenderer mySpriteRenderer;
    
    [SerializeField] ParticleSystem myDeathParticle;
    [SerializeField] float runSpeed = 10;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float climbSpeed = 4;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float respawnDelay = 1f;

    float worldGravity;
    float climbGravity = 0;
    bool isAlive = true;

    Animator myAnimator;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        worldGravity = myRigidbody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value) 
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) 
    {
        if (!isAlive) { return; }
        if (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            return;
        }
        else if (value.isPressed) 
        {
            myRigidbody.velocity += new Vector2(0f, jumpForce);
        }
    }

    void OnFire(InputValue value) 
    {
        if(!isAlive) { return; }
        if (value.isPressed) 
        {
            Instantiate(bullet, bulletSpawn.position, transform.rotation);
        }
    }

    void ClimbLadder()
    {

        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = worldGravity;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = climbGravity;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void Run() 
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
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

    void Die() 
    {
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"))) 
        {
            isAlive = false;
            myDeathParticle.Play();
            myAnimator.SetTrigger("Dying");
            CameraShake.Instance.ShakeCamera(3f, .8f);
            mySpriteRenderer.enabled = false;
            Invoke("ReloadScene", respawnDelay);
        }
    }

    void ReloadScene() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

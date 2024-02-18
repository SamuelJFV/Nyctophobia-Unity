using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public enum PlayerDeath
{
    GeneralDeath = 1,
    TvMonster = 2,
    Drown = 3,
    MirrorMonster = 4,
    HallwayMonster = 5,
}
public class PlayerController : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject colliders;
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpSpeed;
    public bool isGrounded = true;
    [SerializeField] bool isFlipping;
    [SerializeField] bool isDying;
    [SerializeField] bool isRunning;
    [SerializeField] bool isCrouching;
    [SerializeField] Animator playerAnimator;
    public bool canJumpCrouch = true;
    Rigidbody2D rb;
    CameraController cameraController;
    float axisInput;
    float currentAxisInput;
    Death death;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraController = mainCamera.GetComponent<CameraController>();
    }
    void Start()
    {
        isDying = false;
    }

    void Update()
    {
        if (!isDying)
        {  
            axisInput = Input.GetAxisRaw("Horizontal");
            isRunning = axisInput != 0;
            isFlipping = isRunning && axisInput != currentAxisInput;
            isCrouching = Input.GetKey(KeyCode.S);

            Move();
            Animate();
        }
        else if (GameManager.Instance.timer < 0)
        {
            GameManager.Instance.LoadMainMenu();
        }
    }

    void Move()
    {
        Run();

        if (isFlipping)
        {
            Flip();
        }        
        
        CrouchAndJump();
    }

    void Animate()
    {
        if (!isGrounded)
        {
            PlayAnimation("Jumping");
        }
        else if (isGrounded && isRunning && !isCrouching)
        {
            PlayAnimation("Running");
        }
        else if (isCrouching && isGrounded && canJumpCrouch)
        {
            if (isRunning)
            {
                PlayAnimation("Crouching");
            }
            else
            {
                PlayAnimation("Crouching Idle");
            }
        }
        else
        {
            PlayAnimation("Idle");
        }
    }

    void PlayAnimation(string name)
    {
        playerAnimator.SetBool("Running", name == "Running");
        playerAnimator.SetBool("Jumping", name == "Jumping");
        playerAnimator.SetBool("Crouching", name == "Crouching");
        playerAnimator.SetBool("Crouching Idle", name == "Crouching Idle");
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
        PlayAnimation("Idle");
    }
    void Run()
    {
        rb.velocity = new Vector2(axisInput * walkSpeed, rb.velocity.y);
    }

    void Flip()
    {
        playerSprite.flipX = axisInput == -1;
        currentAxisInput = axisInput;
    }

    void Jump()
    {
        rb.velocity += jumpSpeed * Vector2.up;
    }

    void CrouchAndJump()
    {
        if (Input.GetKeyDown(KeyCode.S) && canJumpCrouch)
        {
            colliders.transform.localScale = new Vector3(1.00f, 0.50f, 1.00f);
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            colliders.transform.localScale = Vector3.one;
            isCrouching = false;
        }
        else if (Input.GetKeyDown(KeyCode.W) && !isCrouching && isGrounded && canJumpCrouch)
        {
            Jump();
            isGrounded = false;
        }
    }

    public void Die(PlayerDeath typeIndex, int deathTime)
    {
        if (typeIndex != PlayerDeath.MirrorMonster)
        {
            // player does not play mirror monster animation
            playerAnimator.SetInteger("Death", (int) typeIndex);
        }
        GameManager.pausedGame = true;
        GameManager.Instance.StartTimer(deathTime);
        isDying = true;
    }

    public void SetJumpCrouch(bool value)
    {
        canJumpCrouch = value;
        isGrounded = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (other.CompareTag("Follow Horizontal"))
        {
            cameraController.cameraMode = CameraMode.FollowX;
            cameraController.room = other.transform;
        }
        else if (other.CompareTag("Follow Vertical"))
        {
            cameraController.cameraMode = CameraMode.FollowY;
            cameraController.room = other.transform;
        }
        else if (other.CompareTag("Focused Room"))
        {
            cameraController.cameraMode = CameraMode.Focus;
            cameraController.room = other.transform;
        }
        else if (other.CompareTag("Death Zone") && !isDying)
        {
            Stop();
            death = other.GetComponent<Death>();
            if (death.freezeDirection)
            {
                playerSprite.flipX = false;
            }
            Die(death.type, death.time);
        }
    }
}

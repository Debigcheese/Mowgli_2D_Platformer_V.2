using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public GameObject groundCheck;

    private bool isGrounded;
    public float movementSpeed = 2f;
    private float defaultJumpForce;
    private float defaultMovementSpeed;
    private bool IsMoving;
    private float moveDirection = 0f;
    private bool isJumpPressed = false;
    public float jumpForce = 10f;
    private bool isFacingRight = true;
    private Vector3 velocity;
    public float smoothTime = 0.2f;
    private float normalGravityScale;
    public bool isDead = false;

    [SerializeField] private AudioSource WalkSoundEffect;
    [SerializeField] private AudioSource JumpSoundEffect;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Space(10)]
    [Header("Walljumping")]
    [SerializeField] private float wallSlidingSpeed = 1f;
    public bool canWallJump = false;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    [SerializeField] private float wallJumpingDuration = 0.4f;
    [SerializeField] public Vector2 wallJumpingPower = new Vector2(8f, 16f);
    private bool isWallSliding;

    [Space(10)]
    [Header("Knockback")]
    [SerializeField] private Transform _center;
    [SerializeField] private float _knockbackVel = 16f;
    [SerializeField] private float _knockbackTime = 2f;
    [SerializeField] private bool _knockbacked;
    private float _inputX;


    // Start is called before the first frame update
    public void Start()
    {
        canWallJump = false;
        defaultMovementSpeed = movementSpeed;
        defaultJumpForce = jumpForce;
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        normalGravityScale = rigidBody2D.gravityScale;
    }

    // Update is called once per frame
     void Update() {

        if (!isDead)
        {

            moveDirection = Input.GetAxis("Horizontal");
            if (Mathf.Abs(moveDirection) > 0.05)
            {
                IsMoving = true;
            }
            else
            {
                IsMoving = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) == true && isGrounded == true)
            {
                isJumpPressed = true;
                animator.SetTrigger("DoJump");
                JumpSoundEffect.Play();

            }

            if (IsMoving)
            {
                if (isGrounded)
                {
                    if (!WalkSoundEffect.isPlaying)
                        WalkSoundEffect.Play();
                }
                else
                    WalkSoundEffect.Stop();
            }

            animator.SetBool("IsGrounded", isGrounded);
            animator.SetFloat("Speed", Mathf.Abs(moveDirection));
            WallSlide();
            WallJump();
            if (!isWallJumping)
            {
                Flip();
            }
        }
     }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            isGrounded = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f, whatIsGround);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    isGrounded = true;
                }
            }

            Vector3 calculatedMovement = new Vector3(0, 0, 0);
            float verticalVelocity = 0f;

            if (isGrounded == false)
            {
                verticalVelocity = rigidBody2D.velocity.y;
            }
            if (!_knockbacked)
            {
                calculatedMovement.x = movementSpeed * 100f * moveDirection * Time.fixedDeltaTime;
            }
            else
            {
                var lerpedXVelocity = Mathf.Lerp(rigidBody2D.velocity.x, 0f, Time.fixedDeltaTime);
                rigidBody2D.velocity = new Vector2(lerpedXVelocity, rigidBody2D.velocity.x);
            }

            calculatedMovement.y = verticalVelocity;
            Move(calculatedMovement, isJumpPressed);
            isJumpPressed = false;

            rigidBody2D.gravityScale = normalGravityScale;
        }
        else
        {
            rigidBody2D.velocity = Vector3.zero;
            rigidBody2D.gravityScale = 0f;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, 0.2f, whatIsGround);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if(IsWalled() && !IsGrounded() && moveDirection != 0f)
        {
            isWallSliding = true;
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, Mathf.Clamp(rigidBody2D.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }

        
    }

    public void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f && canWallJump && !IsGrounded())
        {
            isJumpPressed = true;
            animator.SetTrigger("DoJump");
            JumpSoundEffect.Play();
            

            isWallJumping = true;
            rigidBody2D.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }

    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Move(Vector3 moveDirection, bool isJumpPressed) {

        rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, moveDirection, ref velocity, smoothTime);

        if (isJumpPressed == true && isGrounded == true) {
            rigidBody2D.AddForce(new Vector2(0f, jumpForce * 100f));
        }
        if (moveDirection.x > 0f && isFacingRight == true) {
            Flip();
        } else if (moveDirection.x < 0f && isFacingRight == false) {
            Flip();
        }
        
    }

    private void Flip()
    {
        if (isFacingRight && moveDirection < 0f || !isFacingRight && moveDirection > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public bool IsFalling() {
        if (rigidBody2D.velocity.y < -1f) {
            return true;
        }
        return false;

    }

    public void ResetMovementSpeed()
    {
        movementSpeed = defaultMovementSpeed;
    }

    public void SetNewMovementSpeed(float multiplyBy)
    {
        movementSpeed *= multiplyBy;
    }

    public void ApplyMovement()
    {
        var newXVel =
            _knockbacked ? Mathf.Lerp(rigidBody2D.velocity.x, 0f, Time.deltaTime * 5) : _inputX * movementSpeed;

        rigidBody2D.velocity = new Vector2(newXVel, rigidBody2D.velocity.y);
    }
    internal void Knockback(Transform transform)
    {
        var dir = _center.position - transform.position;
        _knockbacked = true;
        rigidBody2D.velocity = dir.normalized * _knockbackVel;

        StartCoroutine(Unknockback());
    }

    private IEnumerator Unknockback()
    {
        yield return new WaitForSeconds(_knockbackTime);
        _knockbacked = false;
    }

    public void ResetJumpforce()
    {
        jumpForce = defaultJumpForce;
    }

    public void SetNewJumpForce(float multiplyBy)
    {
        jumpForce *= multiplyBy;
    }

    public void IsDead()
    {
        isDead = true;
    }

    public void IsAlive()
    {
        isDead = false;
    }
}
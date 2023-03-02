using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SlimeMovement : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    private float movementDirection = 1f;

    Rigidbody2D rigidBody2D;
    private Animator animator;
    public GameObject groundCheck;
    public Collider2D bodyCollider;
    public LayerMask Wall;

    bool isGrounded;

    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

    }

    private void Update()
    {
        animator.SetBool("IsAlive", isAlive);
        animator.SetBool("IsGrounded", isGrounded);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isGrounded == true && isAlive == true) { 
        Vector3 newPosition = gameObject.transform.position;
        newPosition.x += speed * Time.fixedDeltaTime * movementDirection;
        rigidBody2D.MovePosition(newPosition);
        }

       

        if(isAlive) { 
        CheckForGround();

            {

                if (isGrounded == false)
                {
                ChangeDirection();
                }
            }
           
        }
      
    }
    private void CheckForGround()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (bodyCollider.IsTouchingLayers(Wall))
        {
            ChangeDirection();
        }

    }


    private void ChangeDirection()
    {
        movementDirection = -movementDirection;
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x = movementDirection;
        gameObject.transform.localScale = newScale;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 moveInput;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // Only allow one axis movement at a time (Pac-Man style)
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = 0;
        }
        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f)
        {
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.x = 0;
        }
        else
        {
            moveInput = Vector2.zero;
        }

        // Update animator
        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);
    }


    private void FixedUpdate()
    {
        if (moveInput != Vector2.zero)
        {
            // Where we want to move next
            Vector2 targetPos = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;

            // Raycast in the move direction to detect walls
            RaycastHit2D hit = Physics2D.Raycast(
                rb.position,
                moveInput,
                moveSpeed * Time.fixedDeltaTime,
                LayerMask.GetMask("Wall")
            );

            if (hit.collider == null)
            {
                // Safe to move, no wall in the way
                rb.MovePosition(targetPos);
            }
            else
            {
                // Wall ahead — don't move this frame
                rb.MovePosition(rb.position);
            }
        }
    }


}

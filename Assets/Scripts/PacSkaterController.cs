using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PacSkaterController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // tiles per sec
    public float gridSize = 1f;

    [Header("References")]
    public Animator animator;
    public AudioSource moveAudio;
    public ParticleSystem dustParticles;

    [Header("Collision Feedback")]
    public AudioSource wallBumpEffect;

    private Vector2 inputDir;
    private Vector2 currentDir;
    private Vector3 startPos;
    private Vector3 targetPos;
    private float lerpTime = 0f;
    private bool isMoving = false;
    private float audioStopDelay = 0.15f; // small cooldown to prevent spam
    private float audioTimer = 0f;
    private bool isTouchingWall = false;

    private void Start()
    {
        currentDir = Vector2.right;
        inputDir = Vector2.zero;
        targetPos = transform.position;
    }

    private void Update()
    {
        HandleInput();

        if (!isMoving)
        {
            TryMove();
        }
        else
        {
            MoveLerp();
        }

        // handle delayed stop for audio/dust
        if (!isMoving)
        {
            audioTimer += Time.deltaTime;
            if (audioTimer >= audioStopDelay)
            {
                if (moveAudio && moveAudio.isPlaying) moveAudio.Stop();
                if (dustParticles && dustParticles.isPlaying) dustParticles.Stop();

                if (animator)
                {
                    animator.SetFloat("MoveX", 0);
                    animator.SetFloat("MoveY", 0);
                }
            }
        }
        else
        {
            audioTimer = 0f; // reset if moving again
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) inputDir = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S)) inputDir = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A)) inputDir = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D)) inputDir = Vector2.right;
    }

    void TryMove()
    {
        if (CanMoveTo(transform.position + (Vector3)inputDir * gridSize))
        {
            currentDir = inputDir;
            StartLerp(transform.position + (Vector3)inputDir * gridSize);
        }
        else if (CanMoveTo(transform.position + (Vector3)currentDir * gridSize))
        {
            StartLerp(transform.position + (Vector3)currentDir * gridSize);
        }
    }

    void StartLerp(Vector3 destination)
    {
        startPos = transform.position;
        targetPos = destination;
        lerpTime = 0f;
        isMoving = true;

        if (animator)
        {
            animator.SetFloat("MoveX", currentDir.x);
            animator.SetFloat("MoveY", currentDir.y);
        }

        if (moveAudio && !moveAudio.isPlaying)
        {
            moveAudio.loop = true;
            moveAudio.Play();
        }

        if (dustParticles && !dustParticles.isPlaying)
        {
            dustParticles.Play();
        }
    }

    void MoveLerp()
    {
        lerpTime += Time.deltaTime * moveSpeed;
        transform.position = Vector3.Lerp(startPos, targetPos, lerpTime);

        if (lerpTime >= 1f)
        {
            transform.position = targetPos;
            isMoving = false;
        }
    }

    bool CanMoveTo(Vector3 destination)
    {
        Collider2D hit = Physics2D.OverlapCircle(destination, 0.5f);

        if (hit != null && hit.CompareTag("Wall"))
        {
            if (!isTouchingWall)
            {
                isTouchingWall = true;

                // Play bump effect once on contact
                if (wallBumpEffect)
                {
                    AudioSource bump = Instantiate(wallBumpEffect, transform.position, Quaternion.identity);
                    bump.Play();
                    Destroy(bump.gameObject, bump.clip.length);
                }
            }

            return false;
        }
        else
        {
            // Reset when moving away from the wall
            isTouchingWall = false;
        }

        return true;
    }


}







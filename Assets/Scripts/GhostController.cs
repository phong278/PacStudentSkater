using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Vector2 startPosition;
    public Animator animator;
    public string normalAnim = "Ghost_Normal";

    // --- New Stuff ---
    public enum GhostState { Normal, Scared, Recovering, Dead }
    public GhostState currentState = GhostState.Normal;

    public AudioSource deathAudio; // optional (for death sfx)

    void Start()
    {
        startPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // PacStudent dies if ghost is normal
        if (currentState == GhostState.Normal)
        {
            GameManager.instance.PacStudentDeath();
        }
        // PacStudent eats ghost if scared or recovering
        else if (currentState == GhostState.Scared || currentState == GhostState.Recovering)
        {
            GameManager.instance.AddScore(300);
            ChangeState(GhostState.Dead);
            StartCoroutine(DeadRespawn());
        }
    }

    public void ChangeState(GhostState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GhostState.Normal:
                animator.Play(normalAnim);
                break;

            case GhostState.Scared:
                animator.Play("Ghost_Scared");
                break;

            case GhostState.Recovering:
                animator.Play("Ghost_Recovering");
                break;

            case GhostState.Dead:
                animator.Play("Ghost_Dead");
                if (deathAudio) deathAudio.Play();
                break;
        }
    }

    IEnumerator DeadRespawn()
    {
        yield return new WaitForSeconds(3f); // 3 sec invisible dead state
        transform.position = startPosition;
        ChangeState(GhostState.Normal);
    }

    public void ResetGhost()
    {
        transform.position = startPosition;
        ChangeState(GhostState.Normal);
    }
}

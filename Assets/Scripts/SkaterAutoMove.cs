using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SkaterAutoMove : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;

    private int currentWaypoint = 0;
    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>(); // grab Animator

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.loop = true;
            audioSource.Play();
        }

    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        Vector3 targetPos = waypoints[currentWaypoint].position;
        Vector3 direction = (targetPos - transform.position).normalized;

        // Move skater
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        //Send direction info to Animator
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);

        // If reached waypoint, go to next
        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
}







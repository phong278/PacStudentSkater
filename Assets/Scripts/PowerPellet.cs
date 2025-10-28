using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    public int scoreValue = 50;  // how much this power pellet is worth

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something collided with power pellet: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player ate a power pellet!");
            GameManager.instance.AddScore(scoreValue);
            Debug.Log("Power pellet collected! Score should go up.");

            // Trigger frightened (scared) mode for ghosts
            GameManager.instance.ActivatePowerPellet();

            Destroy(gameObject);
        }
    }

}


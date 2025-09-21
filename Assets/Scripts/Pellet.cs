using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int scoreValue = 10;  // how much this pellet is worth

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something collided with pellet: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player ate pellet!");
            GameManager.instance.AddScore(scoreValue);
            Debug.Log("Pellet collected! Score should go up.");

            Destroy(gameObject);
        }
    }



}






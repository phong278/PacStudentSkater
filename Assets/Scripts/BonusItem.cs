using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour
{

    public int scoreValue = 100;  // how much this BonusItem is worth

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something collided with BonusItem: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player ate BonusItem!");
            GameManager.instance.AddScore(scoreValue);
            Debug.Log("BonusItem collected! Score should go up.");

            Destroy(gameObject);
        }
    }



}


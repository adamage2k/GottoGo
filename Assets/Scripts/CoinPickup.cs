using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForPickup = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<GameSession>().IncreseScore(pointsForPickup);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position, 0.2f);
            Destroy(gameObject);
        }
    }

}

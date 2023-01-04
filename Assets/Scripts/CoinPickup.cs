using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int pointsForPickup = 10;
    [SerializeField] AudioClip pickUpSFX;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(pickUpSFX, Camera.main.transform.position, 0.3f);
            FindObjectOfType<GameSession>().IncreseScore(pointsForPickup);
            Destroy(gameObject);
        }
    }

}

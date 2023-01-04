using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rb;
    private float timer;

    SpriteRenderer mySpriteRenderer;

    [SerializeField] float bulletSpeed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mySpriteRenderer = FindObjectOfType<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2 (direction.x, direction.y).normalized * bulletSpeed;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            mySpriteRenderer.enabled = false;
        }
    }
}

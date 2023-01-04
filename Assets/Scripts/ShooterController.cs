using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float shootRange = 10f;
    

    private float timer;
    private GameObject player;
    private float distance;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    private void Update()
    {

        distance = Vector2.Distance(transform.position, ScenePersist.player.transform.position);

        if (distance < shootRange) 
        {
            timer += Time.deltaTime;

            if (timer > 1f)
            {
                timer = 0;
                Shoot();
            }
        }  
    }

    void Shoot() 
    {
        Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
    }
}

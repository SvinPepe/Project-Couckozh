using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

 


public class FuckYeahBehavior : EnemyMovement
{
    [SerializeField] private GameObject fuckPrefab;
    [SerializeField] private GameObject yeahPrefab;
    [SerializeField] private Transform spawn;
    
    
    private float bulletSpeed = 10f;
    private float t;
    private string state = "fuck";
    private bool agroed = false;
    
    private void Start()
    {
        InitializeEnemy();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        Movement();
        
        if (agroed && isBlocked)
        {
            agroed = false;
        }    
        
        if (!agroed && !isBlocked)
        {
            agroed = true;
            audioSource.Play();
        }  

        if (!isBlocked)
        {
            t += Time.deltaTime;

            if (state == "fuck" && t >= 3f)
            {
                GameObject newBullet = Instantiate(fuckPrefab, spawn.position, spawn.rotation);
                newBullet.GetComponentInChildren<Rigidbody>().velocity = spawn.forward * bulletSpeed;
                t = 0;
                state = "yeah";
            }

            if (state == "yeah" && t >= 0.7f)
            {
                GameObject newBullet = Instantiate(yeahPrefab, spawn.position, spawn.rotation);
                newBullet.GetComponentInChildren<Rigidbody>().velocity = spawn.forward * bulletSpeed;
                t = 0;
                state = "fuck";
            }
        }
    }
    
}

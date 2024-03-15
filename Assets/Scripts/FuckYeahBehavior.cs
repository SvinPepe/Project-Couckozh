using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;


public class FuckYeahBehavior : EnemyMovement
{
    [SerializeField] private GameObject fuckPrefab;
    [SerializeField] private GameObject yeahPrefab;
    [SerializeField] private Transform spawn;
    [SerializeField] private AudioClip[] seeAudioClips;
    [SerializeField] private AudioClip[] lostAudioClips;
    private AudioSource audioSource;

    
    private float bulletSpeed = 10f;
    private float t;
    private float seeyouCooldown = 11f;
    private float lostyouCooldown = 9f;
    private string state = "fuck";
    private bool agroed = false;
    
    
    private void Start()
    {
        InitializeEnemy();
        audioSource = GetComponent<AudioSource>();   
    }

    void FixedUpdate()
    {
        seeyouCooldown += Time.deltaTime;
        lostyouCooldown += Time.deltaTime;
        Movement();
        
        if (agroed && isBlocked)
        {
            agroed = false;
            if (lostyouCooldown > 10f)
            {
                audioSource.clip = lostAudioClips[UnityEngine.Random.Range(0, lostAudioClips.Length)];
                audioSource.Play();
                lostyouCooldown = 0;
            }
        }    
        
        if (!agroed && !isBlocked)
        {
            agroed = true;
            if (seeyouCooldown > 10f)
            {
                audioSource.clip = seeAudioClips[UnityEngine.Random.Range(0, seeAudioClips.Length)];
                audioSource.Play();
                seeyouCooldown = 0;
            }
        }  

        if (!isBlocked) // shooting
        {
            t += Time.deltaTime;

            if (state == "fuck" && t >= 3f)
            {
                spawn.LookAt(player.position);
                GameObject newBullet = Instantiate(fuckPrefab, spawn.position, spawn.rotation);
                newBullet.GetComponentInChildren<Rigidbody>().velocity = spawn.forward * bulletSpeed;
                t = 0;
                state = "yeah";
            }

            if (state == "yeah" && t >= 0.7f)
            {
                spawn.LookAt(player.position);
                GameObject newBullet = Instantiate(yeahPrefab, spawn.position, spawn.rotation);
                newBullet.GetComponentInChildren<Rigidbody>().velocity = spawn.forward * bulletSpeed;
                t = 0;
                state = "fuck";
            }
        }
    }
    
}

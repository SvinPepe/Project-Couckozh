using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject fuckPrefab;
    [SerializeField] private GameObject yeahPrefab;
    [SerializeField] private Transform spawn;
    [SerializeField] private float bulletSpeed = 7f;
    private AudioSource _audioSource;
    public GameObject player;
    private float t;
    private string state = "fuck";
    private bool isBlocked = true;
    private NavMeshAgent _navMeshAgent;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        //player = PlayerController.Player;
    }

    void Update()
    {

        RaycastHit hit;
        Ray ray = new Ray(spawn.position, player.transform.position - spawn.position);
        Physics.Raycast(ray, out hit);
        if (hit.collider != null){
            if (hit.collider.gameObject != player || hit.distance > 20f){
                Debug.Log("Путь к врагу преграждает объект: "+hit.collider.name);
                isBlocked = true;
                
            }   
            else
            {
                if (isBlocked)
                {
                    _audioSource.Play();
                    isBlocked = false;
                    Debug.Log("Попадаю во врага!!!");
                }
            }
            Debug.DrawLine(ray.origin, hit.point,Color.red);
        }



        if (isBlocked)
        {

            _navMeshAgent.destination = _navMeshAgent.gameObject.transform.position;
        }
        else
        {
            
            _navMeshAgent.destination = player.transform.position;
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

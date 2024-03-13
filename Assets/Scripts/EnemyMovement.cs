using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.Windows.Speech;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    
    public AudioSource audioSource;
    public Transform player;
    
    [SerializeField] public float detectionRadius = 20f;
    [SerializeField] public float range = 5f;
    [SerializeField] private float _speed = 3.5f;
    public bool isBlocked = true;
    
    public void InitializeEnemy()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            player = PlayerController.Player;
            Debug.Log(player);
        }
    }
    
    public void Movement()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, player.position - transform.position);
        Physics.Raycast(ray, out hit);
        if (hit.collider != null){
            if (hit.collider.gameObject != player.gameObject || hit.distance > detectionRadius) { 
                isBlocked = true;
                _navMeshAgent.speed = 0;
            }   
            else
            {
                if (isBlocked)
                {
                    isBlocked = false;
                }

                if (hit.distance < range)
                {
                    _navMeshAgent.speed = 0;
                }
                else
                {
                    _navMeshAgent.speed = _speed;
                }
                
            }
        }
        
        if (isBlocked)
        {
            _navMeshAgent.destination = _navMeshAgent.gameObject.transform.position;
        }
        else
        {
            _navMeshAgent.destination = player.position;
        }
        
    }
}

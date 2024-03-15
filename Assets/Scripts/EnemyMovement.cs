using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.Windows.Speech;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    
    
    public Transform player;
    
    [SerializeField] public float detectionRadius = 40f;
    [SerializeField] public float range = 15f;
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _inRangeSpeed = 0.5f;
    public bool isBlocked = true;
    
    public void InitializeEnemy()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            player = PlayerMovementTutorial.Player;
            Debug.Log(player);
        }
    }
    
    public void Movement()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, player.position - transform.position);
        Physics.Raycast(ray, out hit);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (hit.collider != null){
            if (!hit.collider.CompareTag("Player") || hit.distance > detectionRadius) { 
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
                    _navMeshAgent.speed = _inRangeSpeed;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yoyo : MonoBehaviour
{
    
    public Transform orientation;
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody>().AddForce(orientation.forward * 10f + Vector3.up * 10, ForceMode.Impulse);
        }
    }
}

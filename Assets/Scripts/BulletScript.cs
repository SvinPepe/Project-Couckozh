using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float _time;
    void Update()
    {
        _time += Time.deltaTime;

        if (_time > 5f)
        {
            Destroy(gameObject);
        }

    }
    
    
    
}

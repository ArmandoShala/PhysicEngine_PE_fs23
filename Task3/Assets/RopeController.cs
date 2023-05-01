using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _rigidbody;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // if the rigidbody with the tag "Wuerfel2" is within the trigger, the rope will be pulled up
        if (Math.Abs(GameObject.FindWithTag("Wuerfel2").GetComponent<Rigidbody>().position.x) < 1f)
        {
            _rigidbody.AddForce(0, 0.5f, 0, ForceMode.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider wheel;
    public float acceleration = 500f;
    public float breakingForce = 300f;

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;

    float adInput, wsInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        adInput = Input.GetAxis("Horizontal");
        wsInput = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        currentAcceleration = acceleration * Input.GetAxis("Vertical");
       
        if (wsInput > 0 || wsInput < 0) {
            wheel.motorTorque = currentAcceleration;
        }
        if (wsInput == 0)
        {
            currentBreakForce = breakingForce;
        }
        else {
            currentBreakForce = 0f;
        }
        wheel.brakeTorque = currentBreakForce;
    }
}

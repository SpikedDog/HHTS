using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoofyNewControls : MonoBehaviour
{
    public float currSpeed;
    public float maxSpeed;
    public float maxReverseSpeed = 30;
    public float maxSpeedR;
    public float loSpeedAng = 30;
    public float hiSpeedAng = 1;
    private float decelSpeed = 0.02f;
    private float topspeed = 150;
    public Rigidbody rb;
    //float speedMpH;

    private float verticalInput;
    private float gasInput;
    private float brakeInput;
    private float steeringInput;
    private float reverseInput;

    // Settings
    [SerializeField] private float motorForce, brakeForce, maxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider wheelFL, wheelFR;
    [SerializeField] private WheelCollider wheelRL, wheelRR;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        UpdateWheels();
        currSpeed = MathF.Round(returnCurrentMPH());
        float curSteerAng = Mathf.Lerp(loSpeedAng, hiSpeedAng, currSpeed / maxSpeed);
        curSteerAng *= Input.GetAxis("Horizontal");
        wheelFL.steerAngle = curSteerAng;
        wheelFR.steerAngle = curSteerAng;
    }

    private bool ShouldReverse()
    {
        return Vector3.Dot(rb.velocity, rb.transform.forward) < 1;
    }

    private void GetInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        steeringInput = Input.GetAxisRaw("Horizontal");
        if (verticalInput < 0)
        {
            if (ShouldReverse())
            {
                // reverse
                reverseInput = -verticalInput;
                gasInput = 0;
                brakeInput = 0;
            }
            else
            {
                // brake                 
                brakeInput = -verticalInput;
                gasInput = 0;
                reverseInput = 0;
            }
        }
        else
        {
            gasInput = verticalInput;
            brakeInput = 0;
            reverseInput = 0;
        }
    }

    private void SetWheels(float motorTorque, float brakeTorque)
    {
        wheelRL.motorTorque = motorTorque;
        wheelRR.motorTorque = motorTorque;
        wheelFR.brakeTorque = brakeTorque;
        wheelFL.brakeTorque = brakeTorque;
        wheelRL.brakeTorque = brakeTorque;
        wheelRR.brakeTorque = brakeTorque;
    }

    private void HandleMotor()
    {
        if (gasInput > 0 && returnCurrentMPH() < maxSpeed)
        {
            SetWheels(gasInput * motorForce, 0);
        }
        else if (brakeInput > 0)
        {
            SetWheels(0, brakeForce);
        }
        else if (reverseInput > 0 && returnCurrentMPH() < maxReverseSpeed)
        {
            SetWheels(-reverseInput * motorForce, 0);
        }
        else
        {
            Debug.Log($"Motor: wheel rpm {wheelRL.rpm} gas {gasInput} brake {brakeInput} reverse {reverseInput}");
            SetWheels(0, decelSpeed);
        }
    }

    public float returnCurrentMPH()
    {
        return rb.velocity.magnitude * 2.237f;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(wheelFL, frontLeftWheelTransform);
        UpdateSingleWheel(wheelFR, frontRightWheelTransform);
        UpdateSingleWheel(wheelRR, rearRightWheelTransform);
        UpdateSingleWheel(wheelRL, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}

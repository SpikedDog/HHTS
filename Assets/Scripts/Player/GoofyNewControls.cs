using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoofyNewControls : MonoBehaviour
{
    private float currentbreakForce;
    public float currSpeed;
    private bool isBreaking;
    public float maxSpeed;
    public float maxSpeedR;
    public float loSpeedAng = 30;
    public float hiSpeedAng = 1;
    private float decelSpeed = 30;
    private float topspeed = 150;
    public Rigidbody rb;
    //float speedMpH;

    public float gasInput;
    public float brakeInput;
    public float steeringInput;

    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;

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
        //The Speed Factor
        float speedF = Vector3.Dot(rb.velocity, transform.forward) * 2.237f;
        Debug.Log("vel =" + rb.velocity.magnitude);
        //currSpeed = 2 * 22 / 7 * wheelRL.radius * wheelRL.rpm * 60 / 1000;
        currSpeed = MathF.Round(currSpeed);
        float curSteerAng = Mathf.Lerp(loSpeedAng, hiSpeedAng, currSpeed);
        curSteerAng *= Input.GetAxis("Horizontal");
        wheelFL.steerAngle = curSteerAng;
        wheelFR.steerAngle = curSteerAng;

        wheelRL.brakeTorque = brakeInput * breakForce;
        wheelRR.brakeTorque = brakeInput * breakForce;

        if (Input.GetButton("Vertical") == false)
        {
            wheelRL.brakeTorque = decelSpeed;
            wheelRR.brakeTorque = decelSpeed;
        }
        //else
        //{
        //    wheelRL.brakeTorque = 0;
        //    wheelRR.brakeTorque = 0;
        //}
        //Debug.Log($"Motor torque L {wheelRL.rotationSpeed} R {wheelRR.rotationSpeed} Brake torque L {wheelRL.brakeTorque} R {wheelRR.brakeTorque}");
    }

    private void GetInput()
    {
            isBreaking = Input.GetKeyDown(KeyCode.Space);
            gasInput = Input.GetAxisRaw("Vertical");
            steeringInput = Input.GetAxisRaw("Horizontal");
            if (gasInput < 0)
            {
                brakeInput = Mathf.Abs(gasInput);
                //gasInput = 0;
            }
            else
            {
                brakeInput = 0;
            }
    }

    private void HandleMotor()
    {
        if (returnCurrentMPH() < maxSpeed && returnCurrentMPH() > -maxSpeedR) //(currSpeed < topspeed && currSpeed > -maxSpeedR)
        {
            wheelRL.motorTorque = gasInput * motorForce;
            wheelRR.motorTorque = gasInput * motorForce;
        }
        else
        {
            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
        }
        currentbreakForce = isBreaking ? breakForce : 0f;
        //ApplyBreaking();
    }

    public float returnCurrentMPH()
    {
        if (gasInput < 0)
        {
            return -rb.velocity.magnitude;
        }
        return rb.velocity.magnitude;
    }

    //private void ApplyBreaking()
    //{
    //    wheelFR.brakeTorque = currentbreakForce;
    //    wheelFL.brakeTorque = currentbreakForce;
    //    wheelRL.brakeTorque = currentbreakForce;
    //    wheelRR.brakeTorque = currentbreakForce;
    //}

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

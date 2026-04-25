using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static CarController;

public class GoofyNewControls : MonoBehaviour
{
    public float currSpeed;
    public float maxSpeed;
    public float maxReverseSpeed = 30;
    public float maxSpeedR;
    public float loSpeedAng = 30;
    public float hiSpeedAng = 1;
    private float decelSpeed = 0.02f;
    //private float topspeed = 150;
    public Rigidbody rb;
    //public GameObject smokePrefab;
    //public WheelParticles wheelParticles;
    //public WheelColliders colliders;
    //float speedMpH;

    private float verticalInput;
    public float gasInput;
    public float brakeInput;
    private float steeringInput;
    private float reverseInput;

    // Settings
    [SerializeField] public float motorForce, brakeForce, maxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider wheelFL, wheelFR;
    [SerializeField] private WheelCollider wheelRL, wheelRR;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    //private void Start()
    //{
    //    InstantiateSmoke();
    //}

    //void InstantiateSmoke()
    //{
    ////    wheelParticles.FRWheel = Instantiate(smokePrefab, colliders.FRWheel.transform.position - (Vector3.up * 0.6f) * colliders.FRWheel.radius, Quaternion.identity, colliders.FRWheel.transform)
    ////        .GetComponent<ParticleSystem>();
    ////    wheelParticles.FLWheel = Instantiate(smokePrefab, colliders.FLWheel.transform.position - (Vector3.up * 0.6f) * colliders.FLWheel.radius, Quaternion.identity, colliders.FLWheel.transform)
    ////.GetComponent<ParticleSystem>();
    //    wheelParticles.RLWheel = Instantiate(smokePrefab, colliders.RLWheel.transform.position - (Vector3.up * 0.6f) * colliders.RLWheel.radius, Quaternion.identity, colliders.RLWheel.transform)
    //        .GetComponent<ParticleSystem>();
    //    wheelParticles.RRWheel = Instantiate(smokePrefab, colliders.RRWheel.transform.position - (Vector3.up * 0.6f) * colliders.RRWheel.radius, Quaternion.identity, colliders.RRWheel.transform)
    //        .GetComponent<ParticleSystem>();
    //}

    //void CheckParticles()
    //{
    //    WheelHit[] wheelHits = new WheelHit[4];
    //    //colliders.FRWheel.GetGroundHit(out wheelHits[0]);
    //    //colliders.FLWheel.GetGroundHit(out wheelHits[1]);
    //    colliders.RLWheel.GetGroundHit(out wheelHits[2]);
    //    colliders.RRWheel.GetGroundHit(out wheelHits[3]);
    //    float slipAllowance = 0.325f;
    //    int index = 0;
    //    foreach (WheelHit wheel in wheelHits)
    //    {
    //        //Debug.Log("Check 1");
    //        if ((Mathf.Abs(wheel.sidewaysSlip) + Mathf.Abs(wheel.forwardSlip) > slipAllowance))
    //        {
    //            if (!CheckWheelParticle(index).isPlaying)
    //            {
    //                CheckWheelParticle(index).Play();
    //            }
    //            //Debug.Log("Check 2: "+ CheckWheelParticle(index)+" " + CheckWheelParticle(index).isEmitting);
    //        }
    //        else
    //        {
    //            if (CheckWheelParticle(index).isPlaying)
    //            {
    //                CheckWheelParticle(index).Stop();
    //            }
    //            //Debug.Log("Check 3");
    //        }
    //        index++;
    //    }
    //}

    //private ParticleSystem CheckWheelParticle(int index)
    //{
    //    //Debug.Log("Check 4"); 
    //    switch (index)
    //    {
    //        default:
    //            return wheelParticles.FRWheel;
    //        case 1:
    //            return wheelParticles.FLWheel;
    //        case 2:
    //            return wheelParticles.RLWheel;
    //        case 3:
    //            return wheelParticles.RRWheel;

    //    }
    //}

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        //UpdateWheels();
        currSpeed = MathF.Round(returnCurrentMPH());
        float curSteerAng = Mathf.Lerp(loSpeedAng, hiSpeedAng, currSpeed / maxSpeed);
        curSteerAng *= Input.GetAxis("Horizontal");
        wheelFL.steerAngle = curSteerAng;
        wheelFR.steerAngle = curSteerAng;
        //CheckParticles();

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
            //Debug.Log($"Motor: wheel rpm {wheelRL.rpm} gas {gasInput} brake {brakeInput} reverse {reverseInput}");
            SetWheels(0, decelSpeed);
        }
    }

    public float returnCurrentMPH()
    {
        return rb.velocity.magnitude * 2.237f;
    }

    public void StopMotor()
    {
        brakeInput = 1;
        gasInput = 0;
        reverseInput = 0;
        HandleMotor();
    }

    //private void UpdateWheels()
    //{
    //    UpdateSingleWheel(wheelFL, frontLeftWheelTransform);
    //    UpdateSingleWheel(wheelFR, frontRightWheelTransform);
    //    UpdateSingleWheel(wheelRR, rearRightWheelTransform);
    //    UpdateSingleWheel(wheelRL, rearLeftWheelTransform);
    //}

    //private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    //{
    //    Vector3 pos;
    //    Quaternion rot;
    //    wheelCollider.GetWorldPose(out pos, out rot);
    //    wheelTransform.rotation = rot;
    //    wheelTransform.position = pos;
    //}
}

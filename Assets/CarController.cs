using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;
    public WheelColliders colliders;
    public WheelMeshes wheelMeshes;
    public float gasInput;
    public float brakeInput;
    public float steeringInput;

    public float motorPower;
    public float brakePower;
    private float slipAngle;
    private float speed;
    public AnimationCurve steeringCurve;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = rb.velocity.magnitude;
        CheckInput();
        ApplySteering();
        ApplyBrake();
        ApplyWheel();
    }

    private void FixedUpdate()
    {
        ApplyMotor();
    }

    //Checking the inputs <--- MIGHT CHANGE
    void CheckInput()
    {
        gasInput = Input.GetAxisRaw("Vertical");
        steeringInput = Input.GetAxisRaw("Horizontal");
        slipAngle = Vector3.Angle(transform.forward, rb.velocity - transform.forward);
        if (slipAngle < 120f)
        {
            if (gasInput < 0)
            {
                brakeInput = Mathf.Abs(gasInput);
                gasInput = 0;
            }
        }
        else
        {
            brakeInput = 0;
        }
    }

    void ApplyBrake()
    {
        colliders.FWheel.brakeTorque = brakeInput * brakePower * 0.7f;
        colliders.RLWheel.brakeTorque = brakeInput * brakePower * 0.3f;
        colliders.RRWheel.brakeTorque = brakeInput * brakePower *0.3f;
    }

    //The engine and the application of power
    void ApplyMotor()
    {
        colliders.FWheel.motorTorque = motorPower * gasInput;
        colliders.RLWheel.motorTorque = motorPower * gasInput;
        colliders.RRWheel.motorTorque = motorPower * gasInput;
    }

    void ApplySteering()
    {
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        colliders.FWheel.steerAngle = steeringAngle;
    }

    //Constant updater for wheels
    private void ApplyWheel()
    {
        UpdateWheel(colliders.FWheel, wheelMeshes.FWheel);
        UpdateWheel(colliders.RLWheel, wheelMeshes.RLWheel);
        UpdateWheel(colliders.RRWheel, wheelMeshes.RRWheel);
    }

    //Updates the individual wheels
    private void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
    {
        Quaternion quat;
        Vector3 pos;
        coll.GetWorldPose(out pos, out quat);
        wheelMesh.transform.position = pos;
        wheelMesh.transform.rotation = quat;
    }

    //These two sections are for allocating the colliders and meshes
    [System.Serializable]
    public class WheelColliders
    {
        public WheelCollider FWheel;
        public WheelCollider RLWheel;
        public WheelCollider RRWheel;
    }
    
    [System.Serializable]
    public class WheelMeshes
    {
        public MeshRenderer FWheel;
        public MeshRenderer RLWheel;
        public MeshRenderer RRWheel;
    }
}

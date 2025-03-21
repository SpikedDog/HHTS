using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;
    public WheelColliders colliders;
    public WheelMeshes wheelMeshes;
    public WheelParticles wheelParticles;
    public float gasInput;
    public float brakeInput;
    public float steeringInput;
    public GameObject smokePrefab;

    public float motorPower;
    public float brakePower;
    private float slipAngle;
    private float speed;
    public AnimationCurve steeringCurve;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        InstantiateSmoke();
    }

    void InstantiateSmoke()
    {
        wheelParticles.FRWheel = Instantiate(smokePrefab, colliders.FRWheel.transform.position - (Vector3.up * 0.6f) * colliders.FRWheel.radius, Quaternion.identity, colliders.FRWheel.transform)
            .GetComponent<ParticleSystem>();
        wheelParticles.FLWheel = Instantiate(smokePrefab, colliders.FLWheel.transform.position - (Vector3.up * 0.6f) * colliders.FLWheel.radius, Quaternion.identity, colliders.FLWheel.transform)
    .GetComponent<ParticleSystem>();
        wheelParticles.RLWheel = Instantiate(smokePrefab, colliders.RLWheel.transform.position - (Vector3.up * 0.6f) * colliders.RLWheel.radius, Quaternion.identity, colliders.RLWheel.transform)
            .GetComponent<ParticleSystem>();
        wheelParticles.RRWheel = Instantiate(smokePrefab, colliders.RRWheel.transform.position - (Vector3.up * 0.6f) * colliders.RRWheel.radius, Quaternion.identity, colliders.RRWheel.transform)
            .GetComponent<ParticleSystem>();
    }

    void CheckParticles()
    {
        WheelHit[] wheelHits = new WheelHit[4];
        colliders.FRWheel.GetGroundHit(out wheelHits[0]);
        colliders.FLWheel.GetGroundHit(out wheelHits[1]);
        colliders.RLWheel.GetGroundHit(out wheelHits[2]);
        colliders.RRWheel.GetGroundHit(out wheelHits[3]);
        float slipAllowance = 0.325f;
        int index = 0;
        foreach (WheelHit wheel in wheelHits)
        {
            //Debug.Log("Check 1");
            if ((Mathf.Abs(wheel.sidewaysSlip) + Mathf.Abs(wheel.forwardSlip) > slipAllowance))
            {
                if (!CheckWheelParticle(index).isPlaying)
                {
                    CheckWheelParticle(index).Play();
                }
                //Debug.Log("Check 2: "+ CheckWheelParticle(index)+" " + CheckWheelParticle(index).isEmitting);
            }
            else
            {
                if (CheckWheelParticle(index).isPlaying)
                {
                    CheckWheelParticle(index).Stop();
                }
                //Debug.Log("Check 3");
            }
            index++;
        }
    }

    private ParticleSystem CheckWheelParticle(int index)
    {
        //Debug.Log("Check 4"); 
        switch(index)
        {
            default:
                return wheelParticles.FRWheel;
            case 1:
                return wheelParticles.FLWheel;
            case 2:
                return wheelParticles.RLWheel;
            case 3:
                return wheelParticles.RRWheel;
                
        }
    }

    // Update is called once per frame
    void Update()
    {
        speed = rb.velocity.magnitude;
        CheckInput();
        ApplySteering();
        ApplyBrake();
        ApplyWheel();
        CheckParticles();
        //Debug.Log(rb.velocity.magnitude);
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
        colliders.FRWheel.brakeTorque = brakeInput * brakePower * 0.5f;
        colliders.FLWheel.brakeTorque = brakeInput * brakePower * 0.5f;
        colliders.RLWheel.brakeTorque = brakeInput * brakePower * 0.3f;
        colliders.RRWheel.brakeTorque = brakeInput * brakePower * 0.3f;
    }

    //The engine and the application of power
    void ApplyMotor()
    {
        //colliders.FWheel.motorTorque = motorPower * gasInput;
        colliders.RLWheel.motorTorque = motorPower * gasInput;
        colliders.RRWheel.motorTorque = motorPower * gasInput;
    }

    void ApplySteering()
    {
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        colliders.FLWheel.steerAngle = steeringAngle;
        colliders.FRWheel.steerAngle = steeringAngle;
    }

    //Constant updater for wheels
    private void ApplyWheel()
    {
        UpdateWheel(colliders.FRWheel, wheelMeshes.FRWheel);
        UpdateWheel(colliders.FLWheel, wheelMeshes.FLWheel);
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
        public WheelCollider FRWheel;
        public WheelCollider FLWheel;
        public WheelCollider RLWheel;
        public WheelCollider RRWheel;
    }

    [System.Serializable]
    public class WheelMeshes
    {
        public MeshRenderer FRWheel;
        public MeshRenderer FLWheel;
        public MeshRenderer RLWheel;
        public MeshRenderer RRWheel;
    }
    
    [System.Serializable]
    public class WheelParticles
    {
        public ParticleSystem FRWheel;
        public ParticleSystem FLWheel;
        public ParticleSystem RLWheel;
        public ParticleSystem RRWheel;
    }
}

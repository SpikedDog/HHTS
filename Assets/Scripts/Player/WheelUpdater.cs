using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelUpdater : MonoBehaviour
{
    // Wheel Colliders
    [SerializeField] private WheelCollider wheelFL, wheelFR;
    [SerializeField] private WheelCollider wheelRL, wheelRR;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateWheels();
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

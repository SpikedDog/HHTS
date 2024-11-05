using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelColliders colliders;
    public WheelMeshes wheelMeshes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ApplyWheel();
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

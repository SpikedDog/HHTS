using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpinFunny : MonoBehaviour
{
    public float spinSpeed;

    public Vector3 rotaVector = new Vector3(45f, 45f, 45f);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotaVector * Time.deltaTime * spinSpeed);
    }
}

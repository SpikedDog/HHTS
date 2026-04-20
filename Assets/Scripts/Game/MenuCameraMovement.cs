using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _cameras;

    public void MtS()
    {
        _cameras[0].SetActive(false);
        _cameras[1].SetActive(true);
        _cameras[2].SetActive(false);
    }

    public void StM()
    {
        _cameras[0].SetActive(true);
        _cameras[1].SetActive(false);
        _cameras[2].SetActive(false);
    }

    public void MtC()
    {
        _cameras[0].SetActive(false);
        _cameras[1].SetActive(false);
        _cameras[2].SetActive(true);
    }

    public void CtM()
    {
        _cameras[0].SetActive(true);
        _cameras[1].SetActive(false);
        _cameras[2].SetActive(false);
    }
}

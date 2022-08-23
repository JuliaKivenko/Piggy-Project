using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera mainCamera;

    private void Start() => mainCamera = Camera.main;
    void Update()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.back);
        this.transform.Rotate(0, 180, 0);
    }
}

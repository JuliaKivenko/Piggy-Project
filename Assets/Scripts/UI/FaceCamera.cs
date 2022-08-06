using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] Transform lookAt;

    void Update()
    {
        if (lookAt)
        {
            transform.LookAt(2 * transform.position - lookAt.position);
        }
    }
}

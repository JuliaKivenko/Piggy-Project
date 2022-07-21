using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    public void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

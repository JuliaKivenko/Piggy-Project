using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    [SerializeField] private float maxRaycastDistance;
    [SerializeField] private LayerMask whatIsResource;
    [SerializeField] private Transform raycastStartPos;

    private RaycastHit hitInfo;

    void Update()
    {
        Physics.Raycast(raycastStartPos.position, raycastStartPos.forward, out hitInfo, maxRaycastDistance, whatIsResource);
        Debug.DrawRay(raycastStartPos.position, raycastStartPos.forward * maxRaycastDistance, Color.red);
        if (hitInfo.collider == null)
            return;
        if (hitInfo.collider.TryGetComponent<ResourceNode>(out ResourceNode resourceNode))
        {
            resourceNode.Gather();
        }

    }
}

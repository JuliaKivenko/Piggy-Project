using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetisable : MonoBehaviour
{
    private bool enableBehaviour = false;
    private float magnetThresholdDistance = 3;
    private float animationThresholdDistance = 0.3f;
    private float flySpeed = 20;
    private bool isMagnetised = true;
    private float dropAnimationSpeed;
    private Vector3 startPos;
    private Vector3 endPos;
    private float trajectoryHeight;
    private float cTime = 0;
    private bool isAnimationDone = false;

    private ResourceType type;

    public void Init(ResourceType resourceType, float animationSpeed, Vector3 start, Vector3 end, float height)
    {
        dropAnimationSpeed = animationSpeed;
        startPos = start;
        endPos = end;
        trajectoryHeight = height;
        enableBehaviour = true;
        type = resourceType;
    }

    void Update()
    {
        if (!enableBehaviour)
            return;

        AnimateDrop();

        if (!isAnimationDone)
            return;

        CheckDistanceFromPlayer();
    }

    private void AnimateDrop()
    {
        if (Vector3.Distance(transform.position, endPos) > animationThresholdDistance && !isAnimationDone)
        {
            cTime += Time.deltaTime * dropAnimationSpeed;
            // calculate straight-line lerp position:
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, cTime);
            // add a value to Y, using Sine to give a curved trajectory in the Y direction
            currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(cTime) * Mathf.PI);
            // finally assign the computed position to our gameObject:
            transform.position = currentPos;
        }
        if (Vector3.Distance(transform.position, endPos) <= animationThresholdDistance)
        {
            isAnimationDone = true;
        }
    }

    private void CheckDistanceFromPlayer()
    {
        Vector3 distanceToPlayer = PlayerController.instance.transform.position - transform.position;
        if (distanceToPlayer.magnitude < magnetThresholdDistance && isMagnetised == true)
        {
            isMagnetised = false;
            StartCoroutine(FlyToPlayer());
        }
    }

    IEnumerator FlyToPlayer()
    {
        while (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > animationThresholdDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, flySpeed * Time.deltaTime);
            yield return null;
        }

        //Add this resources to the resource manager
        ResourceManager.ChangeResourceAmount(type, 1);

        Destroy(gameObject);
    }

}

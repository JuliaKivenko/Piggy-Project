using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private int maxDropAmount;
    [SerializeField] private float dropRadius;
    [SerializeField] private float trajectoryHeight;
    [SerializeField] private float dropAnimationSpeed;
    [SerializeField] private float replenishTime;
    [SerializeField] private GameObject resourceVisual;
    [SerializeField] private Component harvestingMethod;

    private IHarvestable harvestable;
    private bool isReplenished = true;


    private void Start()
    {
        harvestable = GetComponent<IHarvestable>();
    }

    public void Gather()
    {
        if (!isReplenished)
            return;

        //Calculate a random number of resource to drop
        int amountToDrop = Random.Range(1, maxDropAmount + 1);

        //Before spawning, get the height of the visual to spawn it at the correct height
        Renderer r = resourceVisual.GetComponent<Renderer>();
        float height = r.bounds.extents.y;

        //Instantiate as many resources as amount dropped in a random radius around the node
        for (int i = 0; i < amountToDrop; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * dropRadius + new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 spawnPosition = new Vector3(randomPosition.x, height, randomPosition.z);
            GameObject resourceInstance = Instantiate(resourceVisual, transform.position, Quaternion.identity);
            //StartCoroutine(AnimateResourceDrop(resourceInstance, transform.position, spawnPosition));
            Magnetisable magnetisable = resourceInstance.AddComponent<Magnetisable>();
            magnetisable.Init(dropAnimationSpeed, transform.position, spawnPosition, trajectoryHeight);
        }

        //Add this resources to the resource manager
        ResourceManager.ChangeResourceAmount(resourceType, amountToDrop);

        //mark resource as depleted, start timer on when it can be replanished
        isReplenished = false;
        resourceVisual.SetActive(false);
        Invoke(nameof(ReplenishResource), replenishTime);
    }

    IEnumerator AnimateResourceDrop(GameObject objectToAnimate, Vector3 startPos, Vector3 endPos)
    {
        // calculate current time within our lerping time range
        float cTime = 0;

        while (transform.position != endPos)
        {
            cTime += Time.deltaTime * dropAnimationSpeed;
            // calculate straight-line lerp position:
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, cTime);
            // add a value to Y, using Sine to give a curved trajectory in the Y direction
            currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(cTime) * Mathf.PI);
            // finally assign the computed position to our gameObject:
            objectToAnimate.transform.position = currentPos;
            yield return new WaitForEndOfFrame();
        }

    }

    private void ReplenishResource()
    {
        resourceVisual.SetActive(true);
        isReplenished = true;
    }

    public bool IsReplenished() => isReplenished;
}

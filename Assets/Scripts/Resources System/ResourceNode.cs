using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private int maxDropAmount;
    [SerializeField] private float replenishTime;
    [SerializeField] private GameObject resourceVisual;

    private bool isReplenished = true;

    public void Gather()
    {
        if (!isReplenished)
            return;

        //Calculate a random number of resource to drop
        int amountToDrop = Random.Range(1, maxDropAmount + 1);

        //Add thos resources to the resource manager
        ResourceManager.ChangeResourceAmount(resourceType, amountToDrop);

        //mark resource as depleted, start timer on when it can be replanished
        isReplenished = false;
        resourceVisual.SetActive(false);
        Invoke(nameof(ReplenishResource), replenishTime);
    }

    private void ReplenishResource()
    {
        resourceVisual.SetActive(true);
        isReplenished = true;
    }
}

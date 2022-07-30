using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private int maxDropAmount;
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

        //Add this resources to the resource manager
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

    public bool IsReplenished() => isReplenished;
}

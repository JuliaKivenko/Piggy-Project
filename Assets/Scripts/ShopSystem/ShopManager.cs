using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShopManager
{
    public static void SellResources(ResourceType resourceType)
    {
        if (resourceType == ResourceType.Gold)
            return;

        int amountSold = ResourceManager.resources[resourceType];
        ResourceManager.ChangeResourceAmount(resourceType, -amountSold);
        ResourceManager.ChangeResourceAmount(ResourceType.Gold, amountSold);
    }
}

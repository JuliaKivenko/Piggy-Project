using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShopManager
{
    public static void SellResources(ResourceType resourceType, int amountToSell, int exchangeRate)
    {
        if (resourceType == ResourceType.Gold)
            return;

        ResourceManager.ChangeResourceAmount(resourceType, -amountToSell);
        ResourceManager.ChangeResourceAmount(ResourceType.Gold, amountToSell / exchangeRate);
    }
}

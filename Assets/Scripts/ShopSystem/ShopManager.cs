using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShopManager
{
    public delegate void BuyUpgradeAction(Upgrade upgrade);
    public static event BuyUpgradeAction onBuyUpgrade;

    public static void ExchangeResources(ResourceType resourceType, int amountToSell, int exchangeRate)
    {
        if (resourceType == ResourceType.Gold)
            return;

        ResourceManager.ChangeResourceAmount(resourceType, -amountToSell);
        ResourceManager.ChangeResourceAmount(ResourceType.Gold, amountToSell / exchangeRate);
    }

    public static void BuyUpgrade(Upgrade upgrade)
    {
        //Subtract price from the overal points player has
        ResourceManager.ChangeResourceAmount(ResourceType.Gold, -upgrade.price);

        //Invoke OnBuyUpgrade event
        if (onBuyUpgrade != null)
            onBuyUpgrade.Invoke(upgrade);

        //Multiply the corresponding stat by the upgrade multiplier. Upgrade type which correlates with specific stats? Players stats script which has all the components hooked up to specific stat type? 
        PlayerController.instance.playerStats.ModifyPlayerStat(upgrade.type, upgrade.statMultiplier);

        //Increase Upgrade Level
        upgrade.level += 1;

        //Increase price of the next upgrade by multiplier
        upgrade.price = (int)Mathf.Round(upgrade.price * upgrade.priceMultiplier);
    }
}

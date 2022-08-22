using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Upgrade
{
    public string name;
    [TextArea] public string description;
    public StatTypes type;
    public Image visual;
    public int price;
    public float priceMultiplier;
    public int level;
    public float statMultiplier;

    public float GetFutureStatValue(float currentStat) => currentStat * statMultiplier;

}

[CreateAssetMenu(fileName = "New Upgrade Settings", menuName = "Upgrade Settings")]
public class UpgradeSettings : ScriptableObject
{
    public Upgrade[] upgrades;

    public Upgrade GetUpgrade(StatTypes upgradeType)
    {
        foreach (var upgrade in upgrades)
        {
            if (upgrade.type == upgradeType)
                return upgrade;
        }

        return null;
    }

}

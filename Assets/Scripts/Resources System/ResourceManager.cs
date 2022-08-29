using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Truffle,
    Acorn,
    Gold,
    Melon,
    Apple,
}

public static class ResourceManager
{
    public delegate void OnResourceChange();
    public static event OnResourceChange onResourceChange;

    public static Dictionary<ResourceType, int> resources;

    public static void Init()
    {
        resources = new Dictionary<ResourceType, int>();

        foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
        {
            resources.Add(resourceType, 0);
        }
    }

    public static void ChangeResourceAmount(ResourceType type, int amountToAdd)
    {

        if (resources[type] + amountToAdd < 0)
        {
            if (resources[type] == 0)
                return;

            resources[type] -= (resources[type] + amountToAdd) - amountToAdd;
        }
        else
        {
            resources[type] += amountToAdd;
        }
        onResourceChange?.Invoke();
    }

    public static int GetResourceAmount(ResourceType type) => resources[type];
}

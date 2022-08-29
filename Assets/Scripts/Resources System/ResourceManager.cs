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
        resources[type] += amountToAdd;
        onResourceChange?.Invoke();
    }

    public static int GetResourceAmount(ResourceType type) => resources[type];
}

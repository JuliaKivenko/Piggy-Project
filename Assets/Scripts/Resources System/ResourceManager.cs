using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Truffle,
    Acorn,
}

public static class ResourceManager
{
    public delegate void OnResourceChange();
    public static event OnResourceChange onResourceChange;

    public static Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>()
    {
        { ResourceType.Truffle, 0},
    };

    public static void ChangeResourceAmount(ResourceType type, int amountToAdd)
    {
        resources[type] += amountToAdd;
        onResourceChange?.Invoke();
    }

    public static int GetResourceAmount(ResourceType type) => resources[type];
}

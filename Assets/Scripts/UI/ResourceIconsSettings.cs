using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class ResourceIconPair
{
    public ResourceType resourceType;
    public string iconName;
}

[CreateAssetMenu(fileName = "ResourceIconsSettings", menuName = "ScriptableObjects/UISettings", order = 1)]
public class ResourceIconsSettings : ScriptableObject
{
    public ResourceIconPair[] resourceIcons;

    public string GetIconString(ResourceType type)
    {
        foreach (var item in resourceIcons)
        {
            if (item.resourceType == type)
                return item.iconName;
        }
        return string.Empty;
    }
}



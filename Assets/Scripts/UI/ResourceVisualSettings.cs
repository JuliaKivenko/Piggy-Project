using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

[System.Serializable]
public class ResourceVisualSetting
{
    public ResourceType resourceType;
    public string iconName;
    public Mesh mesh;
    public Material material;

    public T GetAttribute<T>(string _name)
    {
        T retval = (T)typeof(ResourceVisualSetting).GetField(_name).GetValue(this);
        if (retval == null) { return default(T); };
        return retval;
    }
}

[CreateAssetMenu(fileName = "ResourceVisualSettings", menuName = "ScriptableObjects/ResourceVisualSettings", order = 1)]
public class ResourceVisualSettings : ScriptableObject
{
    public ResourceVisualSetting[] resourceSettings;

    public T GetResourceVisualParameter<T>(ResourceType type, string parameterName)
    {
        foreach (var item in resourceSettings)
        {
            if (item.resourceType == type)
                return item.GetAttribute<T>(parameterName);
        }

        return default(T);
    }

}



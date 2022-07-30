using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumpable : MonoBehaviour, IHarvestable
{
    [SerializeField] ResourceNode resourceNode;
    public void Harvest()
    {
        if (!PlayerController.instance.isDashing)
            return;
        resourceNode.Gather();
    }
}

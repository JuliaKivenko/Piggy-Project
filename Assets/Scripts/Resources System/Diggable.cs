using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diggable : MonoBehaviour, IHarvestable
{
    [SerializeField] private ResourceNode resourceNode;
    [SerializeField] private float diggingTime;

    private float currentDiggingTime = 0;

    public void Harvest()
    {
        if (!resourceNode.IsReplenished())
            return;
        currentDiggingTime += PlayerController.instance.playerStats.diggingSpeed * Time.deltaTime;
        if (currentDiggingTime >= diggingTime)
        {
            resourceNode.Gather();
            currentDiggingTime = 0;
        }
    }
}

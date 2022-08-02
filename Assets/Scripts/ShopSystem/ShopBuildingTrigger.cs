using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuildingTrigger : MonoBehaviour
{
    [SerializeField] LayerMask whatIsPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShopManager.SellResources(ResourceType.Truffle);
            ShopManager.SellResources(ResourceType.Acorn);
        }

    }
}

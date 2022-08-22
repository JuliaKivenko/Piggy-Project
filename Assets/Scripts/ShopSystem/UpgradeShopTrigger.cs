using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShopTrigger : MonoBehaviour
{
    public delegate void PlayerEnter();
    public static event PlayerEnter onPlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onPlayerEnter?.Invoke();
        }
    }

}

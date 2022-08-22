using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShopUI : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    [SerializeField] UpgradeGroupDisplay[] upgradeDisplaysArray;

    private bool isFirstStartup = true;
    private void Awake()
    {
        UpgradeShopTrigger.onPlayerEnter += OpenShop;
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        UpgradeShopTrigger.onPlayerEnter -= OpenShop;
    }
    private void OpenShop()
    {
        HUD.SetActive(false);
        gameObject.SetActive(true);
        if (isFirstStartup)
        {
            isFirstStartup = false;
            return;
        }
        else
        {
            foreach (var upgradeDisplay in upgradeDisplaysArray)
            {
                upgradeDisplay.UpdateDisplay();
            }

        }

    }
    public void ExitShop()
    {
        gameObject.SetActive(false);
        HUD.SetActive(true);
    }
}

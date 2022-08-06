using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuildingTrigger : MonoBehaviour
{
    [SerializeField] private ResourceType resourceToBuy;
    [SerializeField] private ResourceType resourceToSell;
    [SerializeField] private int exchangeRate;
    [SerializeField] private ShopOptionsDisplay shopUI;
    [SerializeField] private float yOffset = 5f;
    private Vector3 uiPos;
    bool isShopActive = false;

    private void Start()
    {
        shopUI.gameObject.SetActive(false);
        uiPos = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
    }
    private void OnEnable()
    {
        ShopOptionsDisplay.onIconClick += ExchangeResources;
    }
    private void OnDisable()
    {
        ShopOptionsDisplay.onIconClick -= ExchangeResources;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isShopActive = true;
            shopUI.transform.position = uiPos;
            shopUI.DisplayShopOptions(exchangeRate,
                    ResourceManager.GetResourceAmount(resourceToSell),
                    ResourceManager.GetResourceAmount(resourceToSell) / exchangeRate,
                    resourceToSell,
                    resourceToBuy);
            shopUI.gameObject.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isShopActive = false;
            shopUI.gameObject.SetActive(false);
        }
    }

    private void ExchangeResources(bool singlePurchase)
    {
        if (!isShopActive)
            return;
        Debug.Log("trigger");
        int amountToSell = singlePurchase ? exchangeRate : ResourceManager.GetResourceAmount(resourceToSell);
        ShopManager.SellResources(resourceToSell, amountToSell, exchangeRate);
    }
}

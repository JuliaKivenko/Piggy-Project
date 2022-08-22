using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuildingTrigger : MonoBehaviour
{
    [SerializeField] private ResourceType resourceToBuy;
    [SerializeField] private ResourceType resourceToSell;
    [SerializeField] private int exchangeRate;
    private ShopOptionsDisplay shopUI;
    [SerializeField] private float yOffset = 5f;
    private Vector3 uiPos;
    bool isShopActive = false;

    public delegate void ExchangeResourceAction(ResourceType resourceType);
    public static event ExchangeResourceAction onExchangeResources;

    private void Start()
    {
        //TODO: get rid of this nasty singleton approach, make this event based like with unlock. Maybe turn that into a component?
        shopUI = ShopOptionsDisplay.instance;
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
            shopUI.DisplayShopOptions(resourceToSell, resourceToBuy, exchangeRate);
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
        int amountToSell = singlePurchase ? exchangeRate : exchangeRate * (ResourceManager.GetResourceAmount(resourceToSell) / exchangeRate);
        ShopManager.ExchangeResources(resourceToSell, amountToSell, exchangeRate);
        shopUI.DisplayShopOptions(resourceToSell, resourceToBuy, exchangeRate);
        onExchangeResources?.Invoke(resourceToSell);
    }
}

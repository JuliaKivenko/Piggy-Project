using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopOptionsDisplay : MonoBehaviour
{

    public static ShopOptionsDisplay instance;
    [SerializeField] private RectTransform iconForSinglePurchase;
    [SerializeField] private RectTransform singleIconInitialPosition;
    [SerializeField] private Image iconForSinglePurchaseImage;
    [SerializeField] private RectTransform iconForMultiplePurchase;
    [SerializeField] private RectTransform centered;
    [SerializeField] private TextMeshProUGUI exchangeTextForOne;
    [SerializeField] private TextMeshProUGUI amountTextForOne;
    [SerializeField] private TextMeshProUGUI exchangeTextForSeveral;
    [SerializeField] private TextMeshProUGUI amountTextForSeveral;
    [SerializeField] private ResourceVisualSettings resourceVisualSettings;

    private bool isButtonActive = true;


    public delegate void OnIconClick(bool singlePurchase);
    public static event OnIconClick onIconClick;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }
    public void DisplayShopOptions(ResourceType resourceTypeToSell, ResourceType resourceTypeToBuy, int exchangeRate)
    {
        int amountOfResourceToSell = exchangeRate * (ResourceManager.GetResourceAmount(resourceTypeToSell) / exchangeRate);
        if (amountOfResourceToSell <= exchangeRate)
        {
            //if i have exactly the amount of resource to sell y to buy 1 of resource x, only leave the option for tingular purchase
            iconForMultiplePurchase.gameObject.SetActive(false);
            iconForSinglePurchase.position = centered.position;
            iconForSinglePurchaseImage.color = Color.white;
            isButtonActive = true;
            if (amountOfResourceToSell < exchangeRate)
            {
                //if i don't have enough to buy even a single resource x, grey the option out and disable the button
                iconForSinglePurchaseImage.color = Color.grey;
                isButtonActive = false;
            }
        }
        else
        {
            //show both options active in other cases
            iconForMultiplePurchase.gameObject.SetActive(true);
            iconForSinglePurchase.position = singleIconInitialPosition.position;
            iconForSinglePurchaseImage.color = Color.white;
            isButtonActive = true;
        }

        //Get strings for icons
        string iconForSoldResource = resourceVisualSettings.GetResourceVisualParameter<string>(resourceTypeToSell, "iconName");
        string iconForBoughtResource = resourceVisualSettings.GetResourceVisualParameter<string>(resourceTypeToBuy, "iconName");

        //set texts
        exchangeTextForOne.text = $"{exchangeRate.ToString()} {iconForSoldResource}";
        amountTextForOne.text = $"1 {iconForBoughtResource}";
        exchangeTextForSeveral.text = $"{amountOfResourceToSell.ToString()} {iconForSoldResource}";
        amountTextForSeveral.text = $"{(ResourceManager.GetResourceAmount(resourceTypeToSell) / exchangeRate).ToString()} {iconForBoughtResource}";
    }

    public void ClickToPurchase(bool singlePurchase)
    {
        if (!isButtonActive)
            return;
        onIconClick?.Invoke(singlePurchase);
    }
}

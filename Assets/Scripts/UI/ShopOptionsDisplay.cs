using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopOptionsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI exchangeTextForOne;
    [SerializeField] private TextMeshProUGUI amountTextForOne;
    [SerializeField] private TextMeshProUGUI exchangeTextForSeveral;
    [SerializeField] private TextMeshProUGUI amountTextForSeveral;
    [SerializeField] private ResourceIconsSettings resourceIconsSettings;

    public delegate void OnIconClick(bool singlePurchase);
    public static event OnIconClick onIconClick;

    public void DisplayShopOptions(int resourceToSellForSingleAmount, int resourceToSellForSomeAmount, int amountToBuy, ResourceType resourceTypeToSell, ResourceType resourceTypeToBuy)
    {
        string iconForSoldResource = resourceIconsSettings.GetIconString(resourceTypeToSell);
        string iconForBoughtResource = resourceIconsSettings.GetIconString(resourceTypeToBuy);

        exchangeTextForOne.text = $"{resourceToSellForSingleAmount.ToString()} {iconForSoldResource}";
        amountTextForOne.text = $"1 {iconForBoughtResource}";
        exchangeTextForSeveral.text = $"{resourceToSellForSomeAmount.ToString()} {iconForSoldResource}";
        amountTextForSeveral.text = $"{amountToBuy.ToString()} {iconForBoughtResource}";
    }

    public void ClickToPurchase(bool singlePurchase)
    {
        onIconClick?.Invoke(singlePurchase);
    }
}

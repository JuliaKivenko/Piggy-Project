using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeGroupDisplay : MonoBehaviour
{
    [Header("UpgradeType")]
    [SerializeField] StatTypes upgradeType;
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI upgradeNameLvl;
    [SerializeField] Image upgradeVisual;
    [SerializeField] TextMeshProUGUI upgradeDescription;
    [SerializeField] Image buttonVisual;
    [SerializeField] TextMeshProUGUI upgradePrice;
    [SerializeField] TextMeshProUGUI upgradeStatValueChange;
    private UpgradeSettings upgradeSettings;
    private Upgrade upgrade;

    private void Start()
    {
        upgradeSettings = GameManager.instance.GetUpgradeSettingsInstance();
        upgrade = upgradeSettings.GetUpgrade(upgradeType);
        UpdateDisplay();
    }

    public void BuyUpgrade()
    {
        ShopManager.BuyUpgrade(upgrade);
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        upgradeNameLvl.text = $"{upgrade.name} Lvl.{upgrade.level}";
        upgradeVisual = upgrade.visual;
        upgradeDescription.text = upgrade.description;
        upgradePrice.text = $"<sprite=2>{upgrade.price}";
        buttonVisual.color = ResourceManager.GetResourceAmount(ResourceType.Gold) < upgrade.price ? Color.gray : Color.white;
        float currentStat = PlayerController.instance.playerStats.GetPlayerStatValue(upgradeType);
        upgradeStatValueChange.text = $"{currentStat} -> {upgrade.GetFutureStatValue(currentStat)}";
    }


}

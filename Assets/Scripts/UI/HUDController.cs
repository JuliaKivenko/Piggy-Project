using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI speedText;

    [Header("Resources")]
    [SerializeField] private List<TextMeshProUGUI> resourcesTexts;

    private void Update()
    {
        staminaText.text = $"Stamina: {PlayerController.instance.getCurrentStamina}";
        speedText.text = $"Speed: {PlayerController.instance.getCurrentSpeed}";

        resourcesTexts[0].text = $"Truffles: {ResourceManager.GetResourceAmount(ResourceType.Truffle)}";
    }
}

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

    private void Start() => UpdateResourceTexts();
    private void OnEnable() => ResourceManager.onResourceChange += UpdateResourceTexts;
    private void OnDisable() => ResourceManager.onResourceChange -= UpdateResourceTexts;

    private void Update()
    {
        staminaText.text = $"Stamina: {PlayerController.instance.getCurrentStamina}";
        speedText.text = $"Speed: {PlayerController.instance.getCurrentSpeed}";
    }

    private void UpdateResourceTexts()
    {
        int y = 0;
        foreach (var item in ResourceManager.resources)
        {
            if (y >= resourcesTexts.Count)
                break;
            resourcesTexts[y].text = $"{item.Key}: {item.Value}";
            y++;
        }

    }
}

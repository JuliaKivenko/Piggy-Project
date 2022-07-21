using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private PlayerController playerController;

    private void Update()
    {
        staminaText.text = $"Stamina: {playerController.getCurrentStamina}";
        speedText.text = $"Speed: {playerController.getCurrentSpeed}";
    }
}

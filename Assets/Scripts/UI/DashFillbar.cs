using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashFillbar : MonoBehaviour
{
    [SerializeField] Image fillbar;
    void Update()
    {
        fillbar.fillAmount = PlayerController.instance.StaminaReplenishProgress;
    }
}

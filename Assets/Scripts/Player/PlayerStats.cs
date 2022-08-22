using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatTypes
{
    DashMultiplier,
    Stamina,
    StaminaRechargeSpeed,
    DiggingSpeed,
}

public class PlayerStats : MonoBehaviour
{
    public float dashMultiplier { get { return _dashMultiplier; } set { } }
    [SerializeField] private float _dashMultiplier;

    public float stamina { get { return _stamina; } set { } }
    [SerializeField] private float _stamina;

    public float staminaRechargeSpeed { get { return _staminaRechargeSpeed; } set { } }
    [SerializeField] private float _staminaRechargeSpeed;

    public float diggingSpeed { get { return _diggingSpeed; } set { } }
    [SerializeField] private float _diggingSpeed;

    public float GetPlayerStatValue(StatTypes playerStat)
    {
        switch (playerStat)
        {
            case StatTypes.DashMultiplier:
                return _dashMultiplier;
            case StatTypes.Stamina:
                return _stamina;
            case StatTypes.StaminaRechargeSpeed:
                return _staminaRechargeSpeed;
            case StatTypes.DiggingSpeed:
                return _diggingSpeed;
            default:
                return 0;
        }
    }
    public void ModifyPlayerStat(StatTypes playerStat, float multiplier)
    {
        switch (playerStat)
        {
            case StatTypes.DashMultiplier:
                _dashMultiplier *= multiplier;
                break;
            case StatTypes.Stamina:
                _stamina *= multiplier;
                break;
            case StatTypes.StaminaRechargeSpeed:
                _staminaRechargeSpeed *= multiplier;
                break;
            case StatTypes.DiggingSpeed:
                _diggingSpeed *= multiplier;
                break;
            default:
                break;
        }
    }
}

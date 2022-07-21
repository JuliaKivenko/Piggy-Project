using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float dashMultiplier { get { return _dashMultiplier; } set { } }
    [SerializeField] private float _dashMultiplier;

    public float stamina { get { return _stamina; } set { } }
    [SerializeField] private float _stamina;

    public float staminaRechargeSpeed { get { return _staminaRechargeSpeed; } set { } }
    [SerializeField] private float _staminaRechargeSpeed;
}

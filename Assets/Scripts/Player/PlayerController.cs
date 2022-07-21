using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] PlayerVisualRotator playerRotator;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float staminaDepletionRate;

    private PlayerControllerAction playerControllerAcion;
    private float dashSpeed;
    private float tiredSpeed;
    private float input;
    private Coroutine replenishStaminaRoutine;
    [Header("Debug")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private float currentStamina;



    private void Awake()
    {
        playerControllerAcion = new PlayerControllerAction();

        //Calculate and set up different walk and run speeds
        dashSpeed = walkSpeed * playerStats.dashMultiplier;
        tiredSpeed = walkSpeed / playerStats.dashMultiplier;
        currentSpeed = walkSpeed;

        //Set up current stamina
        currentStamina = playerStats.stamina;

    }

    private void OnEnable()
    {
        playerControllerAcion.Enable();
    }

    private void OnDisable()
    {
        playerControllerAcion.Disable();
    }

    public void SetScreenInput(float input) => this.input = input;

    private void Update()
    {
        Move();
        //Dash should only be active when player is moving
#if UNITY_EDITOR
        Dash(playerControllerAcion.Player.Dash.ReadValue<float>());
#endif
#if PLATFORM_ANDROID
        Dash(input);
#endif
    }

    private void Move()
    {
        //Read input from joystick
        Vector2 movemementInput = playerControllerAcion.Player.Move.ReadValue<Vector2>();

        //Rotate the read value by 45 degrees clockwise
        Vector3 toConvert = new Vector3(movemementInput.x, 0, movemementInput.y);
        Vector3 move = IsoVectorConvert(toConvert);

        //Rotate the visual in the direction player is walking
        playerRotator.Rotate(move);

        //Move character
        characterController.Move(move * Time.deltaTime * currentSpeed);
    }

    private Vector3 IsoVectorConvert(Vector3 vector)
    {
        Quaternion rotaton = Quaternion.Euler(0, -135, 0);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotaton);
        Vector3 result = isoMatrix.MultiplyPoint3x4(vector);
        return result;
    }

    public void Dash(float press)
    {
        if (press > 0f)
        {
            //If player is trying to dash, but does not have stamina, set speed to slow
            if (currentStamina <= 0f)
            {
                currentSpeed = tiredSpeed;
                return;
            }

            //If stamina is currently being replenished, stop that. Set speed to dashing speed, and subtract from stamina while player is dashing
            if (replenishStaminaRoutine != null)
                StopCoroutine(replenishStaminaRoutine);
            currentSpeed = dashSpeed;
            currentStamina -= staminaDepletionRate * Time.deltaTime;
        }

        //Set to normal speed if player is not holding dash, start replanishing stamina
        bool coroutineStarted = false;
        if (press <= 0f)
        {
            currentSpeed = walkSpeed;
            if (!coroutineStarted)
            {
                replenishStaminaRoutine = StartCoroutine(StaminaReplenish());
                coroutineStarted = true;
            }
        }
    }

    private IEnumerator StaminaReplenish()
    {
        while (currentStamina < playerStats.stamina)
        {
            currentStamina += playerStats.staminaRechargeSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        currentStamina = playerStats.stamina;
    }

    //For UI
    public float StaminaReplenishProgress => playerStats.stamina / currentStamina;

    //For debug
    public string getCurrentSpeed => currentSpeed.ToString();
    public string getCurrentStamina => currentStamina.ToString();



}

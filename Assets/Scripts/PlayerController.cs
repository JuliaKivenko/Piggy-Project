using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControllerAction playerControllerAcion;
    [SerializeField] CharacterController characterController;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float dashSpeedMultiplier;

    private float dashSpeed;
    [SerializeField] private float currentSpeed;

    private void Awake()
    {
        playerControllerAcion = new PlayerControllerAction();
        dashSpeed = walkSpeed * dashSpeedMultiplier;
        currentSpeed = walkSpeed;
    }

    private void OnEnable()
    {
        playerControllerAcion.Enable();
        playerControllerAcion.Player.Dash.performed += Dash;
    }

    private void OnDisable()
    {
        playerControllerAcion.Disable();
        playerControllerAcion.Player.Dash.performed -= Dash;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 movemementInput = playerControllerAcion.Player.Move.ReadValue<Vector2>();
        Vector3 toConvert = new Vector3(movemementInput.x, 0, movemementInput.y);
        Vector3 move = IsoVectorConvert(toConvert);
        characterController.Move(move * Time.deltaTime * currentSpeed);
    }

    public void Dash(InputAction.CallbackContext context)
    {
        currentSpeed = dashSpeed;
        if (context.canceled)
            currentSpeed = walkSpeed;
    }


    private Vector3 IsoVectorConvert(Vector3 vector)
    {
        Quaternion rotaton = Quaternion.Euler(0, 45, 0);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotaton);
        Vector3 result = isoMatrix.MultiplyPoint3x4(vector);
        return result;
    }
}

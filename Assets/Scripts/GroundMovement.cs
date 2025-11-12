using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public class GroundMovement : MonoBehaviour
{
    public bool IsMoving { get; private set; }
    private PlayerParameters parameters;
    private IInputProvider inputProvider;
    private void Start()
    {
        IsMoving = false;
        parameters = GetComponent<PlayerParameters>();
        inputProvider = InputManager.Instance;
    }

    private void Update()
    {
        Vector2 moveAmount = inputProvider.GetInput();
        if(moveAmount.x == 0 && moveAmount.y == 0)
        {
            IsMoving = false;
            return;
        }
        IsMoving = true;
        Movement(moveAmount);
        Rotation(moveAmount);
    }

    private void Movement(Vector2 move)
    {
        float moveValue = move.y * parameters.MoveSpeed * Time.deltaTime;
        transform.Translate(0, moveValue, 0);
    }

    private void Rotation(Vector2 rotate)
    {
        float rotationValue = -rotate.x * parameters.RotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotationValue);
    }
}

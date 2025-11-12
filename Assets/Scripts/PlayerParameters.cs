using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerParameters : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    public float MoveSpeed { get; private set; }
    public float RotationSpeed { get; private set; }

    private void Update()
    {
        MoveSpeed = moveSpeed;
        RotationSpeed = rotationSpeed;
    }
}

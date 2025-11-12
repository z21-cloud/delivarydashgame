using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InputManager : MonoBehaviour, IInputProvider
{
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public Vector2 GetInput()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        return new Vector2(xAxis, yAxis);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JumpData : MonoBehaviour
{
    [Header("Jump Info")]
    [SerializeField] private float jumpHeightScale = 1.0f;
    [SerializeField] private float jumpPushScale = 1.0f;

    public float JumpHeightScale { get; private set; }
    public float JumpPushScale { get; private set; }

    private void Start()
    {
        JumpHeightScale = jumpHeightScale;
        JumpPushScale = jumpPushScale;
    }
}

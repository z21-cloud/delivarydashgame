using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using NUnit.Framework;

public class Driver : MonoBehaviour
{
    public static event Action PackageEffectsEnable;
    public static event Action PackageEffectsDisable;
    public bool HasPackage { get; private set; }

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer playerSprite;

    [Header("Jumping")]
    [SerializeField] private AnimationCurve jumpCurve;

    [SerializeField] private LayerMask obstacle;

    Collider2D playerCollider;
    Rigidbody2D rb;
    private bool isJumping = false;

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Trap.PlayerFellTrap += DeletePackage;
    }

    private void DeletePackage()
    {
        HasPackage = false;
        PackageEffectsDisable?.Invoke();   //  передать информацию клиенту, что пакет передался
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Package>() && !HasPackage)
        {
            HasPackage = true;              //  подобрать пакет
            PackageEffectsEnable?.Invoke();  //  передать информацию пакету, что он подобран
            //  передать информацию менеджеру, что игрок подобрал пакет (для спауна противников)
            //  воспроизвести эффект и анимацию
        }

        if(other.GetComponent<Customer>() && HasPackage)
        {
            HasPackage = false;
            //  воспроизвести эффект и анимаацию
            PackageEffectsDisable?.Invoke();   //  передать информацию клиенту, что пакет передался
        }

        if(other.GetComponent<JumpData>())
        {
            JumpData jump = other.GetComponent<JumpData>();
            Jump(jump.JumpHeightScale, jump.JumpPushScale);
        }
    }

    public void Jump(float jumpHeightScale, float jumpPushScale)
    {
        if (!isJumping)
            StartCoroutine(JumpCoroutine(jumpHeightScale, jumpPushScale));
    }

    private IEnumerator JumpCoroutine(float jumpHeightScale, float jumpPushScale)
    {
        isJumping = true;
        float jumpStartTime = Time.time;

        //float velocityMagnitude = Mathf.Max(rb.linearVelocity.magnitude, 1f);
        float jumpDuration = 2f;

        //if (jumpDuration <= 0f)
        //    jumpDuration = .5f;

        //jumpHeightScale *= velocityMagnitude * 0.05f;
        //jumpHeightScale = Mathf.Clamp01(jumpHeightScale);

        playerCollider.enabled = false;

        rb.AddForce(rb.linearVelocity.normalized * jumpPushScale * 10, ForceMode2D.Impulse);

        while(isJumping)
        {
            float jumpCompletedPercentage = (Time.time - jumpStartTime) / jumpDuration;
            jumpCompletedPercentage = Mathf.Clamp01(jumpCompletedPercentage);

            playerSprite.transform.localScale = Vector3.one + jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale * Vector3.one;

            if (jumpCompletedPercentage == 1.0f)
                break;

            yield return null;
        }

        playerSprite.transform.localScale = Vector3.one;

        playerCollider.enabled = true;

        isJumping = false;
    }

    private void OnDisable()
    {
        Trap.PlayerFellTrap -= DeletePackage;
    }
}

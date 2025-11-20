using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Jump : MonoBehaviour
{
    [Header("Jump Parameters")]
    [SerializeField] private float jumpDuration = 1f;

    [Header("Jumping Curve")]
    [SerializeField] private AnimationCurve jumpCurve;

    [SerializeField] private LayerMask obstacle;

    private Collider2D playerCollider;
    private Rigidbody2D rb;
    private SpriteRenderer playerSprite;

    private bool isJumping = false;

    private void Start()
    {
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void CarJump(float jumpHeightScale, float jumpPushScale)
    {
        if (!isJumping)
            StartCoroutine(JumpCoroutine(jumpHeightScale, jumpPushScale));
    }

    private IEnumerator JumpCoroutine(float jumpHeightScale, float jumpPushScale)
    {
        isJumping = true;
        float jumpStartTime = Time.time;

        //float velocityMagnitude = Mathf.Max(rb.linearVelocity.magnitude, 1f);

        //if (jumpDuration <= 0f)
        //    jumpDuration = .5f;

        //jumpHeightScale *= velocityMagnitude * 0.05f;
        //jumpHeightScale = Mathf.Clamp01(jumpHeightScale);

        playerCollider.enabled = false;

        rb.AddForce(rb.linearVelocity.normalized * jumpPushScale * 10, ForceMode2D.Impulse);

        while (isJumping)
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
}

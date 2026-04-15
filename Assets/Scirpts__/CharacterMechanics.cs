
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class CharacterMechanics : MonoBehaviour
{
    public Transform targetPoint;
    public GameObject RayStartPoint;
    public float walkSpeed = 3f;
    public float stoppingDistance = 0.1f;

    private Rigidbody rb;
    private Animator animator;

    public bool IsStartMovement = false;
    private bool IsRayCastStart = true;

    public UnityEvent OnWalkStart;
    public UnityEvent AfterCompleteQuestion;
    public UnityEvent CheckLunchBox;

    public bool IsAnswerTrue = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void ChangeTarget(Transform target)
    {
        targetPoint = target;
    }

    public void Quesiton_Complete(float value)
    {
        StartCoroutine(OnQuestionComplete(value));
    }

    private IEnumerator OnQuestionComplete(float value)
    {
        yield return new WaitForSeconds(value);
        AfterCompleteQuestion.Invoke();
    }

    public void AutoChangeTarget(Transform target)
    {
        targetPoint = target;
        IsStartMovement = true;
    }

    void Update()
    {
        MoveTowardsTarget();
    }

    #region Animations

    public void PlayIdleAnimation()
    {
        if (animator == null) return;

        animator.SetBool("isWalking", false);
        animator.SetBool("IsIdle", true);
    }

    public void PlayNOAnimation()
    {
        if (animator == null) return;

        animator.SetBool("isWalking", false);
        animator.SetBool("IsIdle", false);
    }

    public void ResetAnimations()
    {
        if (animator == null) return;

        animator.SetBool("isWalking", false);
        animator.SetBool("IsIdle", false);
    }

    public void PlayWalkAnimation()
    {
        if (animator == null) return;

        animator.SetBool("isWalking", true);
        animator.SetBool("IsIdle", false);
        animator.SetBool("What", false);
    }

    #endregion

    private void MoveTowardsTarget()
    {
        if (!IsStartMovement)
        {
            StopMovement();
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, targetPoint.position);
        bool isObstacleInFront = Physics.Raycast(
            RayStartPoint.transform.position,
            -Vector3.back,
            out RaycastHit hit,
            stoppingDistance
        );

        Debug.DrawRay(RayStartPoint.transform.position, -Vector3.back, Color.red);

        if (isObstacleInFront && hit.collider.CompareTag("Player"))
        {
            Debug.DrawRay(RayStartPoint.transform.position, -Vector3.back, Color.green);
            Debug.Log("Player in front, stopping movement");

            IsRayCastStart = false;
            PlayIdleAnimation();
        }
        else
        {
            IsRayCastStart = true;
        }

        if (IsRayCastStart && distanceToTarget > stoppingDistance)
        {
            Vector3 direction = (targetPoint.position - transform.position).normalized;
            transform.LookAt(targetPoint);
            transform.position += direction * walkSpeed * Time.deltaTime;
            PlayWalkAnimation();
        }
        else
        {
            StopMovement();
        }
    }

    public void StopMovement()
    {
        rb.linearVelocity = Vector3.zero;
        PlayIdleAnimation();
    }

    public void SadAnimation(int variant )
    {
        Debug.Log($"SadAnimationPlay Variant {variant}");

        // Play different sad animations based on the variant
        switch (variant)
        {
            case 0:
                animator?.SetTrigger("IsSad");
                break;
            case 1:
                animator?.SetTrigger("IsSad1");
                break;
            case 2:
                animator?.SetTrigger("IsSad2");
                break;
            default:
                animator?.SetTrigger("IsSad1");
                break;
        }
    }

    public void HappyAnimation(int variant)
    {
        Debug.Log($"HappyAnimationPlay Variant {variant}");

        // Play different happy animations based on the variant
        switch (variant)
        {
            case 0:
                animator?.SetTrigger("IsHappy");
                break;
            case 1:
                animator?.SetTrigger("IsHappy1");
                break;
            case 2:
                animator?.SetTrigger("IsHappy2");
                break;
            default:
                animator?.SetTrigger("IsHappy1");
                break;
        }
    }
    public void WhatAnimation(bool value)
    {
        Debug.Log("WhatAnimationPlay");
        animator?.SetBool("What", value);

        if (value)
        {
            animator?.SetBool("Clapping", value);
        }
    }
}

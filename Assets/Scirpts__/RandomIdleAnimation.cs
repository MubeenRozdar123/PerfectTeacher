
using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
    public Animator playerAnimators;
    public string[] animationTriggers;

    private Animator currentAnimator;

    void Start()
    {
        currentAnimator = GetComponent<Animator>();
        PlayRandomAnimation();
    }

    public void PlayRandomAnimation()
    {
        if (animationTriggers == null || animationTriggers.Length == 0)
        {
           // Debug.LogWarning("No animation triggers specified.");
            return;
        }

        string randomTrigger = animationTriggers[Random.Range(0, animationTriggers.Length)];
        currentAnimator.SetTrigger(randomTrigger);
    }

    public void OnAnimationComplete()
    {
       // Debug.Log("Animation Completed. Switching to a new one.");
        PlayRandomAnimation();
    }

}
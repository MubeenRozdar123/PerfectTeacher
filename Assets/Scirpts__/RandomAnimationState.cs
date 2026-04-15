using UnityEngine;

public class RandomAnimationState : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Select a random trigger
        string[] triggers = { "Idle1", "Idle2", "Idle3" };

        string randomTrigger = triggers[Random.Range(0, triggers.Length)];

        // Trigger the next animation
        animator.SetTrigger(randomTrigger);
    }
}

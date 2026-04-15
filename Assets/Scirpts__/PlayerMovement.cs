using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator; // Reference to the player's Animator
    [SerializeField] private Transform[] checkpoints; // Array of checkpoint positions
    [SerializeField] private float movementSpeed = 2f; // Movement speed of the player

    private int currentCheckpointIndex = 0; // Index of the current checkpoint
    private bool isMoving = false; // To track if the player is moving

    private void Start()
    {
        // Ensure the player starts in the sitting state
        animator.SetTrigger("Sit");
    }

    public void StandUpAndMove()
    {
        if (currentCheckpointIndex >= checkpoints.Length) return; // No checkpoints left

        StartCoroutine(MoveToCheckpoints());
    }

    private IEnumerator MoveToCheckpoints()
    {
        // Play StandUp animation
        animator.SetTrigger("StandUp");
        yield return new WaitForSeconds(1f); // Adjust based on StandUp animation length

        // Start moving towards the first checkpoint
        while (currentCheckpointIndex < checkpoints.Length)
        {
            // Move to the next checkpoint
            Transform targetCheckpoint = checkpoints[currentCheckpointIndex];
            animator.SetBool("IsWalking", true);

            while (Vector3.Distance(transform.position, targetCheckpoint.position) > 0.1f)
            {
                // Move towards the target checkpoint
                Vector3 direction = (targetCheckpoint.position - transform.position).normalized;
                transform.position += direction * movementSpeed * Time.deltaTime;
                transform.LookAt(targetCheckpoint); // Ensure the player faces the checkpoint
                yield return null;
            }

            // Reached the checkpoint
            transform.position = targetCheckpoint.position;
            currentCheckpointIndex++;

            yield return new WaitForSeconds(0.5f); // Optional pause at each checkpoint
        }

        // Stop walking animation when all checkpoints are reached
        animator.SetBool("IsWalking", false);
        Debug.Log("All checkpoints reached!");
    }
}

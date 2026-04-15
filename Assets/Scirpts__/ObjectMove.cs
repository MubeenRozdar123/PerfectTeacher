using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement.

    void Update()
    {
        // Move left when the left arrow key or "A" is pressed.
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        // Move right when the right arrow key or "D" is pressed.
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
    }

    void MoveLeft()
    {
        // Move the object to the left.
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    void MoveRight()
    {
        // Move the object to the right.
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}

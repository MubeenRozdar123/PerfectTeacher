using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCheater : MonoBehaviour
{
    private void Update()
    {
        // Check if input is from touch or mouse
        if (IsTouchInput())
        {
            // Handle touch input
            HandleTouchInput();
        }
        else
        {
            // Handle mouse input (Editor or desktop)
            HandleMouseInput();
        }
    }

    private bool IsTouchInput()
    {
        // Checks if there are any touches, which indicates Android or mobile device
        return Input.touchCount > 0;
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Check if the ray hits an object
            {
                // Execute method for the object hit by mouse click
                ExecuteMethod(hit.transform);
            }
        }
    }

    private void HandleTouchInput()
    {
        // Handle the first touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            Camera.main.transform.LookAt(touch.position);
            if (Physics.Raycast(ray, out hit)) // Check if the ray hits an object
            {
                // Execute method for the object touched
                ExecuteMethod(hit.transform);
            }
        }
    }

    private void ExecuteMethod(Transform objectTransform)
    {
        CheatingStudents ref_CheatingStudent = objectTransform.GetComponent<CheatingStudents>();

        if (ref_CheatingStudent && ref_CheatingStudent.IsCheating)
        {
            ref_CheatingStudent.CheatingCaught();
            Debug.Log("Object clicked/touched: " + objectTransform.name);
        }
        // Execute custom logic when an object is clicked/touched
    }
}

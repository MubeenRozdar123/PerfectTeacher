using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassQuiz : MonoBehaviour
{
    public Camera Ref_Camera;
 
    //public GameObject[] AllStudents;




    //public void StartAnimation()
    //{
    //    for (int i = 0; i < AllStudents.Length; i++)
    //    {

    //        Animator Ref_Animator = AllStudents[i].transform.GetComponent<Animator>();
    //        Ref_Animator.SetBool("IsIdle", true);
    //        Ref_Animator.SetBool("HandDown", false);
    //    }
    //}





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
            Ray ray = Ref_Camera.ScreenPointToRay(Input.mousePosition);
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
            Ray ray = Ref_Camera.ScreenPointToRay(touch.position);
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
        QuizAnswer ref_QuizAnswer = objectTransform.GetComponent<QuizAnswer>();

        if (ref_QuizAnswer )
        {
            ref_QuizAnswer.GiveAnswer();
            Debug.Log("Object clicked/touched: " + objectTransform.name);
        }
        // Execute custom logic when an object is clicked/touched
    }

}

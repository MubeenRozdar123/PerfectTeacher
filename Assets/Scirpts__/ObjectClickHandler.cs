using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClickHandler : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) // Left mouse click
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        if (hit.collider.gameObject.CompareTag("Player"))
        //        {
        //            // ExecuteAction();
        //            Debug.Log(hit.collider.gameObject.name);

        //        }
        //    }
        //}

        //

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
              {
        //            // ExecuteAction();
                   Debug.Log(hit.collider.gameObject.name);

                           }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwipeLook : MonoBehaviour
{

    public Camera Camera;
    public float sensitivity = 0.2f; // Control swipe sensitivity.
    public float maxVerticalAngle = 80f; // Maximum look up/down angle.
    public float minHorizontalAngle = -60f; // Minimum horizontal angle (left).
    public float maxHorizontalAngle = 60f; // Maximum horizontal angle (right).

    private float xRotation = 0f; // Tracks vertical rotation (up/down).
    private float yRotation = 0f; // Tracks horizontal rotation (left/right).


    public float raycastRange = 100f; // Maximum range of the raycast.
    public LayerMask hitLayers; //
  //  public bool HitRaycast = false;
    public string targetTag;
    void Start()
    {
        // Initialize rotation from the current camera rotation.
        Vector3 initialRotation = transform.eulerAngles;
        xRotation = NormalizeAngle(initialRotation.x); // Vertical rotation (pitch)
        yRotation = NormalizeAngle(initialRotation.y); // Horizontal rotation (yaw)
    }

    void Update()
    {
        FireRaycast();
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                HandleRotation(touch.deltaPosition.x, touch.deltaPosition.y);
            }
        }
        else if (Application.isEditor)
        {
            // Fallback for mouse input in the editor.
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            HandleRotation(mouseX * 10f, mouseY * 10f);
        }
    }

    void HandleRotation(float deltaX, float deltaY)
    {
        // Adjust the rotation based on input.
        yRotation += deltaX * sensitivity;
        xRotation -= deltaY * sensitivity;

        // Clamp the rotations.
        xRotation = Mathf.Clamp(xRotation, -maxVerticalAngle, maxVerticalAngle);
        yRotation = Mathf.Clamp(yRotation, minHorizontalAngle, maxHorizontalAngle);

        // Apply the rotation.
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    // Normalize angles to -180 to 180 range for proper clamping.
    private float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 180) angle -= 360;
        return angle;
    }

    void FireRaycast()
    {
      
        // Get the center of the screen.
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        // Create a ray from the camera through the screen center.
        Ray ray = Camera.ScreenPointToRay(screenCenter);

        // Perform the raycast.
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastRange, hitLayers))
        {
            // Check the tag of the hit object.
          //  if (hit.collider.CompareTag(targetTag))
            {
                Debug.Log("Ray Hitted");
                // Hit object has the desired tag, draw green ray.
                Debug.DrawLine(ray.origin, ray.direction * hit.distance, Color.green, 10f);
                Debug.Log($"Hit {hit.collider.gameObject.name} with tag {targetTag}");
            //    RayCastFill.IsStartFilling = true;
                // Perform a specific action on the hit object.
                PerformAction(hit.collider.gameObject); // perform actions when hit specific ray cast object
            }
         
        }
        else
        {
            RayCastFill.IsStartFilling = false;
            // No hit, draw red ray.
            Debug.DrawLine(ray.origin, ray.direction * raycastRange, Color.red, 2f);
            Debug.Log("No hit detected.");
        }

    }
    CheatingStudents ref_CheatingSudents;
    public RayCastFill RayCastFill;
    void PerformAction(GameObject hitObject)
    {
        // Example: Call a method on the hit object if it has a specific component.
      //  if (HitRaycast)
        {
          

            ref_CheatingSudents = hitObject.GetComponent<CheatingStudents>();
            Debug.Log("Method  execute 0 ");
            if (ref_CheatingSudents != null && ref_CheatingSudents.IsCheating)
            {
               
                RayCastFill.IsStartFilling = true;
                Debug.Log("Method  execute");
                
               // ref_CheatingSudents.CheatingCaught();
               //  HitRaycast = false;

            } 
            else
            {
                ref_CheatingSudents = null;
                RayCastFill.IsStartFilling = false;
            }

            RayCastFill.ref_cheatingStudents = ref_CheatingSudents;
        }

        //else
        //{
        //    Invoke(nameof(AutoEnableRaycast), 2f);
        //}

     

    }


    //public void AutoEnableRaycast()
    //{
    //    Debug.Log("Raycast Stopped");
        
    //    HitRaycast = true;
    //}




}

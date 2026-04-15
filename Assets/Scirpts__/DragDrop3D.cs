using UnityEngine;
using UnityEngine.Events;

public class DragDrop3D : MonoBehaviour
{
    public UnityEvent OnObjectPlaced;
    private Vector3 offset; // Offset between the object and the touch position private Vector3 offset; // Offset between the object and the input position
    private bool isDragging = false;
    private Vector3 originalPosition;

    [SerializeField] private Transform correctDropPoint; // Correct drop point for this object
//
    [SerializeField] private DragDrop3D[] allDraggableObjects; // Reference to all draggable objects
    [SerializeField] private bool restrictToVertical = false; // Restrict to Y-axis
    [SerializeField] private bool restrictToHorizontal = false; // Restrict to X/Z axes

    public Camera mainCamera;

    private void Awake()
    {
        // Register this object as not placed initially
        //if (!placedObjects.ContainsKey(transform))
        //{
        //    placedObjects.Add(transform, false);
        //}
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            // Handle mouse input in the Unity Editor
            HandleMouseInput();
        }
        else if (Input.touchCount > 0)
        {
            // Handle touch input on Android (or other touch-enabled devices)
          //  HandleTouchInput();
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                originalPosition = transform.position;

                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Vector3.Distance(mainCamera.transform.position, transform.position); // Set depth
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

                offset = transform.position - worldPosition;

                isDragging = true;
            }
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Vector3.Distance(mainCamera.transform.position, transform.position); // Set depth
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition) + offset;

            ApplyRestrictions(ref worldPosition);

            transform.position = worldPosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDragging();
        }
    }

    private void HandleTouchInput()
    {
        Touch touch = Input.GetTouch(0); // Get the first touch

        if (touch.phase == TouchPhase.Began && !isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                originalPosition = transform.position;

                Vector3 touchPosition = touch.position;
                touchPosition.z = Vector3.Distance(mainCamera.transform.position, transform.position); // Set depth
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

                offset = transform.position - worldPosition;

                isDragging = true;
            }
        }
        else if (touch.phase == TouchPhase.Moved && isDragging)
        {
            Vector3 touchPosition = touch.position;
            touchPosition.z = Vector3.Distance(mainCamera.transform.position, transform.position); // Set depth
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition) + offset;

            ApplyRestrictions(ref worldPosition);

            transform.position = worldPosition;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            EndDragging();
        }
    }

    private void ApplyRestrictions(ref Vector3 worldPosition)
    {
        if (restrictToVertical)
        {
            // Allow only vertical (Y-axis) movement
            worldPosition.x = originalPosition.x;
            worldPosition.z = originalPosition.z;
        }
        else if (restrictToHorizontal)
        {
            // Allow only horizontal (X/Z axes) movement
            worldPosition.y = originalPosition.y;
        }
    }

    private void EndDragging()
    {
        isDragging = false;

        // Check if the object is close to its correct drop point
        if (Vector3.Distance(transform.position, correctDropPoint.position) < 0.5f) // Adjust distance threshold as needed
        {
            // Snap to the correct drop point
            transform.position = correctDropPoint.position;
            // Mark this object as placed
         //   placedObjects[transform] = true;

            Debug.Log($"Object {transform.name} placed.");
            OnObjectPlaced.Invoke();
            // Check if all objects are correctly placed
            //if (AllObjectsPlaced())
            //{
            //    Debug.Log("All objects are placed!");
            //    Complete();
            //}
        }
        else
        {
            // Reset to the original position if not dropped correctly
            transform.position = originalPosition;

            // Mark this object as not placed
          //  placedObjects[transform] = false;
        }
    }

    //private bool AllObjectsPlaced()
    //{
    //    // Return true only if all objects are placed correctly
    //    foreach (var isPlaced in placedObjects.Values)
    //    {
    //        if (!isPlaced) return false;
    //    }
    //    return true;
    //}

    private void Complete()
    {
        Debug.Log("All objects placed correctly! Task completed.");
        // Add your logic here, e.g., transition to the next level, show a success message, etc.
    }
}

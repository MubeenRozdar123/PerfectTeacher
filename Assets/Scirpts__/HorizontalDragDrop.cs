using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class HorizontalDragDrop : MonoBehaviour
{


    [SerializeField]
    private Vector3 originalPosition; // Store the object's original position
    private Vector3 offset; // Offset between object and input point
    public Camera mainCamera; // Reference to the main camera
    private bool isDragging = false;
    public UnityEvent OnPlaced;
    [SerializeField] private Transform targetPosition; // The position to snap to
    [SerializeField] private float snapThreshold = 1.0f; // Distance to snap
    [SerializeField] private float fixedYPosition = 0.0f; // Fixed Y-axis value

    private static List<HorizontalDragDrop> allDraggableObjects = new List<HorizontalDragDrop>();
    private static HashSet<HorizontalDragDrop> placedObjects = new HashSet<HorizontalDragDrop>();

    private void Start()
    {
        originalPosition = transform.position;
        // Automatically assign the main camera if not set
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Register this object in the list of draggable objects
        if (!allDraggableObjects.Contains(this))
        {
            allDraggableObjects.Add(this);
        }
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            HandleMouseInput();
        }
        else
        {
            HandleTouchInput();
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                // Start dragging
                isDragging = true;
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(GetMouseWorldPoint());
                offset = transform.position - worldPosition;
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            // Dragging object
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(GetMouseWorldPoint());
            worldPosition.y = fixedYPosition; // Restrict movement to a fixed Y value
            transform.position = worldPosition + offset;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            // Stop dragging and check snap condition
            isDragging = false;
            SnapToTarget();
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = mainCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    // Start dragging
                    isDragging = true;
                    Vector3 worldPosition = mainCamera.ScreenToWorldPoint(GetTouchWorldPoint(touch));
                    offset = transform.position - worldPosition;
                }
            }

            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                // Dragging object
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(GetTouchWorldPoint(touch));
                worldPosition.y = fixedYPosition; // Restrict movement to a fixed Y value
                transform.position = worldPosition + offset;
            }

            if (touch.phase == TouchPhase.Ended && isDragging)
            {
                // Stop dragging and check snap condition
                isDragging = false;
                SnapToTarget();
            }
        }
    }

    private Vector3 GetMouseWorldPoint()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Vector3.Distance(mainCamera.transform.position, transform.position); // Set depth
        return mousePosition;
    }

    private Vector3 GetTouchWorldPoint(Touch touch)
    {
        Vector3 touchPosition = touch.position;
        touchPosition.z = Vector3.Distance(mainCamera.transform.position, transform.position); // Set depth
        return touchPosition;
    }

    private void SnapToTarget()
    {
        if (Vector3.Distance(transform.position, targetPosition.position) <= snapThreshold)
        {
            transform.position = new Vector3(
                targetPosition.position.x,
                fixedYPosition, // Ensure Y position stays fixed even when snapping
                targetPosition.position.z
            );

            targetPosition.transform.gameObject.SetActive( false );
            OnPlaced.Invoke();
            // Mark this object as placed
            if (!placedObjects.Contains(this))
            {
                placedObjects.Add(this);
            }

            // Check if all objects are placed
            if (AllObjectsPlaced())
            {
                Complete();
            }
        }

        else
        {
            // Return the object to its original position
            transform.position = originalPosition;

        }
    }

    private static bool AllObjectsPlaced()
    {
        return placedObjects.Count == allDraggableObjects.Count;
    }

    private void Complete()
    {
        Debug.Log("All objects placed! Task complete!");
        // Add your logic here, e.g., show a success message, load the next level, etc.
        //GameManager.instance.GameComplete();
    }
}

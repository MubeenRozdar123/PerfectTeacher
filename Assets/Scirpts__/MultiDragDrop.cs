using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MultiDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;

    [SerializeField] private RectTransform correctDropPoint; // The correct drop point for this image
    private static Dictionary<RectTransform, bool> placedImages = new Dictionary<RectTransform, bool>(); // Tracks placed images

    // Ensure this script has access to all draggable objects
    [SerializeField] private MultiDragDrop[] allDraggableObjects;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Register the image as not placed initially
        if (!placedImages.ContainsKey(rectTransform))
        {
            placedImages.Add(rectTransform, false);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;

        // Make the dragged object more transparent and ignore raycasts during drag
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the object's position to follow the mouse
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Restore object's alpha and raycast blocking
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Check if the dragged object is close to its correct drop point
        if (RectTransformUtility.RectangleContainsScreenPoint(correctDropPoint, Input.mousePosition, eventData.pressEventCamera))
        {
            // Snap the object to the correct drop point
            rectTransform.anchoredPosition = correctDropPoint.anchoredPosition;

            // Mark this image as placed correctly
            placedImages[rectTransform] = true;

            // Check if all images are correctly placed
            if (AllImagesPlaced())
            {
                Complete();
            }
        }
        else
        {
            // Reset to the original position if not dropped at the target
            rectTransform.anchoredPosition = originalPosition;

            // Mark this image as not placed
            placedImages[rectTransform] = false;
        }
    }

    private bool AllImagesPlaced()
    {
        // Return true only if all images are placed correctly
        foreach (var isPlaced in placedImages.Values)
        {
            if (!isPlaced) return false;
        }
        return true;
    }

    private void Complete()
    {
        Debug.Log("All images placed correctly! Task completed.");
        // Add your logic here, e.g., transition to the next level, show a success message, etc.
    }
}

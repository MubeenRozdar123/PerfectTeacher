
using UnityEngine.UI;
using UnityEngine;

public class RayCastFill : MonoBehaviour
{
    public Image fillImage;      // UI Image to fill
    public float fillTime = 2f;  // Time to fill the image completely (in seconds)

    private float fillSpeed;     // Calculated fill speed

    public CheatingStudents ref_cheatingStudents;


    public bool IsStartFilling = false;
    void OnEnable()
    {
        // Calculate the fill speed (1.0 divided by the fill time)
        fillSpeed = 1f / fillTime;

        // Ensure the fillImage starts at 0
        if (fillImage != null)
        {
            fillImage.fillAmount = 0f;
        }
        else
        {
            Debug.LogError("Fill Image is not assigned!");
        }
    }

    void Update()
    {
        if (IsStartFilling)
        {
            if (fillImage != null && fillImage.fillAmount < 1f)
            {
                // Gradually increase the fill amount
                fillImage.fillAmount = Mathf.MoveTowards(fillImage.fillAmount, 1f, fillSpeed * Time.deltaTime);

                // Debug to monitor progress
                Debug.Log($"Current Fill Amount: {fillImage.fillAmount}");
            }

            // Execute method when the image is completely filled
            if (fillImage.fillAmount >= 1f)
            {
                if (ref_cheatingStudents && ref_cheatingStudents.IsCheating)
                {
                    ref_cheatingStudents.CheatingCaught();
                    ref_cheatingStudents = null;
                }
                if (fillImage != null)
                {
                    fillImage.fillAmount = 0f;
                }

            } 
        }

        else
        {
            if (fillImage != null)
            {
                fillImage.fillAmount = 0f;
            }
        }
    }

    //void OnImageFilled(CheatingStudents ref_cheatingStudents)
    //{
    //    Debug.Log("Image completely filled! Method executed.");
    //    // Place your custom functionality here
    //    // Example: Trigger an event, enable a GameObject, etc.
    //}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderFill : MonoBehaviour
{
    public Slider slider;         // Reference to the Slider component
    public Text percentageText;  // Reference to the Text component for percentage display
    public float fillTime = 3f;  // Time to fill the slider

    private void OnEnable()
    {
        // Ensure the slider starts at 0
        if (slider != null)
            slider.value = 0;

        // Initialize percentage text
        if (percentageText != null)
            percentageText.text = "0%";


        StartFilling();
    }
    
    public void StartFilling()
    {
        StartCoroutine(FillSlider());
    }

    private IEnumerator FillSlider()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fillTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / fillTime); // Normalize the value
            slider.value = progress;

            // Update the percentage text
            if (percentageText != null)
                percentageText.text = Mathf.RoundToInt(progress * 100) + "%";

            yield return null;
        }

        slider.value = 1; // Ensure it's fully filled

        // Update the percentage text to 100%
        if (percentageText != null)
            percentageText.text = "100%";
    }
}

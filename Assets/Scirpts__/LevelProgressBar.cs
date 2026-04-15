using UnityEngine.UI;

using UnityEngine;
using DG.Tweening;

public class LevelProgressBar : MonoBehaviour
{

    public Image ProgressBarFill; // Assign your progress bar's fill Image in the Inspector
    public float fillDuration = 0.5f; // Duration for the smooth fill animation
    int Counter;
    public float MaxValue = 4f;
    public static LevelProgressBar Instance;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Counter = 0;
    }
    public void FillProgressBar()
    {
        Debug.Log("In Progress Bar");
        Counter++;
        // Target fill amount, clamped to a range of 0 to 1
       if (Counter <= MaxValue)
        {
            float targetFill = Mathf.Clamp(Counter / MaxValue, 0f, 1f);
            // Smoothly tween the fill amount
            ProgressBarFill.DOFillAmount(targetFill, fillDuration).SetEase(Ease.InOutQuad);
        }
      

    }

  

    public void SetMaxValue(float maxValue)
    {
        MaxValue = maxValue;
    }
}

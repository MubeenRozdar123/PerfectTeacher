using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public Text CurrencyText;
    public GameObject GamePlayUI;
    public static UIController Instance;
    public GameObject ObjectivePanel;
    public Text ObjectiveTxt;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void OnEnable()
    {
        Time.timeScale = 0f;
        GamePlayUI.SetActive(true);
        UpdateCurrencyText();
    }

    public void UpdateCurrencyText()
    {
        CurrencyText.text = PlayerPrefsManager.CurrencyUpdate().ToString();
    }

    public void EnableObjectivePanel(string text)
    {
        ObjectivePanel.SetActive(true);
        ObjectiveTxt.text = text;
    }

    public void ObjectiveOkay()
    {
        PlayButtonClickSound();
        ResumeGame();
        HideObjectivePanel();
    }

    private void PlayButtonClickSound()
    {
        GameManager.instance.ref_AudioManager.PlayButtonClickSound();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    private void HideObjectivePanel()
    {
        // ObjectivePanel.GetComponent<DOTweenAnimation>().DOPlayBackwards(); // Uncomment if animation is needed
        ObjectivePanel.SetActive(false);
    }

}

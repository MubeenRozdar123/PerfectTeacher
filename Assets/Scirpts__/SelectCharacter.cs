using System.Collections;
using Unity.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SelectCharacter : MonoBehaviour
{
    [Header("Text")]
    public Text ShowCurrency;

    [Space(5)]
    [Header("Animator")]
    public Animator male_Animator;
    public Animator female_Animator;

    public GameObject Bg;
    public GameObject LoadinScreen;

    private const string SelectCharacterKey = "selectcharacter";

    private void Awake()
    {
        // Ensure the "selectcharacter" PlayerPref exists
        if (!PlayerPrefs.HasKey(SelectCharacterKey))
        {
            PlayerPrefs.SetInt(SelectCharacterKey, 0);
        }
    }

    private void Start()
    {
        UpdateCurrencyDisplay();

        // Toggle background visibility based on selected character
        Bg.SetActive(PlayerPrefs.GetInt(SelectCharacterKey) != 0);
    }

    public void Change_Scene()
    {
        StartCoroutine(ChangeSceneCoroutine());
    }

    public void PlayClick()
    {
        Change_Scene();
    }

    private IEnumerator ChangeSceneCoroutine()
    {
        yield return new WaitForSeconds(2f);
        LoadinScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync("GamePlay");
    }

    public void MailSelect()
    {
        TriggerAnimator(male_Animator, "handshake");
        PlayerPrefs.SetInt(SelectCharacterKey, 1);
        Change_Scene();
    }

    public void FemaleSelect()
    {
        TriggerAnimator(female_Animator, "handshake");
        PlayerPrefs.SetInt(SelectCharacterKey, 1);
        Change_Scene();
    }

    public void SetCurrencyValue(int value)
    {
        PlayerPrefsManager.CurrencyUpdate(value);
        UpdateCurrencyDisplay();
    }

    public void UpdateCurrencyDisplay()
    {
        ShowCurrency.text = PlayerPrefsManager.CurrencyUpdate().ToString();
    }

    private void TriggerAnimator(Animator animator, string triggerName)
    {
        animator?.SetTrigger(triggerName);
    }
}

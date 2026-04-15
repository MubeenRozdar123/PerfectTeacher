using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class TeacherOperations : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject PayMoneyPop;
    public GameObject StudentNamePanel;
    public GameObject Btns;
    public Button YesBtn, NoBtn, PayMoneyBtn, PayMoneyRewardBtn;
    public Image ProgressFill;

    [Header("Transform")]
    public Transform LeftPoint, RightPoint;

    [Header("Variables")]
    public static int Counter;

    [Header("Script References")]
    public CharacterMechanics ref_characterMchanics;
    private CelebrationTextManager ref_CelebrationTextManager;

    private void Start()
    {
        ref_CelebrationTextManager = GetComponent<CelebrationTextManager>();
        Counter = 0;
        PayMoneyPop.SetActive(false);
        ProgressFill.fillAmount = 0;

        // Register button events
        YesBtn.onClick.AddListener(TrueAnswer);
        NoBtn.onClick.AddListener(FalseAnswer);
        PayMoneyBtn.onClick.AddListener(PayMoney);
        PayMoneyRewardBtn.onClick.AddListener(WatchRewardedVideo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ref_characterMchanics = other.GetComponent<CharacterMechanics>();
            if (ref_characterMchanics != null)
            {
                ref_characterMchanics.IsStartMovement = false;
                ref_characterMchanics.CheckLunchBox?.Invoke();
                ref_characterMchanics.transform.LookAt(transform.GetChild(0).position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && ref_characterMchanics != null)
        {
            ref_characterMchanics.IsStartMovement = true;
        }
    }

    public void TrueAnswer()
    {
        if (ref_characterMchanics == null)
        {
            Debug.LogWarning("Character mechanics reference is missing.");
            return;
        }

        bool isAnswerCorrect = ref_characterMchanics.IsAnswerTrue;
        GameManager.instance.ref_AudioManager.OtherSound(isAnswerCorrect ? "Yes" : "No");

        if (isAnswerCorrect)
        {
            Debug.Log("True");
            ref_CelebrationTextManager.OnRightAnswer();
            PlayerPrefsManager.StreakUpdate(1);
        }
        else
        {
            Debug.Log("Wrong");
            ref_CelebrationTextManager.OnWrongAnswer();
            PlayerPrefsManager.StreakUpdate(-1);
        }

        ref_CelebrationTextManager.UpdateStreak();
        ref_characterMchanics.HappyAnimation(Random.Range(0 , 2));
        StartCoroutine(EnableDisableAnswer(isAnswerCorrect ? 2f : 1f));

        if (LeftPoint)
            ref_characterMchanics.ChangeTarget(LeftPoint);
    }

    public void FalseAnswer()
    {
        if (ref_characterMchanics == null)
        {
            Debug.LogWarning("Character mechanics reference is missing.");
            return;
        }

        bool isAnswerCorrect = ref_characterMchanics.IsAnswerTrue;
        if (isAnswerCorrect)
        {
            Debug.Log("Answer Was True");
            HandleIncorrectAnswer();
        }
        else
        {
            Debug.Log("Actually Wrong");
            ref_characterMchanics.SadAnimation(Random.Range(0, 2));
            ref_CelebrationTextManager.OnRightAnswer();
            PlayerPrefsManager.StreakUpdate(1);
            ref_CelebrationTextManager.UpdateStreak();

            if (RightPoint)
                ref_characterMchanics.ChangeTarget(RightPoint);

            StartCoroutine(EnableDisableAnswer(2f));
        }
    }

    private void HandleIncorrectAnswer()
    {
        GameManager.instance.ref_AudioManager.OtherSound("No");
        PlayerPrefs.SetInt("Streak", 0);
        ref_CelebrationTextManager.UpdateStreak();
        ref_CelebrationTextManager.OnWrongAnswer();

        ref_characterMchanics.WhatAnimation(true);
        Btns.SetActive(false);

        if (LeftPoint)
            ref_characterMchanics.ChangeTarget(LeftPoint);

        if (PayMoneyPop)
            PayMoneyPop.SetActive(true);
    }

    public void PayMoney()
    {
        if (ref_characterMchanics == null || !ref_characterMchanics.IsAnswerTrue)
            return;

        PlayerPrefsManager.CurrencyUpdate(-25);
        UIController.Instance.UpdateCurrencyText();
        Debug.Log("Pay Money");
        GameManager.instance.ref_AudioManager.PlayButtonClickSound();
        Btns.SetActive(true);
        PayMoneyPop.SetActive(false);
        ref_characterMchanics.WhatAnimation(false);
        StartCoroutine(EnableDisableAnswer(0.5f));
    }

    public void WatchRewardedVideo()
    {
        Debug.Log("Watch Video");
        GameManager.instance.ref_AudioManager.PlayButtonClickSound();
        Btns.SetActive(true);
        PayMoneyPop.SetActive(false);
        ref_characterMchanics.WhatAnimation(false);
        StartCoroutine(EnableDisableAnswer(0.5f));
    }

    private IEnumerator EnableDisableAnswer(float delay)
    {
        ProgressBarFill();
        ref_characterMchanics?.OnWalkStart?.Invoke();
        yield return new WaitForSeconds(delay);

        ref_characterMchanics?.Quesiton_Complete(0f);
        GetComponent<BoxCollider>().enabled = false;
        ref_characterMchanics.IsStartMovement = true;
        ref_characterMchanics = null;

        yield return new WaitForSeconds(2f);
        GetComponent<BoxCollider>().enabled = true;
    }

    private void ProgressBarFill()
    {
        Counter++;
        float targetFill = Mathf.Clamp(Counter / 4f, 0f, 1f);
        ProgressFill.DOFillAmount(targetFill, 0.5f).SetEase(Ease.InOutQuad);

        if (Counter > 3)
        {
            Debug.Log("Level Complete " + Counter);
            Counter = 0;
        }
    }

    private void OnDisable()
    {
        if (ref_characterMchanics != null)
        {
            Debug.Log("Level Complete " + Counter);
            ref_characterMchanics.IsStartMovement = true;
        }
    }
}

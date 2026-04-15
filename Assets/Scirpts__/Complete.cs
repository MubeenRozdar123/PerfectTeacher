
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Complete : MonoBehaviour
{
    public GameObject Stars, Passed;
    public GameObject LoadingPanel;
    public static Complete Instance;
    public GameObject marker;
    public GameObject RewardPanel;
    public Text RewardText;
    public GameObject Complete_Panel;
    public Text BtnText;
    private const int BaseReward = 25;

    public void Start()
    {
        Stars.SetActive(false);
        Passed.SetActive(false);
        LoadingPanel.SetActive(false);
        Complete_Panel.SetActive(true);

        StartCoroutine(Active_Stars());

        if (GameManager.instance != null)
        {
            GameManager.instance.ref_AudioManager.PlayButtonClickSound();
        }
    }

    private IEnumerator Active_Stars()
    {
        yield return new WaitForSeconds(1f);
        Stars.SetActive(true);
        yield return new WaitForSeconds(1f);
        Passed.SetActive(true);
    }

    public void getCoins()
    {
        DisableGameObjects();
        RewardPanel.SetActive(true);
        UpdateReward(BaseReward);

        if (GameManager.instance != null)
        {
            GameManager.instance.ref_AudioManager.PlayButtonClickSound();
        }
    }

    public void DoubleBtn()
    {
        DisableGameObjects();

        float markerPositionX = marker.GetComponent<RectTransform>().anchoredPosition.x;
        int rewardMultiplier = DetermineRewardMultiplier(markerPositionX);
        int rewardAmount = BaseReward * rewardMultiplier;


        UpdateReward(rewardAmount);

        if (GameManager.instance != null)
        {
            GameManager.instance.ref_AudioManager.PlayButtonClickSound();
        }
    }
    private void Update()
    {
        float markerPositionX = marker.GetComponent<RectTransform>().anchoredPosition.x;

        UpdateText(markerPositionX);
    }
    public void Next()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ref_AudioManager.PlayButtonClickSound();
        }

        StartCoroutine(SwitchScene());
    }

    private IEnumerator SwitchScene()
    {
        LoadingPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync("GamePlay");
    }

    private void DisableGameObjects()
    {
        marker.GetComponent<DOTweenAnimation>().DOPause ();
       // Passed.SetActive(false);
        Stars.SetActive(false);
    }

    bool IsWithinRange(float value, float min, float max)
    {
        bool result = value >= min && value <= max;
        Debug.Log($"Checking {value} in range {min} to {max}: {result}");
        return result;
    }

    public void UpdateText(float positionX)
    {
        if (IsWithinRange(positionX, 296, 424))
        {
            Debug.Log("5X");
            BtnText.text = "5X";
          
        }
        if (IsWithinRange(positionX, 186, 295) || IsWithinRange(positionX, 425, 516))
        {
            Debug.Log("4X");
            BtnText.text = "4X";
        }

        if (IsWithinRange(positionX, 101, 186) || IsWithinRange(positionX, 517, 618))
        {
            Debug.Log("3X");
            BtnText.text = "3X";

        }

        if (IsWithinRange(positionX, 54, 100) || IsWithinRange(positionX, 619, 706))
        {
            Debug.Log("2X");
            BtnText.text = "2X";

        }

        else
        {
            Debug.Log("1X");
          
        }
    }    

    private int DetermineRewardMultiplier(float positionX)
    {
        Debug.Log($"positionX: {positionX}");

        if (IsWithinRange(positionX, 296, 424))
        {
            Debug.Log("5X");
            return 5;
        }
         if (IsWithinRange(positionX, 186, 295) || IsWithinRange(positionX, 425, 516))
        {
            Debug.Log("4X");
            return 4;
        }

        if (IsWithinRange(positionX, 101, 186) || IsWithinRange(positionX, 517, 618))
        {
            Debug.Log("3X");
            return 3;
        }

        if (IsWithinRange(positionX, 54, 100) || IsWithinRange(positionX, 619, 706))
        {
            Debug.Log("2X");
            return 2;
        }

        else
        {
            Debug.Log("1X");
            return 1;
        }
    }

    private void UpdateReward(int amount)
    {
        RewardPanel.SetActive(true);
        PlayerPrefsManager.CurrencyUpdate(amount);
        RewardText.text = amount.ToString();
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class CelebrationTextManager : MonoBehaviour
{

    [Space(2)]
    public Text StreakTxt;

    [Space(2)]
    public GameObject[] GoodStatments;

    [Space(2)]
    public GameObject[] BadStatments;

    public Button SaveStreak;

    private void Start()
    {
        SaveStreak.gameObject.SetActive(false);

        SaveStreak.onClick.AddListener(() => WatchVideoGetStreak());
        UpdateStreak();
    }
    public void OnDisable()
    {
       SaveStreak.onClick.RemoveAllListeners();
    }

    #region Streak
    public void UpdateStreak()
    {
        StreakTxt.text = PlayerPrefsManager.StreakUpdate().ToString();

        if (PlayerPrefsManager.StreakUpdate() <= 0)

        {
            StreakTxt.color = Color.red;
            StreakTxt.text = "-";
           //SaveStreak.gameObject.SetActive(true);
        }
        else
        {
            StreakTxt.color = Color.green;
            StreakTxt.text = PlayerPrefsManager.StreakUpdate().ToString();
          //  SaveStreak.gameObject.SetActive(false);
        }

    }

    public void WatchVideoGetStreak()
    {
        PlayerPrefsManager.StreakUpdate(10);
        UpdateStreak();
    }

    #endregion

    public void OnRightAnswer()
    {

        StartCoroutine(nameof(RightAnswer));
    }

    IEnumerator RightAnswer()
    {

        int value = Random.Range(0, GoodStatments.Length - 1);

        GoodStatments[value].SetActive(true);

        yield return new WaitForSeconds(1f);

        GoodStatments[value].SetActive(false);

    }


    public void OnWrongAnswer()
    {
        StartCoroutine(nameof(WrongAnswer));
    }

    IEnumerator WrongAnswer()
    {

        int value = Random.Range(0, BadStatments.Length - 1);

        BadStatments[value].SetActive(true);

        yield return new WaitForSeconds(2f);

        BadStatments[value].SetActive(false);

    }


}

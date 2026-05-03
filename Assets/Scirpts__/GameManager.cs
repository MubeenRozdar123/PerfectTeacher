using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    [Header("References ======")]
    [Space(2)]
    public CameraSwipeLook ref_CameraSwipeLook;
    [Space(2)]
    public TeacherOperations ref_TeacherOperations;
    [Space(2)]
    public LevelManager ref_LevelManager;
    [Space(2)]
    public AudioManager ref_AudioManager;
    [Space(2)]
    public GameObject MainCamera;

    [Space(10)]
    public GameObject GamePlayBtn;

    public GameObject Loading_Screen;

    public static GameManager instance;



    public Image ProgressBarFill;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Start()
    {
       
        ProgressBarFill.fillAmount = 0f;
        ref_AudioManager.MusicOn();

        if (Loading_Screen)
        {
            Loading_Screen.SetActive(false);
        }
        if (GamePlayBtn)
        {
            GamePlayBtn.SetActive(true);
        }
    }

    #region  ButtonClick
    public void LoadNextLevel()
    {
        SceneManager.LoadScene("GamePlay");
    }

    // Restart the current level
    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync("GamePlay");
    }

    // Load the main menu
    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    #endregion
    public void LoadingScreen()
    {
        if (Loading_Screen)
        {
            Loading_Screen.SetActive(true);
        }
    }
    #region Level Complete
    public void GameComplete()
    {

        StartCoroutine(nameof(Complete));
    }
    IEnumerator Complete()
    {
    
       // UIController.Instance.GamePlayUI.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        ref_AudioManager.MusicOf();
        UIController.Instance.GamePlayUI.SetActive(false);
        SceneManager.LoadSceneAsync("Complete");


        if (PlayerPrefsManager.LevelUpdate() < 7)
        {
            PlayerPrefsManager.LevelUpdate(1);

        }
        else
        {
            PlayerPrefsManager.LevelUpdate(-1);
        }




        //if (CompletePanel)
        //{
        //    CompletePanel.SetActive(true);


        //    if (GamePlayBtn)
        //    {
        //        GamePlayBtn.SetActive(false);
        //    }
        //}
    }
    #endregion


 

}

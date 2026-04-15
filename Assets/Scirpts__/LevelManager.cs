
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; // Singleton for global access

    string mainMenuSceneName = "MainMenu";

    public int CurrentLevel = 0; // Tracks the current level index

    public bool IsTestingMode = false;


    [System.Serializable]
    public class LevelData
    {
        public string LevelName;
        public GameObject LevelObjects;
        public string Objective;

    }


    public LevelData[] ref_LevelData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
          
        }
       
    }


    private void Start()
    {
        GameStart();
    }
    public void GameStart()
    {

        if (!IsTestingMode)
        {
            CurrentLevel = PlayerPrefsManager.LevelUpdate();


        }
        else
        {

        }


            LoadLevel(CurrentLevel);
    }

    public void LoadLevel(int level)
    {
        for (int i = 0; i < ref_LevelData.Length; i++)
        {
            ref_LevelData[i].LevelObjects.SetActive(false);
        }

        ref_LevelData[CurrentLevel].LevelObjects.SetActive(true);
        UIController.Instance.EnableObjectivePanel(ref_LevelData[CurrentLevel].Objective);
    }


    public void QuitGame()
    {
       // Debug.Log("Quitting game...");
        Application.Quit();
    }
}

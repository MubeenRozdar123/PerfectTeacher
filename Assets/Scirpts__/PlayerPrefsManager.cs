using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    // Keys for PlayerPrefs
    private const string CurrencyPref = "Cash";
    private const string StreakPref = "Streak";
    private const string LevelPref = "Level";

    #region Generic PlayerPrefs Update/Get Methods

    private static void UpdatePref(string key, int value)
    {
        int currentValue = PlayerPrefs.GetInt(key, 0);
        PlayerPrefs.SetInt(key, Mathf.Max(0, currentValue + value)); // Ensures non-negative values
    }

    private static int GetPref(string key)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, 0);
        }

        int value = PlayerPrefs.GetInt(key);
        if (value < 0)
        {
            PlayerPrefs.SetInt(key, 0); // Reset negative values to 0
            value = 0;
        }

        return value;
    }

    #endregion

    #region Currency Prefs

    public static void CurrencyUpdate(int value)
    {
        UpdatePref(CurrencyPref, value);
    }

    public static int CurrencyUpdate()
    {
        return GetPref(CurrencyPref);
    }

    #endregion

    #region Streak Prefs

    public static void StreakUpdate(int value)
    {
        UpdatePref(StreakPref, value);
    }

    public static int StreakUpdate()
    {
        return GetPref(StreakPref);
    }

    #endregion

    #region Level Prefs

    public static void LevelUpdate(int value)
    {
        UpdatePref(LevelPref, value);
    }

    public static int LevelUpdate()
    {
        return GetPref(LevelPref);
    }

    #endregion
}

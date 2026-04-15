
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUpdate : MonoBehaviour
{
    public void OnEnable()
    {
      transform.GetChild(0).GetComponent<Text>().text = PlayerPrefsManager.CurrencyUpdate().ToString();
    }



}

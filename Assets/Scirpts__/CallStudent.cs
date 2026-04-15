using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallStudent : MonoBehaviour
{
    public Button[] NameBtn;



    public GameObject[] Students;



    public void Start()
    {
        for (int i = 0; i < NameBtn.Length; i++)
        {
            int index = i;
            NameBtn[i].onClick.AddListener(()=>
            {
                //Students[i].SetActive(false);
                StudentDeactive(index);
            });
        }
    }


    public void StudentDeactive(int value)
    {
        Students[value].SetActive(false);
    }

}

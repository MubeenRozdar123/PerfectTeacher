using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomExecution : MonoBehaviour
{
    public GameObject[] AllObjects;


    public void Start()
    {
        InvokeRepeating(nameof(RandomEnable), 5f, 5f);
    }

    public void RandomEnable()
    {

        int rand = Random.Range(0, AllObjects.Length);

        for (int i = 0; i < AllObjects.Length; i++)
        {
          
            AllObjects[i].GetComponent<CheatingStudents>().IsCheating = false;
        }


        AllObjects[rand].GetComponent<CheatingStudents>().IsCheating = true;

        AllObjects[rand].GetComponent<CheatingStudents>().StartCheating();
    }

}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class LunchBoxChecking : MonoBehaviour
{

    public GameObject CineMachineCamras;

    public GameObject PhoneInLunchBox;

    public GameObject LunchBox;
   
    
    public UnityEvent OnEnableCameras , OnDisableCameras;

    public Animator ref_LunchBoxAnimator;


    CharacterMechanics ref_CharacterMechanics;
    

    public void OnTriggerEnter(Collider other)
    {



        if (other.gameObject.CompareTag("Player"))
        {

            ref_CharacterMechanics = other.GetComponent<CharacterMechanics>();

            if(ref_CharacterMechanics != null  && ref_CharacterMechanics.IsAnswerTrue)
            {
                PhoneInLunchBox.SetActive(false);
            }

            else
            {
                PhoneInLunchBox.SetActive(true);

            }


            LunchBox.SetActive(true);

            EnableCameras();



        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            LunchBox.SetActive(false);

        }
    }


    public void Start()
    {
        if(LunchBox)
        {
            LunchBox.SetActive(false);

        }


        LunchBox.SetActive(false);
        CineMachineCamras.SetActive(false);
    }



    public void EnableCameras()
    {
       StartCoroutine(nameof(Enable_Disable));
    }


    IEnumerator Enable_Disable()
    {
       

        yield return new WaitForSeconds(1.5f);

        CineMachineCamras.SetActive(true);

        OnEnableCameras.Invoke();

        ref_LunchBoxAnimator.enabled = true;

        yield return new WaitForSeconds(3f);

      //  ref_LunchBoxAnimator.SetTrigger("Close");

        CineMachineCamras.SetActive(false);

        yield return new WaitForSeconds(1f);

        OnDisableCameras.Invoke();
    }
   

}

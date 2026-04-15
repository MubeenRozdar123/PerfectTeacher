
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class LabChecking : MonoBehaviour
{
    public GameObject LabCkeckingPop;

    public Text Glasses, Mask;

    CharacterMechanics ref_characterMovement;


    public GameObject MaskCheck, MaskCross, GlassesCheck, GlassesCross;


    public string[] HavingGlasses;
    public string[] HavingMask;

    int counter = 0;

    public void Start()
    {
        counter = 0;
        LabCkeckingPop.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LabCkeckingPop.SetActive(true);

          

            ref_characterMovement = other.gameObject.GetComponent<CharacterMechanics>();

            if (ref_characterMovement && ref_characterMovement.IsAnswerTrue)
            {
                Glasses.text = "Yes";
                Mask.text = "Yes";

                ActiveCheckBox();
            }
            else
            {
                Glasses.text = HavingGlasses[counter].ToString();
                Mask.text = HavingMask[counter].ToString();
                ActiveCheckBox();
              



            }

            counter++;
        }
    }

    public void ActiveCheckBox()
    {
        if (Glasses.text == "Yes")
        {
            GlassesCheck.SetActive(true);
            GlassesCross.SetActive(false);
        }
        else
        {
            GlassesCheck.SetActive(false);
            GlassesCross.SetActive(true);
        }
        if (Mask.text == "Yes")
        {
            MaskCheck.SetActive(true);
            MaskCross.SetActive(false);
        }
        else
        {
            MaskCheck.SetActive(false);
            MaskCross.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LabCkeckingPop.SetActive(false);
        }
    }
}

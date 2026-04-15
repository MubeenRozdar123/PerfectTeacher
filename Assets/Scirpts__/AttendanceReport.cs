using UnityEngine;
using UnityEngine.UI;


public class AttendanceReport : MonoBehaviour
{

    public GameObject AttendancePop;
    public Text PresentDay, AbsentDay;
    CharacterMechanics ref_characterMovement;


 


    public int[] Present_Day;
    public int[] Absent_Day;

    int counter = 0;

    public void Start()
    {
        counter = 0;
        AttendancePop.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AttendancePop.SetActive(true);

            counter++;

            ref_characterMovement = other.gameObject.GetComponent<CharacterMechanics>();

            if (ref_characterMovement && ref_characterMovement.IsAnswerTrue)
            {
                PresentDay.text = Present_Day[counter].ToString();
                AbsentDay.text = Absent_Day[counter].ToString();
            }

            else
            {
                PresentDay.text = Present_Day[counter].ToString();
                AbsentDay.text = Absent_Day[counter].ToString();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AttendancePop.SetActive(false);
        }
    }
}

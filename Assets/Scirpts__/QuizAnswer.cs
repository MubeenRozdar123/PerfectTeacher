using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuizAnswer : MonoBehaviour
{

    public ClassQuiz ref_ClassQuiz;
    public GameObject QuizPop;
    public bool Quiz;
    public UnityEvent OnCorrectAnswer;
    public Text ShowText;

    public string RightText, WrongText;

    Animator Ref_Animator;

    public void Start()
    {
        QuizPop.SetActive(false);
        Ref_Animator = GetComponent<Animator>();
        if (Ref_Animator != null)
        {
            Ref_Animator.SetBool("IsIdle", true);
            Ref_Animator.SetBool("HandDown", false);
        }

    }

    public void GiveAnswer()
    {

        if (Quiz)
        {
           

            ShowText.text = RightText;
            StartCoroutine(nameof(Delay));
        }
        else
        {
            ShowText.text = WrongText;

        }

        if (Ref_Animator != null)
        {
            Ref_Animator.SetBool("IsIdle", false);
            Ref_Animator.SetBool("HandDown", true);
        }

        StartCoroutine(nameof(AutoEnablePop));
    }


    IEnumerator AutoEnablePop()
    {
        QuizPop.SetActive(true);
        yield return new WaitForSeconds(2f);
        QuizPop.SetActive(false);
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        OnCorrectAnswer.Invoke();
    }

}

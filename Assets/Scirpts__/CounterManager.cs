using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CounterManager : MonoBehaviour
{

    public int Counter;
    public int ShowComplete;
    public UnityEvent OnComplete;
    public UnityEvent OnCounter;
    public void Start()
    {
        Counter = 0;
    }

    public void CounterMethod()
    {
        OnCounter.Invoke();

        if (Counter >= ShowComplete)
        {
            OnComplete.Invoke();
            GameManager.instance.GameComplete();
        }
        Counter++;
    }
}

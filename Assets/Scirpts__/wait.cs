using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class wait : MonoBehaviour
{
   

    [Space]
    public float OnEnabledelay;
    public float TriggerDelay;
    public float DisableTime;

    public UnityEvent  Enable , Disable , TriggerEnter , TriggerExit , TriggerStay  , CollisionEnter , CollisionStay  , CollisionExit , OnStart;

    public void OnEnable()
    {
        Invoke("OnEnableDelay", OnEnabledelay);
    }
    public void start()
    {
       OnStart.Invoke();
    }

    public void OnEnableDelay()
    {
        Enable.Invoke();
        Invoke("OnDisbleDelay", DisableTime);

    }

    public void OnDisable()
    {
        Invoke("OnDisbleDelay", DisableTime);
    }
    public void OnDisbleDelay()
    {
        Disable.Invoke();
    }


    public void OnTriggerEnter(Collider other)
    {
        Invoke("OntriggerDelay", TriggerDelay);
    }

    public void OntriggerDelay()
    {
        TriggerEnter.Invoke();
    }


   
}

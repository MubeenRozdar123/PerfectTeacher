using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class General : MonoBehaviour
{
    public string ObjectTag = "Player";
  //  public string OtherObject = "None";

    [HideInInspector]
    public GameObject line;


   

    [Space]
    public UnityEvent TriggerEnter;
    public UnityEvent TriggerExit;
    public UnityEvent TriggerStay;
    public UnityEvent CollisionEnter , CollisionExit , CollisionStay;
    public UnityEvent Enable;
    public UnityEvent Disable;
    

    public void OnEnable()
    {
        Enable.Invoke();
    }

    public void OnDisable()
    {
        Disable.Invoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(ObjectTag) )
        {
            TriggerEnter.Invoke();
        }
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(ObjectTag ) )
        {
            TriggerStay.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(ObjectTag) )
        {
            TriggerExit.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag (ObjectTag) )
        {
            CollisionEnter.Invoke();
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(ObjectTag) )
        {
            CollisionStay.Invoke();
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(ObjectTag) )
        {
            CollisionExit.Invoke();
        }

       
    }




  
}

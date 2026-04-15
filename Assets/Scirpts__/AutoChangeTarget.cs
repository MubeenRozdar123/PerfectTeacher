
using UnityEngine;

public class AutoChangeTarget : MonoBehaviour
{
    public CharacterMechanics ref_characterMovement;
    public Transform target;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          

            ref_characterMovement = other.gameObject.GetComponent<CharacterMechanics>();

            if (ref_characterMovement )
            {
                ref_characterMovement.AutoChangeTarget(target);
            }
           
        }
    }

    public void OnTriggerExit(Collider other)
    {
      
    }
}

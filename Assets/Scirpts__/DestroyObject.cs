using UnityEngine;
using UnityEngine.Events;

public class DestroyObject : MonoBehaviour
{

    public UnityEvent Counter;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Counter.Invoke();
            Destroy(other.gameObject);

        }
    }
}

using System.Collections;
using UnityEngine;

public class CheatingStudents : MonoBehaviour
{
    public GameObject PoPUp;
    public GameObject MainObject;

    public float moveSpeed = 5f;

    public bool IsCheating = false;
    public bool IsMove = false;
    public bool Move_Left = false;

    private int counter;

    void Start()
    {
        PoPUp.SetActive(false);
        counter = 0;
        // Optionally schedule cheating to start
        // Invoke(nameof(StartCheating), Random.Range(2, 10));
    }

    void Update()
    {
        if (IsMove)
        {
            Move(Move_Left ? Vector3.left : Vector3.right);
        }
    }

    private void Move(Vector3 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    public void StartCheating()
    {
        if (IsCheating)
        {
            StartCoroutine(nameof(CheatingCoroutine));
        }
    }

    private IEnumerator CheatingCoroutine()
    {
        SetCheatingState(true);

        yield return new WaitForSeconds(Random.Range(2, 6));

        SetCheatingState(false);
    }

    public void StartCheatingAnimation()
    {
        SetCheatingState(true);
    }

    public void StopCheatingAniamtion()
    {
        SetCheatingState(false);
    }

    public void CheatingCaught()
    {
       // Debug.Log("Caught Cheating!");
        SetCheatingState(false);

        LevelProgressBar.Instance.MaxValue = 2f;
        LevelProgressBar.Instance.FillProgressBar();

        IsMove = true;
        MainObject.SetActive(false);
    }

    private void SetCheatingState(bool state)
    {
        IsCheating = state;
        PoPUp.SetActive(state);
    }

}

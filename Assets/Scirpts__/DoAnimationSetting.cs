using DG.Tweening;
using UnityEngine;

public class DoAnimationSetting : MonoBehaviour
{

    public DOTweenAnimation RefDotween;

    private void Start()
    {

        RefDotween = GetComponent<DOTweenAnimation>();
;
    }

   
    public void DoEnable()
    {
        if (RefDotween)
        {
            RefDotween.DOPlayForward(); 
        }
    }


    public void DoDisable()
    {
        if (RefDotween)
        {
            RefDotween.DOPlayBackwards(); 
        }
    }


}

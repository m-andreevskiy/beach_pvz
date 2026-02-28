using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TutorialPopupBase : MonoBehaviour
{

    [SerializeField] private float upcomingWaitTime;

    private Animator animator;



    public float GetWaitTime()
    {
        return upcomingWaitTime;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void Disappear()
    {
        animator.Play("PopupDisappear");
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    // void OnDisable()
    // {
    //     Debug.Log("popup disabled");
    //     animator.Play("PopupDisappear");
    // }

    public void Appear()
    {
        animator.Play("PopupAppear");
    }

    // void OnEnable()
    // {
        // animator.Play("PopupAppear");
    // }

}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class TransitionScript : MonoBehaviour
{
    enum ANIMATION_TRIGGER
    {
        TRANSITION
    }
    Animator animator;
    Animator Animator
    {
        get
        {
            return animator ?? (animator = GetComponent<Animator>());
        }
    }
    private void Awake()
    {
        SingletonUtil.SetInstance(this);
    }
    public void Transition()
    {
        Animator.SetTrigger(ANIMATION_TRIGGER.TRANSITION.ToString());
    }
}

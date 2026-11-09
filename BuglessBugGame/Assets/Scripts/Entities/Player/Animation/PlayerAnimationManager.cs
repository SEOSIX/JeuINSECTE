using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator plAnimator;

    private static readonly int IsRunHash = Animator.StringToHash("isRun");
    private static readonly int IsCatchHash = Animator.StringToHash("isCatch");

    private void Start()
    {
        plAnimator = GetComponentInChildren<Animator>();
    }

    public void SetWalk()
    {
        if (plAnimator == null) return;
        plAnimator.SetBool(IsRunHash, true);
    }

    public void SetIdle()
    {
        if (plAnimator == null) return;
        plAnimator.SetBool(IsRunHash, false);
    }

    public void SetCatch()
    {
        if (plAnimator == null) return;
        plAnimator.SetBool(IsRunHash, false);
        plAnimator.SetTrigger(IsCatchHash);
    }
}
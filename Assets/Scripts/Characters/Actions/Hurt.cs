using UnityEngine;

public class Hurt : CharAction
{
    public override string GetNextAction(InputPackage input)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            return input.action;
        }
        return "";
    }

    public override void Enter(InputPackage input) {
        animator.SetTrigger("hurt");
    }
    public override void Exit() { }

    public override void Process(InputPackage input) {
        //print(input.movedir);
    }
}

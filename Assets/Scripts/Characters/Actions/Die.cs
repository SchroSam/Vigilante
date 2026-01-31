using UnityEngine;

public class Die : CharAction
{
    public override string GetNextAction(InputPackage input)
    {
        return "";
    }

    public override void Enter(InputPackage input)
    {
        animator.SetTrigger("die");
        CameraBehavior.RemoveFromLockables(behavior.transform);
    }
    public override void Exit() { }

    public override void Process(InputPackage input) {
        //print(input.movedir);
    }
}

using UnityEngine;

public class Die : CharAction
{
    public override string GetNextAction(InputPackage input)
    {
        return "Idle";
    }

    public override void Enter(InputPackage input)
    {
        animator.SetTrigger("die");
    }
    public override void Exit() { }

    public override void Process(InputPackage input) {
        //print(input.movedir);
    }
}

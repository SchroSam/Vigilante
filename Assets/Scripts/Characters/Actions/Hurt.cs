using UnityEngine;

public class Hurt : CharAction
{
    public override string GetNextAction(InputPackage input)
    {
        return "Idle";
    }

    public override void Enter(InputPackage input) {
        animator.SetTrigger("hurt");
    }
    public override void Exit() { }

    public override void Process(InputPackage input) {
        //print(input.movedir);
    }
}

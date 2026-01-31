using UnityEngine;

public class IdleCharAction : CharAction
{
    public override string GetNextAction(InputPackage input)
    {
        if (input.action != "Idle") return input.action;
        return "";
    }

    public override void Enter(InputPackage input) { }
    public override void Exit() { }

    public override void Process(InputPackage input) {
        //print("idle");
    }
}

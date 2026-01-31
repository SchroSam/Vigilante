using UnityEngine;

public class IdleCharAction : CharAction
{
    public override string GetNextAction(InputPackage input)
    {
        if (input.action == "Move") return "Move";
        return "";
    }

    public override void Enter() { }
    public override void Exit() { }

    public override void Process(InputPackage input) {
        //print("idle");
    }
}

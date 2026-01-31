using UnityEngine;

public class Move : CharAction
{
    public override string GetNextAction(InputPackage input)
    {
        return "";
    }

    public override void Enter() { }
    public override void Exit() { }

    public override void Process(InputPackage input) {
        print(input.movedir);
    }
}

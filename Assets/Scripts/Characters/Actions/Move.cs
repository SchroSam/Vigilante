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
        //print(input.movedir);
        controller.Move(new Vector3(input.movedir.x, 0, input.movedir.y) * Time.fixedDeltaTime);
    }
}

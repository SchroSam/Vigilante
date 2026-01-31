using UnityEngine;

public class Attack : CharAction
{
    public override string GetNextAction(InputPackage input)
    {
        if (input.action != "Attack") return input.action;
        return "";
    }

    public override void Enter() { }
    public override void Exit() { }

    public override void Process(InputPackage input) {
        //print(input.movedir);
        print(input.strikedir);
    }
}

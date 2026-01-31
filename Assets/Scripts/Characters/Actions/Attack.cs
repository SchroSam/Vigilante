using UnityEngine;

public class Attack : CharAction
{
    public override string GetNextAction(InputPackage input)
    {
        if (input.action != "Attack") return input.action;
        return "";
    }

    public override void Enter(InputPackage input)
    {
        animator.SetBool("attack", true);
        animator.SetFloat("strikeV", input.strikedir.y);
        animator.SetFloat("strikeH", input.strikedir.x);

    }
    public override void Exit()
    {
        animator.SetBool("attack", false);
    }

    public override void Process(InputPackage input)
    {
        //print(input.movedir);
    }
}

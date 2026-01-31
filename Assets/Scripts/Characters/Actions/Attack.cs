using UnityEngine;

public class Attack : CharAction
{
    public override string GetNextAction(InputPackage input)
    {
        if (input.action != "Attack") return input.action;
        return "";
    }

    public override void Enter()
    {
        animator.SetBool("attack", true);

    }
    public override void Exit()
    {
        animator.SetBool("attack", false);
    }

    public override void Process(InputPackage input)
    {
        animator.SetFloat("strikeV", input.strikedir.y);
        animator.SetFloat("strikeH", input.strikedir.x);
        //print(input.movedir);
        print(input.strikedir);
    }
}

using UnityEngine;

public class Block : CharAction
{
    public override string GetNextAction(InputPackage input)
    {
        if (input.action != "Block") return input.action;
        return "";
    }

    public override void Enter()
    {
        animator.SetBool("block", true);

    }
    public override void Exit() {
        animator.SetBool("block", false);
    }

    public override void Process(InputPackage input)
    {
        animator.SetFloat("strikeV", input.strikedir.y);
        animator.SetFloat("strikeH", input.strikedir.x);
        //print(input.movedir);
    }
}

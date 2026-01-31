using UnityEngine;

public class Block : CharAction
{
    public override string GetNextAction(InputPackage input)
    {
        if (input.action != "Block") return input.action;
        return "";
    }

    public override void Enter(InputPackage input)
    {
        animator.SetBool("block", true);
        animator.SetFloat("strikeV", input.strikedir.y);
        animator.SetFloat("strikeH", input.strikedir.x);
        behavior.blocking = true;
        behavior.dir = input.strikedir;

    }
    public override void Exit() {
        animator.SetBool("block", false);
        behavior.blocking = false;
        behavior.dir = Vector2Int.zero;
    }

    public override void Process(InputPackage input)
    {
        //print(input.movedir);
    }
}

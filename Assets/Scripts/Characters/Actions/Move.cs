using UnityEngine;

public class Move : CharAction
{
    public override string GetNextAction(InputPackage input)
    {
        if (input.action != "Move") return input.action;
        return "";
    }

    public override void Enter(InputPackage input)
    {
        animator.SetBool("move", true);

    }
    public override void Exit()
    {
        animator.SetBool("move", false);
    }

    public override void Process(InputPackage input) {
        //print(input.movedir);
        controller.Move(new Vector3(input.movedir.x, 0, input.movedir.y) * Time.fixedDeltaTime);
        animator.SetFloat("moveV", input.movedir.y);
        animator.SetFloat("moveH", input.movedir.x);
    }
}

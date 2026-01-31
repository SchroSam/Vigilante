using UnityEngine;

public class Attack : CharAction
{
    int repeat = 1; //amount of times this action's animation has repeated, used for transitioning out when animation is done

    public override string GetNextAction(InputPackage input)
    {
        print(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 * repeat && !animator.IsInTransition(0))
        {
            repeat += 1;
            if (input.action != "Attack") repeat = 1;
            return input.action;
        }
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

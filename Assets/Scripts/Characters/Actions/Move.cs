using UnityEngine;

public class Move : CharAction
{
    public float speed = 10f;
    public float groundSnapDistance = 10f;

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
        controller.Move(((transform.forward * input.movedir.y) + (transform.right * input.movedir.x)) * speed * Time.fixedDeltaTime);
        animator.SetFloat("moveV", input.movedir.y);
        animator.SetFloat("moveH", input.movedir.x);

        // Raycast to snap to ground directly below
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundSnapDistance))
        {
            Vector3 snapPosition = hit.point;
            controller.Move(snapPosition - transform.position);
        }
    }
}

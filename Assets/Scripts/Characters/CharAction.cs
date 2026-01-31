using UnityEngine;

public class CharAction : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public virtual string GetNextAction(InputPackage input)
    {
        return "";
    }

    public virtual void Enter(InputPackage input) { }
    public virtual void Exit() { }
    public virtual void Process(InputPackage input) { }
}

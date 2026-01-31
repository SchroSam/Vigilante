using UnityEngine;

public class CharAction : MonoBehaviour
{
    public virtual string GetNextAction(InputPackage input)
    {
        return "";
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Process(InputPackage input) { }
}

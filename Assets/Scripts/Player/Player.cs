using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterBehavior behavior;
    public PlayerInput pinput;
    InputPackage input;
    public Transform cam;

    private void Update()
    {
        input = pinput.GetInput();
        transform.forward = (cam.forward - new Vector3(0, cam.forward.y)).normalized;
    }

    void FixedUpdate()
    {
        if (input.debugDie) behavior.forcedAct = "Die";
        if (input.debugReset) behavior.Reset();
        behavior.Process(input);
    }
}
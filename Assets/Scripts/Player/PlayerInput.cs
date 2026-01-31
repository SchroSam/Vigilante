using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    InputAction move;
    InputAction iHit, jHit, kHit, lHit;
    InputAction attack;

    private void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        attack = InputSystem.actions.FindAction("Attack");
        iHit = InputSystem.actions.FindAction("iHit");
        jHit = InputSystem.actions.FindAction("jHit");
        kHit = InputSystem.actions.FindAction("kHit");
        lHit = InputSystem.actions.FindAction("lHit");
    }

    public InputPackage GetInput()
    {
        InputPackage package = new InputPackage();
        package.movedir = move.ReadValue<Vector2>();
        if (package.movedir != Vector2.zero) {
            package.action = "Move";
        } else
        {
            package.action = "Idle";
        }
        bool attacking = attack.IsPressed();
        if (iHit.WasPressedThisFrame())
        {
            package.action = attacking ? "IAttack" : "IBlock";
        }
        else if (jHit.WasPressedThisFrame())
        {
            package.action = attacking ? "JAttack" : "JBlock";
        }
        else if (kHit.WasPressedThisFrame())
        {
            package.action = attacking ? "KAttack" : "KBlock";
        }
        else if (lHit.WasPressedThisFrame())
        {
            package.action = attacking ? "LAttack" : "LBlock";
        }
        return package;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    InputAction move;
    InputAction iHit, jHit, kHit, lHit;
    InputAction attack;

    InputAction debugDie;

    private void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        attack = InputSystem.actions.FindAction("Attack");
        iHit = InputSystem.actions.FindAction("iHit");
        jHit = InputSystem.actions.FindAction("jHit");
        kHit = InputSystem.actions.FindAction("kHit");
        lHit = InputSystem.actions.FindAction("lHit");
        debugDie = InputSystem.actions.FindAction("debugDie");
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
        if (iHit.IsPressed())
        {
            package.action = attacking ? "Attack" : "Block";
            package.strikedir = Vector2Int.up;
        }
        else if (jHit.IsPressed())
        {
            package.action = attacking ? "Attack" : "Block";
            package.strikedir = Vector2Int.left;
        }
        else if (kHit.IsPressed())
        {
            package.action = attacking ? "Attack" : "Block";
            package.strikedir = Vector2Int.down;
        }
        else if (lHit.IsPressed())
        {
            package.action = attacking ? "Attack" : "Block";
            package.strikedir = Vector2Int.right;
        }
        if (debugDie.IsPressed()) package.debugDie = true;
        //print(package.action);
        return package;
    }
}

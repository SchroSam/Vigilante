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
        bool attacking = !attack.IsPressed();
        print(attacking);
        return package;
    }
}

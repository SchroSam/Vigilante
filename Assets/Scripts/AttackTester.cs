using UnityEngine;

public class AttackTester : MonoBehaviour
{
    public CharacterBehavior behavior;

    // Update is called once per frame
    void FixedUpdate()
    {
        InputPackage attack = new InputPackage();
        attack.action = "Attack";
        attack.strikedir = Vector2Int.left;
        behavior.Process(attack);
    }
}

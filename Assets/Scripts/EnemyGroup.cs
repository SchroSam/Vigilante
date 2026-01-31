using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public CharacterBehavior[] enemies;

    void Start()
    {
        enemies = GetComponentsInChildren<CharacterBehavior>();
        EnterCombat();
    }

    public void EnterCombat()
    {
        foreach (CharacterBehavior e in enemies)
        {
            CameraBehavior.lockables.Add(e.transform);
        }
    }
}

using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public CharacterBehavior[] enemies;

    void Start()
    {
        enemies = GetComponentsInChildren<CharacterBehavior>(true);
    }

    public void Spawn()
    {
        print("Spawning");
        gameObject.SetActive(true);
        foreach (CharacterBehavior e in enemies)
        {
            CameraBehavior.lockables.Add(e.transform);
        }
    }
}

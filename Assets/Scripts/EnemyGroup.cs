using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public Transform[] enemies;

    void Start()
    {
        enemies = GetComponentsInChildren<Transform>();
        EnterCombat();
    }

    public void EnterCombat()
    {
        foreach (Transform e in enemies)
        {
            if (e != transform) CameraBehavior.lockables.Add(e);
        }
    }
}

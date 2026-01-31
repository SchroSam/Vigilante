using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public CharacterBehavior behavior;

    private void OnTriggerEnter(Collider other)
    {
        print(other);
        if (!behavior.objectsHit.Contains(other))
        {
            behavior.objectsHit.Add(other);
        }
    }
}

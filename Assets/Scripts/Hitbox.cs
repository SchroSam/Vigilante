using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public CharacterBehavior behavior;
    public bool canHit;

    private void OnTriggerEnter(Collider other)
    {
        if (!canHit) return;
        print("Colliding with " + other);
        print("Contains: " + behavior.objectsHit.Contains(other));
        if (!behavior.objectsHit.Contains(other))
        {
            behavior.AddToObjectsHit(other);
            CharacterBehavior oBeh = other.GetComponent<CharacterBehavior>();
            HitInfo h = new HitInfo();
            h.source = behavior;
            h.dir = behavior.dir;
            h.dmg = 15;
            oBeh.HandleHit(h);
        }
    }
}

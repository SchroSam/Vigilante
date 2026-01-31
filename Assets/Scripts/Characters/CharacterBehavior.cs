using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    public void Process(InputPackage input)
    {
        print(input.movedir);
    }
}

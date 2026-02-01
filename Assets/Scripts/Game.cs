using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    public float timer = 180f;
    public GameObject groupsContainer;
    //List<CharacterBehavior> enemies = new List<CharacterBehavior>();

    public GameUI gameUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<GroupManager> groups = new List<GroupManager>(groupsContainer.GetComponentsInChildren<GroupManager>(true));
        for (int i = 3; i > 0 && i >= groups.Count; i--)
        {
            int chosen = Random.Range(0, groups.Count-1);
            groups[chosen].Spawn();
            groups.RemoveAt(chosen);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        gameUI.UpdateTimer(timer);
    }
}

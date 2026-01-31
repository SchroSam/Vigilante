using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{

    public List<Nav> members;
    public Nav activeFighter;

    void Start()
    {
        foreach (Transform child in transform) 
        { 
            members.Add(child.GetComponent<Nav>());
        }
    }

    public void AssignAttacker()
    {
        bool fightingAlready = false;

        for(int i = 0; i < members.Count; i++)
        {
            if(members[i].state == Nav.E_State.Fight)
            {
                activeFighter = members[i];
                fightingAlready = true;
                break;
            }
        }

        if(!fightingAlready){

            members[0].StateChange(Nav.E_State.Fight);
            activeFighter = members[0];
            
            for(int i = 1; i < members.Count; i++)
            {
                members[i].StateChange(Nav.E_State.Waiting);
            }
        }

        else
        {
            activeFighter.StateChange(Nav.E_State.Waiting);

            activeFighter = members[Random.Range(0, members.Count - 1)];

            activeFighter.StateChange(Nav.E_State.Fight);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

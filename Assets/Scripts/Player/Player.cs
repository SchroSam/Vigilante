using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterBehavior behavior;
    public PlayerInput pinput;
    InputPackage input;
    public Transform cam;
    public GameObject currentGroup;

    private void Update()
    {
        input = pinput.GetInput();
        transform.forward = (cam.forward - new Vector3(0, cam.forward.y)).normalized;
    }

    void FixedUpdate()
    {
        if (input.debugDie) behavior.forcedAct = "Die";
        if (input.debugReset) behavior.Reset();
        behavior.Process(input);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Group")
        {
            currentGroup = other.gameObject;
            foreach (Transform child in other.transform) 
            { 
                Debug.Log(child.gameObject.name);
                child.GetComponent<Nav>().StateChange(Nav.E_State.Seek); 
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Group")
        {
            currentGroup = null;
            foreach (Transform child in other.transform) 
            { 
                Debug.Log(child.gameObject.name);
                child.GetComponent<Nav>().StateChange(Nav.E_State.Idle); 
            }
            
        }
    }
}
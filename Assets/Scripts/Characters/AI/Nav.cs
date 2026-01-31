using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Nav : MonoBehaviour
{
    [System.Serializable]
    public enum E_State{Idle, Seek, Fight, Die}

    NavMeshAgent m_Agent;
    CharacterBehavior behavior;
    private GameObject player;
    InputPackage nextInput;
    Vector3 movDir3;
    public GameObject pusher;
    bool pushed = false;

    public float pushDist = 0.3f;
    public float pushReduce = 2f;

    
    public E_State state = E_State.Idle;

    void Start()
    {
        player = FindFirstObjectByType<Player>().gameObject;
        behavior = GetComponent<CharacterBehavior>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == E_State.Seek)
        {
            //Debug.Log($"x:{m_Agent.steeringTarget.x} z:{m_Agent.steeringTarget.z}");
            if(m_Agent.steeringTarget != null && !pushed)
                movDir3 = (m_Agent.steeringTarget - new Vector3(transform.position.x, transform.position.y, transform.position.z)).normalized;
            else if (pushed)
                movDir3 = (transform.position - pusher.transform.position).normalized + m_Agent.steeringTarget.normalized * pushReduce;

            nextInput.movedir.x = movDir3.x;
            nextInput.movedir.y = movDir3.z;

            nextInput.action = "Move";

            behavior.Process(nextInput);

            m_Agent.destination = player.transform.position;

            if(pushed && Vector3.Distance(transform.position, pusher.transform.position) > pushDist)
            {
                pushed = false;
            }
        }
    }

    public void StateChange(E_State nextState)
    {
        if(state == E_State.Seek)
        {
            movDir3 = Vector3.zero;
        }

        state = nextState;
    }


    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Wall")
        {
            pusher = other.gameObject;
            pushed = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            pusher = collision.gameObject;
            pushed = true;
        }
    }


}

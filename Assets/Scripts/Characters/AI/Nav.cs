using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Nav : MonoBehaviour
{
    [System.Serializable]
    public enum E_State{Idle, Seek, Fight, Die, Waiting}

    NavMeshAgent m_Agent;
    CharacterBehavior behavior;
    private GameObject player;
    InputPackage nextInput;
    Vector3 movDir3;
    public GroupManager group;
    private GameObject pusher;
    

    bool pushed = false;
    public float pushDist = 0.3f;
    public float pushReduce = 2f;

    bool engaging = false;
    bool striking = false;
    public float attackRange = 0.3f;

    
    public E_State state = E_State.Idle;

    void Start()
    {
        group = transform.parent.GetComponent<GroupManager>();
        player = FindFirstObjectByType<Player>().gameObject;
        behavior = GetComponent<CharacterBehavior>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.isStopped = true;
    }

    // Update is called once per frame
    void FixedUpdate()
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

        else if(state == E_State.Waiting)
        {
            //add propper waiting behavior
            nextInput.action = "Block";

        }

        else if(state == E_State.Fight)
        {
            if(Vector3.Distance(transform.position, player.transform.position) > attackRange && !striking)
            {
                nextInput.action = "Move";

                if(m_Agent.steeringTarget != null && !pushed)
                    movDir3 = (m_Agent.steeringTarget - new Vector3(transform.position.x, transform.position.y, transform.position.z)).normalized;
                else if (pushed)
                    movDir3 = (transform.position - pusher.transform.position).normalized + m_Agent.steeringTarget.normalized * pushReduce;

                if(pushed && Vector3.Distance(transform.position, pusher.transform.position) > pushDist)
                {
                    pushed = false;
                }
            }

            else if(Vector3.Distance(transform.position, player.transform.position) <= attackRange && !striking)
            {
                nextInput.action = "Attack";
                
            }




        }
    }

    public void StateChange(E_State nextState)
    {
        if(state == E_State.Seek)
        {
            movDir3 = Vector3.zero;
            nextInput.movedir = Vector2.zero;
            nextInput.action = "Idle";

            behavior.Process(nextInput);
        }

        state = nextState;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "FightRadius")
        {
            group.AssignAttacker();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.name == "FightRadius")
        {
            StateChange(E_State.Seek);
        }
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

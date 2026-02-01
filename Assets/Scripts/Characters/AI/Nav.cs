using Unity.VisualScripting;
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
    public bool striking = false;
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
        if(state == E_State.Idle)
        {
            behavior.SetForcedAct("Idle");
            nextInput.action = "Idle";
            behavior.Process(nextInput);
        }

        else if(state == E_State.Seek)
        {
            if(m_Agent.steeringTarget != null && !pushed)
            {
                movDir3 = (m_Agent.steeringTarget - new Vector3(transform.position.x, transform.position.y, transform.position.z)).normalized;
                // movDir3.x = -movDir3.x;
                // movDir3.z = -movDir3.z;
            }
            else if (pushed)
                movDir3 = (transform.position - pusher.transform.position).normalized + m_Agent.steeringTarget.normalized * pushReduce;

            nextInput.movedir.x = -movDir3.x;
            nextInput.movedir.y = -movDir3.z;

            nextInput.action = "Move";

            behavior.Process(nextInput);

            m_Agent.destination = player.transform.position;
            transform.LookAt(player.transform);

            if(pushed && Vector3.Distance(transform.position, pusher.transform.position) > pushDist)
            {
                pushed = false;
            }
        }

        else if(state == E_State.Waiting)
        {
            if(Vector3.Distance(player.transform.position, transform.position) > attackRange)
            {
                nextInput.action = "Idle";
                behavior.Process(nextInput);
            }
            else
            {
                transform.LookAt(player.transform.position);
                swing();
            }

        }

        else if(state == E_State.Fight)
        {
            if(Vector3.Distance(transform.position, player.transform.position) > attackRange && !striking)
            {
                transform.LookAt(player.transform);
                //Debug.LogWarning("Engaging player!");

                nextInput.action = "Move";

                if(m_Agent.steeringTarget != null && !pushed)
                {
                    movDir3 = (m_Agent.steeringTarget - new Vector3(transform.position.x, transform.position.y, transform.position.z)).normalized;
                    // movDir3.x = -movDir3.x;
                    // movDir3.z = -movDir3.z;
                }
                else if (pushed)
                    movDir3 = (transform.position - pusher.transform.position).normalized + m_Agent.steeringTarget.normalized * pushReduce;

                nextInput.movedir.x = -movDir3.x;
                nextInput.movedir.y = -movDir3.z;

                behavior.Process(nextInput);

                if(pushed && Vector3.Distance(transform.position, pusher.transform.position) > pushDist)
                {
                    pushed = false;
                }
            }

            else if(Vector3.Distance(transform.position, player.transform.position) <= attackRange && !striking)
            {
                //Debug.LogWarning("Making a swing!");
                swing();

                //Fight Radius check
                if(Vector3.Distance(transform.position, player.transform.position) <= 4.14f)
                {
                    
                    group.AssignAttacker();
                }
                else
                {
                    foreach(Nav member in group.members)
                    {
                        member.state = E_State.Seek;
                    }
                }

                striking = false;
            }

            // else if (striking)
            // {
                
            // }


        }
    }

    public void swing()
    {
        striking = true;

        nextInput.movedir = Vector2.zero;
        nextInput.action = "Attack";

        int attackType = Random.Range(0, 4);

        switch (attackType)
        {
            case 0:
                nextInput.strikedir = Vector2Int.left;

            break;

            case 1:
                nextInput.strikedir = Vector2Int.right;

            break;

            case 2:
                nextInput.strikedir = Vector2Int.up;

            break;

            case 3:
                nextInput.strikedir = Vector2Int.down;

            break;
        }

        behavior.Process(nextInput);
    }

    public void chanceToBlock()
    {
        if(Random.Range(0, 2) == 1)
        {
            nextInput.action = "Block";

            int blockType = Random.Range(0, 4);

            switch (blockType)
            {
                case 0:
                    nextInput.strikedir = Vector2Int.left;

                break;

                case 1:
                    nextInput.strikedir = Vector2Int.right;

                break;

                case 2:
                    nextInput.strikedir = Vector2Int.up;

                break;

                case 3:
                    nextInput.strikedir = Vector2Int.down;

                break;
            }

            behavior.Process(nextInput);
        }
    }

    public void StateChange(E_State nextState)
    {
        //Debug.LogWarning($"Changing from {state}, to {nextState}");

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
            //Debug.LogWarning($"Entered FightRadius");
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

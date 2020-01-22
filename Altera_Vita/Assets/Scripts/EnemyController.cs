using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public Vector3 startingPosition = Vector3.zero;
    public Quaternion startingRotation = Quaternion.identity;
    public GameObject rifle;

    public GameObject target = null;
    public Vector3 last_location = Vector3.zero;

    public Shot_Collector shotCollector;

    public AIPercieve percieve;
    public AIPerceptionManager perception_manager;

    private PlayerController player;
    private Animator animator;
    public NavMeshAgent agent;

    private NodeCanvas.StateMachines.FSMOwner fsm;

    void Start()
    {
        percieve = GetComponent<AIPercieve>();
        perception_manager = GetComponent<AIPerceptionManager>();
        fsm = GetComponent<NodeCanvas.StateMachines.FSMOwner>();

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        animator = gameObject.GetComponent<Animator>();
        health = maxHealth;
        GameObject gameController = GameObject.Find("GameController");
        gameController.GetComponent<ManageScene>().enemies.Add(this);
        shotCollector = gameController.GetComponent<Shot_Collector>();
        startingPosition = transform.position;
        startingRotation = transform.rotation;

        agent = GetComponent<NavMeshAgent>();
    }

    public bool ScanForPlayers()
    {
        foreach (GameObject go in percieve.detected)
        {
            if (LayerMask.LayerToName(go.layer) == "Player" && !go.GetComponent<PlayerController>().IsDead())
            {
                target = go;
                return true;
            }
        }

        return false;
    }

    public bool ScanShotsFired()
    {
        if (shotCollector == null)
            shotCollector = GameObject.Find("GameController").GetComponent<Shot_Collector>();
        else
            foreach (Shot_Collector.Shot shot_it in shotCollector.shotLocations)
            {
                Vector3 shot_pos = new Vector3();
                shot_pos = shot_it.author.transform.position;

                if (LayerMask.LayerToName(shot_it.author.layer) == "Player" && Vector3.Distance(shot_pos, gameObject.transform.position) < percieve.hear_distance)
                {
                    last_location = shot_pos;
                    return true;
                }
            }

        return false;
    }

    public bool IsTargetDead()
    {
        if (target != null)
            return target.GetComponent<PlayerController>().IsDead();
        else
            return true;
    }

    public void FireAtTarget()
    {
        //Shot
        //transform.Rotate(Vector3.up, );
        rifle.GetComponent<SpawnBullet>().Shoot(player.transform.position);

        if (IsTargetDead())
            if (!ScanForPlayers())
                target = null;
    }

    public void PursueTarget()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathComplete || agent.pathStatus == NavMeshPathStatus.PathInvalid)
            agent.SetDestination(last_location);
    }

    public bool IsPathFinished()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            last_location = Vector3.zero;
            return true;
        }
        else if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
            agent.SetDestination(last_location);

        return false;
    }

    void Update()
    {

    }

    public void SetDead()
    {
        animator.SetBool("IsDead", true);
        agent.enabled = false;
        fsm.enabled = false;
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            health -= player.damage;
        }
    }

    public void ResetEnemy()
    {
        health = maxHealth;
        transform.position = startingPosition;
        transform.rotation = startingRotation;
        animator.SetBool("IsDead", false);

        target = null;
        last_location = Vector3.zero;

        agent.enabled = true;
        fsm.enabled = true;
        fsm.RestartBehaviour();
    }
}
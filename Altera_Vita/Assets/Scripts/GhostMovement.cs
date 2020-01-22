using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{

    List<PlayerActions> actionsListW = new List<PlayerActions>();
    List<PlayerActions> actionsListA = new List<PlayerActions>();
    List<PlayerActions> actionsListS = new List<PlayerActions>();
    List<PlayerActions> actionsListD = new List<PlayerActions>();
    List<PlayerShoot> actionsListShoot = new List<PlayerShoot>();

    Vector3 initialPos;

    float speed;
    float ghostTimer;

    bool moveForward = false;
    bool moveBackwards = false;
    bool moveRight = false;
    bool moveLeft = false;
    bool shoot = false;

    float health = 100f;

    float timeLeftW, timeLeftA, timeLeftS, timeLeftD, timeLeftShoot = 0f;

    float totalTime;

    int indexW, indexA, indexS, indexD, indexShoot = 0;

    int nResets, limitResets;

    PlayerController playerMovement;

    Animator animator;

    float Input_X;
    float Input_Z;

    GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerController>();

        for (int i = 0; i < playerMovement.prevActionsListW.Count; ++i)
        {
            actionsListW.Add(playerMovement.prevActionsListW[i]);
        }

        for (int i = 0; i < playerMovement.prevActionsListA.Count; ++i)
        {
            actionsListA.Add(playerMovement.prevActionsListA[i]);
        }

        for (int i = 0; i < playerMovement.prevActionsListS.Count; ++i)
        {
            actionsListS.Add(playerMovement.prevActionsListS[i]);
        }

        for (int i = 0; i < playerMovement.prevActionsListD.Count; ++i)
        {
            actionsListD.Add(playerMovement.prevActionsListD[i]);
        }

        for (int i = 0; i < playerMovement.prevActionsListShoot.Count; ++i)
        {
            actionsListShoot.Add(playerMovement.prevActionsListShoot[i]);
        }
        animator = GetComponent<Animator>();

        speed = playerMovement.speed;
        ghostTimer = playerMovement.ghostTimer;
        totalTime = playerMovement.totalTime;
        initialPos = playerMovement.startingPosition;
        limitResets = playerMovement.limitResets;
        nResets = playerMovement.nResets;
        Input_X = playerMovement.Input_X;
        Input_Z = playerMovement.Input_Z;
        gun = playerMovement.gun;

        playerMovement.actionsListW.Clear();
        playerMovement.actionsListA.Clear();
        playerMovement.actionsListS.Clear();
        playerMovement.actionsListD.Clear();
        playerMovement.actionsListShoot.Clear();
        playerMovement.prevActionsListW.Clear();
        playerMovement.prevActionsListA.Clear();
        playerMovement.prevActionsListS.Clear();
        playerMovement.prevActionsListD.Clear();
        playerMovement.prevActionsListShoot.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        nResets = playerMovement.nResets;
        if (nResets <= limitResets && playerMovement.IsDead())
        {
            transform.position = initialPos;
            indexW = indexA = indexS = indexD = indexShoot = 0;
            timeLeftW = timeLeftA = timeLeftS = timeLeftD = timeLeftShoot = 0f;
        }

        if (IsDead())
            animator.SetBool("IsDead", true);
        
        else
            MoveGhostPlayer();

        if (moveRight)
            MoveRight();
        if (moveLeft)
            MoveLeft();
        if (moveForward)
            MoveForward();
        if (moveBackwards)
            MoveBackwards();

        if (!moveLeft && !moveRight && !moveForward && !moveBackwards)
        {
            animator.SetFloat("Input_Z", 0f);
            animator.SetFloat("Input_X", 0f);
        }
    }

    void MoveGhostPlayer()
    {
        if (indexW < actionsListW.Count)
        {
            if (timeLeftW == 0)
                timeLeftW = actionsListW[indexW].timePressed;

            timeLeftW -= Time.deltaTime;
            if (timeLeftW > 0)
            {
                if (actionsListW[indexW].isMoving)
                    moveForward = true;
                else
                    moveForward = false;
            }

            else
            {
                indexW++;
                timeLeftW = 0;
            }
        }

        if (indexA < actionsListA.Count)
        {
            if (timeLeftA == 0)
                timeLeftA = actionsListA[indexA].timePressed;

            timeLeftA -= Time.deltaTime;
            if (timeLeftA > 0)
            {
                if (actionsListA[indexA].isMoving)
                    moveLeft = true;
                else
                    moveLeft = false;
            }

            else
            {
                indexA++;
                timeLeftA = 0;
            }
        }

        if (indexS < actionsListS.Count)
        {
            if (timeLeftS == 0)
                timeLeftS = actionsListS[indexS].timePressed;

            timeLeftS -= Time.deltaTime;
            if (timeLeftS > 0)
            {
                if (actionsListS[indexS].isMoving)
                    moveBackwards = true;
                else
                    moveBackwards = false;
            }

            else
            {
                indexS++;
                timeLeftS = 0;
            }
        }

        if (indexD < actionsListD.Count)
        {
            if (timeLeftD == 0)
                timeLeftD = actionsListD[indexD].timePressed;

            timeLeftD -= Time.deltaTime;
            if (timeLeftD > 0)
            {
                if (actionsListD[indexD].isMoving)
                    moveRight = true;
                else
                    moveRight = false;
            }

            else
            {
                indexD++;
                timeLeftD = 0;
            }
        }

        if (indexShoot < actionsListShoot.Count)
        {
            if (timeLeftShoot == 0)
                timeLeftShoot = actionsListShoot[indexShoot].timePressed;

            timeLeftShoot -= Time.deltaTime;

            if (timeLeftShoot > 0)
            {
                if (actionsListShoot[indexShoot].shoot && !shoot)
                {
                    SpawnBullet bullet = gun.GetComponent<SpawnBullet>();
                    bullet.Shoot(actionsListShoot[indexShoot].direction);
                    shoot = true;
                }
            }

            else
            {
                shoot = false;
                indexShoot++;
                timeLeftShoot = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
            health -= 100;
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    void MoveForward()
    {
        transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        Vector3 aux = new Vector3(0, 0, 1);
        transform.LookAt(transform.position + aux, Vector3.up);
        animator.SetFloat("Input_Z", 0.8f);
    }

    void MoveBackwards()
    {
        transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
        Vector3 aux = new Vector3(0, 0, -1);
        transform.LookAt(transform.position + aux, Vector3.up);
        animator.SetFloat("Input_Z", -0.8f);
    }

    void MoveRight()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        Vector3 aux = new Vector3(1, 0, 0);
        transform.LookAt(transform.position + aux, Vector3.up);
        animator.SetFloat("Input_X", 0.8f);
    }

    void MoveLeft()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        Vector3 aux = new Vector3(-1, 0, 0);
        transform.LookAt(transform.position + aux, Vector3.up);
        animator.SetFloat("Input_X", -0.8f);
    }

}

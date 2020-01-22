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

        speed = playerMovement.speed;
        ghostTimer = playerMovement.ghostTimer;
        totalTime = playerMovement.totalTime;
        initialPos = playerMovement.startingPosition;
        limitResets = playerMovement.limitResets;
        nResets = playerMovement.nResets;
        animator = playerMovement.animator;
        Input_X = playerMovement.Input_X;
        Input_Z = playerMovement.Input_Z;
        gun = playerMovement.gun;

        playerMovement.actionsListW.Clear();
        playerMovement.actionsListA.Clear();
        playerMovement.actionsListS.Clear();
        playerMovement.actionsListD.Clear();
        playerMovement.prevActionsListW.Clear();
        playerMovement.prevActionsListA.Clear();
        playerMovement.prevActionsListS.Clear();
        playerMovement.prevActionsListD.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        nResets = playerMovement.nResets;
        if (nResets <= limitResets && Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = initialPos;
            indexW = indexA = indexS = indexD = 0;
            timeLeftW = timeLeftA = timeLeftS = timeLeftD = 0f;
        }

        MoveGhostPlayer();

        if (moveRight)
            MoveRight();
        if (moveLeft)
            MoveLeft();
        if (moveForward)
            MoveForward();
        if (moveBackwards)
            MoveBackwards();

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
                Debug.Log("else");
                shoot = false;
                indexShoot++;
                timeLeftShoot = 0;
            }
        }
    }

    void MoveForward()
    {
        transform.position += new Vector3(0, 0, speed);
        //Input_Z = Input.GetAxis("Vertical");
        //animator.SetFloat("Input_Z", Input_Z);
    }

    void MoveBackwards()
    {
        transform.position += new Vector3(0, 0, -speed);
        //Input_Z = Input.GetAxis("Vertical");
        //animator.SetFloat("Input_Z", Input_Z);
    }

    void MoveRight()
    {
        transform.position += new Vector3(speed, 0, 0);
        //Input_X = Input.GetAxis("Horizontal");
        //animator.SetFloat("Input_X", Input_X);
    }

    void MoveLeft()
    {
        transform.position += new Vector3(-speed, 0, 0);
        //Input_X = Input.GetAxis("Horizontal");
        //animator.SetFloat("Input_X", Input_X);
    }

}

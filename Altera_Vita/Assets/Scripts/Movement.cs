using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum KEY_PRESSED { W, A, S, D};

//public struct PlayerActions
//{
//    public KEY_PRESSED key;
//    public float timePressed;
//    public Vector3 position;
//    public float totalTime;
//    public bool isMoving;
//}

public class Movement : MonoBehaviour
{
    public Vector3 initialPosition;
    public int speed = 5;
    Rigidbody rb;

    float timerW, timerA, timerS, timerD;
    float lastTimerW, lastTimerA, lastTimerS, lastTimerD;
    float idleTimerW, idleTimerA, idleTimerS, idleTimerD;

    [HideInInspector]
    public float totalTime;

    [HideInInspector]
    public float ghostTimer;

    public List<PlayerActions> actionsListW = new List<PlayerActions>();
    public List<PlayerActions> actionsListA = new List<PlayerActions>();
    public List<PlayerActions> actionsListS = new List<PlayerActions>();
    public List<PlayerActions> actionsListD = new List<PlayerActions>();

    public List<PlayerActions> prevActionsListW = new List<PlayerActions>();
    public List<PlayerActions> prevActionsListA = new List<PlayerActions>();
    public List<PlayerActions> prevActionsListS = new List<PlayerActions>();
    public List<PlayerActions> prevActionsListD = new List<PlayerActions>();
        
    PlayerActions playerActions;

    List<float> resetTime = new List<float>();

    [HideInInspector]
    public int nResets = 0;
    public int limitResets = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = new Vector3(2.17f, 0.34f, -2.2f);
        resetTime.Add(0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (nResets > limitResets)
                return;

            if (actionsListW.Count == 0)
            {
                playerActions.key = KEY_PRESSED.W;
                playerActions.timePressed = Time.time - resetTime[nResets];
                playerActions.position = transform.position;
                playerActions.totalTime = Time.time - resetTime[nResets];
                playerActions.isMoving = false;
                actionsListW.Add(playerActions);
            }

            else
            {
                idleTimerW = Time.time - actionsListW[actionsListW.Count-1].totalTime;
                playerActions.key = KEY_PRESSED.W;
                playerActions.timePressed = idleTimerW;
                playerActions.position = transform.position;
                playerActions.totalTime = Time.time - resetTime[nResets];
                playerActions.isMoving = false;
                actionsListW.Add(playerActions);
            }

            lastTimerW = Time.time;
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (nResets > limitResets)
                return;

            if (actionsListA.Count == 0)
            {
                playerActions.key = KEY_PRESSED.A;
                playerActions.timePressed = Time.time - resetTime[nResets];
                playerActions.position = transform.position;
                playerActions.totalTime = Time.time - resetTime[nResets];
                playerActions.isMoving = false;
                actionsListA.Add(playerActions);
            }

            else
            {
                idleTimerA = Time.time - actionsListA[actionsListA.Count - 1].totalTime;
                playerActions.key = KEY_PRESSED.A;
                playerActions.timePressed = idleTimerA;
                playerActions.position = transform.position;
                playerActions.totalTime = Time.time - resetTime[nResets];
                playerActions.isMoving = false;
                actionsListA.Add(playerActions);
            }

            lastTimerA = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (nResets > limitResets)
                return;

            if (actionsListS.Count == 0)
            {
                playerActions.key = KEY_PRESSED.S;
                playerActions.timePressed = Time.time - resetTime[nResets];
                playerActions.position = transform.position;
                playerActions.totalTime = Time.time - resetTime[nResets];
                playerActions.isMoving = false;
                actionsListS.Add(playerActions);
            }

            else
            {
                idleTimerS = Time.time - actionsListS[actionsListS.Count - 1].totalTime;
                playerActions.key = KEY_PRESSED.S;
                playerActions.timePressed = idleTimerS;
                playerActions.position = transform.position;
                playerActions.totalTime = Time.time - resetTime[nResets];
                playerActions.isMoving = false;
                actionsListS.Add(playerActions);
            }
            lastTimerS = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (nResets > limitResets)
                return;

            if (actionsListD.Count == 0)
            {
                playerActions.key = KEY_PRESSED.D;
                playerActions.timePressed = Time.time - resetTime[nResets];
                playerActions.position = transform.position;
                playerActions.totalTime = Time.time - resetTime[nResets];
                playerActions.isMoving = false;
                actionsListD.Add(playerActions);
            }

            else
            {
                idleTimerD = Time.time - actionsListD[actionsListD.Count - 1].totalTime;
                playerActions.key = KEY_PRESSED.D;
                playerActions.timePressed = idleTimerD;
                playerActions.position = transform.position;
                playerActions.totalTime = Time.time - resetTime[nResets];
                playerActions.isMoving = false;
                actionsListD.Add(playerActions);
            }
            lastTimerD = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (timerW != 0)
            {
                playerActions.key = KEY_PRESSED.W;
                playerActions.timePressed = timerW;
                playerActions.position = transform.position;
                playerActions.totalTime = Time.time;
                playerActions.isMoving = true;
                actionsListW.Add(playerActions);
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (timerA != 0)
            {
                playerActions.key = KEY_PRESSED.A;
                playerActions.timePressed = timerA;
                playerActions.position = transform.position;
                playerActions.totalTime = Time.time;
                playerActions.isMoving = true;
                actionsListA.Add(playerActions);
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (timerS != 0)
            {
                playerActions.key = KEY_PRESSED.S;
                playerActions.timePressed = timerS;
                playerActions.position = transform.position;
                playerActions.totalTime = Time.time;
                playerActions.isMoving = true;
                actionsListS.Add(playerActions);
            }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            if (timerD != 0)
            {
                playerActions.key = KEY_PRESSED.D;
                playerActions.timePressed = timerD;
                playerActions.position = transform.position;
                playerActions.totalTime = Time.time;
                playerActions.isMoving = true;
                actionsListD.Add(playerActions);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            MoveForward();
            timerW = Time.time - lastTimerW;
        }

        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
            timerA = Time.time - lastTimerA;
        }

        if (Input.GetKey(KeyCode.S))
        {
            MoveBackwards();
            timerS = Time.time - lastTimerS;
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
            timerD = Time.time - lastTimerD;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            nResets++;

            if (nResets <= limitResets)
            {
                transform.position = initialPosition;

                playerActions.isMoving = false;
                actionsListW.Add(playerActions);
                actionsListA.Add(playerActions);
                actionsListS.Add(playerActions);
                actionsListD.Add(playerActions);

                prevActionsListW = actionsListW;
                prevActionsListA = actionsListA;
                prevActionsListS = actionsListS;
                prevActionsListD = actionsListD;

                totalTime = Time.time;
            
                GameObject ghost;
                ghost = Instantiate(gameObject, initialPosition, Quaternion.identity);
                ghost.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                ghost.AddComponent<GhostMovement>();
                Movement move = ghost.GetComponent<Movement>();
                move.enabled = false;

                resetTime.Add(Time.time);
            }
        } 
    }

    void MoveForward()
    {
        gameObject.transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    void MoveBackwards()
    {
        gameObject.transform.position -= Vector3.forward * speed * Time.deltaTime;
    }

    void MoveRight()
    {
        gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
    }

    void MoveLeft()
    {
        gameObject.transform.position -= Vector3.right * speed * Time.deltaTime;
    }
}

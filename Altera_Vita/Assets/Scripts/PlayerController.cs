using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum KEY_PRESSED { W, A, S, D };

public struct PlayerActions
{
    public KEY_PRESSED key;
    public float timePressed;
    public Vector3 position;
    public float totalTime;
    public bool isMoving;
}

public struct PlayerShoot
{
    public float timePressed;
    public float totalTime;
    public bool shoot;
    public Vector3 direction;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Vector3 hipsOffset;
    [SerializeField] private Vector3 chestOffset;
    [SerializeField] public GameObject gun;

    public bool mustRespawn = false;
    public float lastDeathTime = 0.0f;
    public float respawnWait = 2.0f;
    public float resetLevelWait = 4.0f;

    public int health;
    public float speed = 0.025f;
    public Vector3 startingPosition = Vector3.zero;
    public Quaternion startingRotation = Quaternion.identity;
    public Transform target;
    public int max_clip_ammo = 7;
    public int damage = 10;
    private int clip_ammo;

    public Reload reloader;

    private Shot_Collector shotCollector;

    [HideInInspector]
    public Animator animator;
    private Transform chest;
    private Transform hips;

    [HideInInspector]
    public float Input_X;
    [HideInInspector]
    public float Input_Z;

    float timerW, timerA, timerS, timerD, timerShoot;
    float lastTimerW, lastTimerA, lastTimerS, lastTimerD, lastTimerShoot;
    float idleTimerW, idleTimerA, idleTimerS, idleTimerD, idleTimerShoot;

    [HideInInspector]
    public float totalTime;

    [HideInInspector]
    public float ghostTimer;

    public List<PlayerActions> actionsListW = new List<PlayerActions>();
    public List<PlayerActions> actionsListA = new List<PlayerActions>();
    public List<PlayerActions> actionsListS = new List<PlayerActions>();
    public List<PlayerActions> actionsListD = new List<PlayerActions>();
    public List<PlayerShoot> actionsListShoot = new List<PlayerShoot>();

    public List<PlayerActions> prevActionsListW = new List<PlayerActions>();
    public List<PlayerActions> prevActionsListA = new List<PlayerActions>();
    public List<PlayerActions> prevActionsListS = new List<PlayerActions>();
    public List<PlayerActions> prevActionsListD = new List<PlayerActions>();
    public List<PlayerShoot> prevActionsListShoot = new List<PlayerShoot>();

    PlayerActions playerActions;
    PlayerShoot playerShots;

    List<float> resetTime = new List<float>();

    [HideInInspector]
    public int nResets = 0;
    public int limitResets = 5;

    public AudioClip reload_sound;
    public AudioClip empty_clip_sound;
    private AudioSource source;
    public AudioLowPassFilter low_pass_filter;

    void Start()
    {
        shotCollector = GameObject.Find("GameController").GetComponent<Shot_Collector>();
        animator = gameObject.GetComponent<Animator>();
        chest = animator.GetBoneTransform(HumanBodyBones.Chest);
        hips = animator.GetBoneTransform(HumanBodyBones.Hips);
        health = maxHealth;

        resetTime.Add(0f);
        clip_ammo = max_clip_ammo;
        low_pass_filter.cutoffFrequency = 22000;
        source = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {    
        if (IsDead() || Input.GetKey(KeyCode.F))
        {
            animator.SetBool("IsDead", true);
            lastDeathTime = Time.time;
            mustRespawn = true;
            low_pass_filter.cutoffFrequency = 300;
        }

        // Respawn in case it died
        if (mustRespawn && Time.time >= lastDeathTime + respawnWait)
        {
            ResetCharacter();
            mustRespawn = false;
        }

        if (Time.time >= lastDeathTime + resetLevelWait)
        {       
            // Player movement
            Input_X = Input.GetAxis("Vertical");
            animator.SetFloat("Input_X", Input_X);
            Input_Z = Input.GetAxis("Vertical");
            animator.SetFloat("Input_Z", Input_Z);

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
                    idleTimerW = Time.time - actionsListW[actionsListW.Count - 1].totalTime;
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
                transform.position += new Vector3(0, 0, speed * Time.deltaTime);
                Input_Z = Input.GetAxis("Vertical");
                animator.SetFloat("Input_Z", Input_Z);
                timerW = Time.time - lastTimerW;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
                Input_Z = Input.GetAxis("Vertical");
                animator.SetFloat("Input_Z", Input_Z);
                timerS = Time.time - lastTimerS;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
                Input_X = Input.GetAxis("Horizontal");
                animator.SetFloat("Input_X", Input_X);
                timerA = Time.time - lastTimerA;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                Input_X = Input.GetAxis("Horizontal");
                animator.SetFloat("Input_X", Input_X);
                timerD = Time.time - lastTimerD;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                nResets++;

                if (nResets <= limitResets)
                {
                    transform.position = startingPosition;

                    playerActions.isMoving = false;
                    actionsListW.Add(playerActions);
                    actionsListA.Add(playerActions);
                    actionsListS.Add(playerActions);
                    actionsListD.Add(playerActions);

                    prevActionsListW = actionsListW;
                    prevActionsListA = actionsListA;
                    prevActionsListS = actionsListS;
                    prevActionsListD = actionsListD;
                    prevActionsListShoot = actionsListShoot;

                    totalTime = Time.time;

                    GameObject ghost;
                    ghost = Instantiate(gameObject, startingPosition, startingRotation);
                    ghost.AddComponent<GhostMovement>();
                    PlayerController move = ghost.GetComponent<PlayerController>();
                    move.enabled = false;

                    resetTime.Add(Time.time);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                timerShoot = Time.time - lastTimerShoot;

                playerShots.shoot = true;
                playerShots.timePressed = timerShoot;
                playerShots.totalTime = Time.time - resetTime[nResets];

                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                float midPoint = (transform.position - Camera.main.transform.position).magnitude;
                Vector3 destination = mouseRay.origin + mouseRay.direction * midPoint;
                destination.y = transform.position.y + 1.5f;

                playerShots.direction = destination;
                actionsListShoot.Add(playerShots);
            }

            // Player shoot
            if (Input.GetMouseButtonDown(0) && clip_ammo > 0)
            {
                if (actionsListShoot.Count != 0)
                {
                    idleTimerShoot = Time.time - actionsListShoot[actionsListShoot.Count - 1].totalTime;

                    playerShots.shoot = false;
                    playerShots.timePressed = idleTimerShoot;
                    playerShots.totalTime = Time.time - resetTime[nResets];
                    playerShots.direction = Vector3.zero;
                    actionsListShoot.Add(playerShots);
                }
                else
                {
                    playerShots.shoot = false;
                    playerShots.timePressed = Time.time - resetTime[nResets];
                    playerShots.totalTime = Time.time - resetTime[nResets];
                    playerShots.direction = Vector3.zero;
                    actionsListShoot.Add(playerShots);
                }


                lastTimerShoot = Time.time;

                clip_ammo--;

                gun.GetComponent<SpawnBullet>().Shoot(Vector3.zero);
                shotCollector.shotLocations.Add(new Shot_Collector.Shot(gameObject));
            }
            else if (Input.GetMouseButtonDown(0))
            {
                source.clip = empty_clip_sound;
                source.Play();
            }

            // reload!
            if (Input.GetKeyDown(KeyCode.R))
            {
                source.clip = reload_sound;
                source.Play();
                StartCoroutine(Reloadinger());
            }
        }
    }

    IEnumerator Reloadinger()
    {
        GameObject.FindGameObjectWithTag("reload").GetComponent<Text>().enabled = true;
        reloader.ReloadAnim();
        yield return new WaitForSeconds(1.5f);
        clip_ammo = max_clip_ammo;
        reloader.EndReloadAnim();
        GameObject.FindGameObjectWithTag("reload").GetComponent<Text>().enabled = false;
    }

    private void LateUpdate()
    {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos.y = 0;
        //Debug.Log(mousePos);

        //chest.LookAt(target);
        //chest.rotation *= Quaternion.Euler(chestOffset);

        // hips.LookAt(CalculateForwardTransform());        
        // hips.rotation *= Quaternion.Euler(hipsOffset);

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float midPoint = (transform.position - Camera.main.transform.position).magnitude;

        Vector3 destination = mouseRay.origin + mouseRay.direction * midPoint;
        destination.y = transform.position.y + 1.5f;

        chest.LookAt(destination);

    }

    public bool IsDead()
    {
        return health <= 0;
    }

    private void ResetCharacter()
    {
        // Stats reset
        health = maxHealth;
        clip_ammo = max_clip_ammo;

        // Position reset
        transform.position =    startingPosition;
        transform.rotation = startingRotation;

        // Animation reset
        animator.SetBool("IsDead", false);
        animator.SetFloat("Input_X", 0.0f);
        animator.SetFloat("Input_Z", 0.0f);

        // Audio effect reset
        low_pass_filter.cutoffFrequency = 22000;

        // Clone code
    }

}
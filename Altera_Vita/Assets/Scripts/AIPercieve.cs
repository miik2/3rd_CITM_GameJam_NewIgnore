using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPercieve : MonoBehaviour
{
    public LayerMask targets;
    public LayerMask obstacles;

    [Header("Contact")]
    public bool contact = true;
    public float contact_radius = 5.0f;

    [Header("Hearing")]
    public bool hearing = true;
    public float hear_distance = 15.0f;

    [Header("Sight")]
    public bool sight = true;
    public Camera vision = null;

    private float visionDistance;
    private float colliderSearchRadius;

    public AIPerceptionManager manager;
    public List<GameObject> spotted_by = new List<GameObject>();
    public List<GameObject> detected = new List<GameObject>();
    private Collider myCollider;

    public Shot_Collector shotCollector;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<AIPerceptionManager>();
        myCollider = GetComponent<Collider>();
        //shotCollector = GameObject.Find("Ambient Light").GetComponent<Shot_Collector>();

        if (vision == null)
            GetComponent<Camera>();

        visionDistance = vision.farClipPlane / -Mathf.Cos(vision.fieldOfView / 2.0f);  // Make cone radius out of FOV angle and farClipPlane distance from camera

        //colliderSearchRadius = Mathf.Max(visionDistance, distance);   // Because sound (we don't use colliders for sound)
        colliderSearchRadius = Mathf.Max(visionDistance, contact_radius); // Use the largest area of perception for collecting in a sphere
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, colliderSearchRadius, targets);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(vision);

        List<PerceptionEvent> currently_percieving = new List<PerceptionEvent>();   // Record of all perceived objects and the method used for detection

        foreach (Collider col in colliders)
        {
            if (col != myCollider) // If collider isn't from the agent who's scanning
            {
                PerceptionEvent foundData = new PerceptionEvent();
                Vector3 targetVector = col.transform.position - transform.position;
                float targetDistance = targetVector.magnitude;

                bool inRange = false;

                if (sight && GeometryUtility.TestPlanesAABB(planes, col.bounds))   // In vision FOV
                {
                    foundData.sense = PerceptionEvent.senses.VISION;
                    inRange = true;
                }
                else if (contact && targetDistance < contact_radius)  // In contact distance
                {
                    foundData.sense = PerceptionEvent.senses.CONTACT;
                    inRange = true;
                }

                if (inRange && !Physics.Raycast(transform.position, targetVector, targetDistance, obstacles))  // If in hearing distance or no obstacles in the way
                {
                    foundData.target = col.gameObject;
                    currently_percieving.Add(foundData);   // We list all that we percieve
                }
            }
        }
        
        // The following process consists in matching both lists and pair their objects, the ones that remain unpaired must be updated in the "detected" list
        bool[] detectedgMatch = new bool[detected.Count];
        bool[] percievingMatch = new bool[currently_percieving.Count];  // These arrays will flag the pairing of the content in the same index positions
                                                                        // Ask Marc: We could use a "index list" for the same purpose, should we?
        for (int i = 0; i < detected.Count; i++)
        {
            for (int j = 0; j < currently_percieving.Count; j++)
            {
                if (detected[i] == currently_percieving[j].target)  // If objects match in both lists, it means what I remember what I just percieved
                {
                    detectedgMatch[i] = true;   // We flag the booleans at their corresponding indexes
                    percievingMatch[j] = true;
                    break;
                }
            }
        }

        // We iterate the boolean arrays, were unpaired objects are marked with false flags
        for (int i = detected.Count - 1; i >= 0; i--)
        {
            if (detectedgMatch[i] == false) // If an object that was remembered to be detected isn't percieved currently, it is LOST
            {
                PerceptionEvent lostData = new PerceptionEvent();
                lostData.eye = this.gameObject;
                lostData.target = detected[i];
                lostData.type = PerceptionEvent.types.LOST;
                manager.PerceptionEvent(lostData);
                //SendMessage("PerceptionEvent", lostData);
                detected.RemoveAt(i);
            }
        }

        for (int i = currently_percieving.Count - 1; i >= 0; i--)   // If a currently percieved object isn't remembered, we need to add it to "memory" as NEW
        {
            if (percievingMatch[i] == false)
            {
                currently_percieving[i].type = PerceptionEvent.types.NEW;
                currently_percieving[i].eye = this.gameObject;
                manager.PerceptionEvent(currently_percieving[i]);
                //SendMessage("PerceptionEvent", currently_percieving[i]);
                detected.Add(currently_percieving[i].target);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        if (contact)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, contact_radius);
        }
        if (hearing)
        {
            Gizmos.color = new Color(255.0f, 140.0f, 0.0f);
            Gizmos.DrawWireSphere(transform.position, hear_distance);
        }
    }

    private void OnDisable()
    {
        spotted_by.Clear();
        detected.Clear();
    }
}
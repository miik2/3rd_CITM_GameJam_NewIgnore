using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public float speed;
    private Rigidbody rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }
    void Start()
    {
        rig.velocity = transform.forward * speed;
    }
}
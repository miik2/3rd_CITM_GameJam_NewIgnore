using UnityEngine;

public enum BulletType
{
    PLAYER = 0,
    ENEMY,
};

public class BulletMover : MonoBehaviour
{
  
    public float speed;
    public BulletType owner;

    Vector3 direction;

    public Vector3 target;

    bool calculatedDirection = false;

    private SphereCollider col;

    private void Awake()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float midPoint = (transform.position - Camera.main.transform.position).magnitude;

        Vector3 destination = mouseRay.origin + mouseRay.direction * midPoint;
        destination.y = transform.position.y;

        direction = (destination - transform.position).normalized;
        col = GetComponent<SphereCollider>();
    }

    void Update()
    {
        if (target == Vector3.zero)
            transform.position += direction * speed * Time.deltaTime;

        else if (!calculatedDirection)
        {
            direction = (target - transform.position).normalized * speed * Time.deltaTime;
            calculatedDirection = true;
        }

        if (calculatedDirection)
            transform.position += direction;

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (owner == BulletType.PLAYER)
        if(!collision.transform.parent.CompareTag("Player"))
        {
            Destroy(this.transform.parent);
        }

        if(owner == BulletType.ENEMY)
        if (!collision.transform.parent.CompareTag("Enemy"))
        {
            Destroy(this.transform.parent);
        }
    }
}
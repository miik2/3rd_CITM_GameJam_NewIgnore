using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAlign : MonoBehaviour
{
    public  EnemyController controller;

    bool corroutine_active = false;

    IEnumerator Rotator(Vector3 desired)
    {
        Quaternion targetRotation = Quaternion.LookRotation(desired);
        Quaternion currentRotation = transform.rotation;
        for (float i = 0; i < 1.0f; i += Time.deltaTime / 0.2f)
        {
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, i);
            yield return null;
        }
    }

    void Start()
    {
        controller = gameObject.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller != null)
        {
            Vector3 desired = (controller.target.transform.position - transform.position).normalized;

            if (controller.target != null && Vector3.Angle(transform.forward, desired) > 2.0f)
            {
                StartCoroutine("Rotator", desired);
            }
        }
    }
}

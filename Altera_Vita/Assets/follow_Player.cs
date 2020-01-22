using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_Player : MonoBehaviour
{

    public GameObject object_to_follow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(object_to_follow.transform.position.x, this.transform.position.y, object_to_follow.transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Collector : MonoBehaviour
{
    public struct Shot
    {
        public Shot(GameObject author_)
        {
            author = author_;
            started_at = Time.time;
        }

        public GameObject author;
        float started_at;

        public bool Finished()
        {
            return Time.time - started_at > 0.1;
        }
    }

    public List<Shot> shotLocations = new List<Shot>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Shot shot_it in shotLocations)
            if (shot_it.Finished())
                shotLocations.Remove(shot_it);

    }
}

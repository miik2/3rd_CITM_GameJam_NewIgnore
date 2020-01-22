using UnityEngine;
using System.Collections.Generic;

public class PerceptionEvent
{
	public enum senses { VISION, SOUND, CONTACT };
	public enum types { NEW, LOST };

    public GameObject eye;
	public GameObject target;
	public senses sense;
	public types type;
}

public class AIPerceptionManager : MonoBehaviour {

    //public GameObject Alert;
    //public List<PerceptionEvent> target_events = new List<PerceptionEvent>();
    //public List<PerceptionEvent> spotter_events = new List<PerceptionEvent>();

    // Update is called once per frame
    public void PerceptionEvent (PerceptionEvent ev) {

        //AIPerceptionManager target_manager = ev.target.GetComponent<AIPerceptionManager>();
        if (ev.target != null)
        {
            AIPercieve target_perception = ev.target.GetComponent<AIPercieve>();

            if (ev.type == global::PerceptionEvent.types.NEW)
            {
                Debug.Log("FOUND: " + ev.target.name);
                target_perception.spotted_by.Add(this.gameObject);
                //Alert.SetActive(true);
            }
            else
            {
                Debug.Log("LOST: " + ev.target.name);
                target_perception.spotted_by.Remove(this.gameObject);
                //Alert.SetActive(false);
            }

            //spotter_events.Add(ev);
            //target_manager.target_events.Add(ev);
        }
    }

    //public void Clear()
    //{
    //    target_events.Clear();
    //    spotter_events.Clear();
    //}

    //private void OnEnable()
    //{
    //    target_events.Clear();
    //    spotter_events.Clear();
    //}

    //private void OnDisable()
    //{
    //    target_events.Clear();
    //    spotter_events.Clear();
    //}
}

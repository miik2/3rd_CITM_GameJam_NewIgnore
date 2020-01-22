using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMouse : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float midPoint = (transform.position - Camera.main.transform.position).magnitude;

        Vector3 destination = mouseRay.origin + mouseRay.direction * midPoint;
        destination.y = 10;

        transform.position = destination;
    }
}

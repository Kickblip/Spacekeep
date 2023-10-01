using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point_to : MonoBehaviour
{
    public float offset;
    // Update is called once per frame
    void Update()
    {
        //get angle between mouse and object, rotate accordingly
        Vector3 targetPos = Vector3.zero - transform.position;
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + offset, Vector3.forward);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship_Movement : MonoBehaviour
{
    //Public
    public float speed;
    public float mouse_distance_effect;
    public float offset;
    //Private
    private Vector2 mouse_position;
    public bool canMove = true; //public variable to disable movement

    // Update is called once per frame
    void Update()
    {
        //on left click
        if (canMove) {
            if (Input.GetMouseButton(0))
            {
                //update mouse position
                mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //get mouse distance and adjusts speed
                float mouse_distance = Vector2.Distance(mouse_position, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2)));
                float distance_multiplier = mouse_distance_effect * mouse_distance * speed;
                //move towards mouse
                transform.position = Vector2.MoveTowards(transform.position, mouse_position, distance_multiplier * Time.deltaTime);
                //get angle between mouse and object, rotate accordingly
                Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle + offset, Vector3.forward);
            }  
        }
    }
}

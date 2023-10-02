using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] GameObject inventory;

    [SerializeField] GameObject MothershipInventory;
    [SerializeField] GameObject Mothership;
    [SerializeField] GameObject outline;

    Spaceship_Movement movement;
    float accessRange = 30;

    // Update is called once per frame
    void Start() {
        movement = GetComponent<Spaceship_Movement>();
    }
    void Update()
    {
        if (Input.GetKeyDown("e")) {
            if (inventory.activeSelf) {
                inventory.SetActive(false);
                movement.canMove = true;
            } else {
                inventory.SetActive(true);
                movement.canMove = false;
            }

            if (Mothership != null) {
                if (Vector3.Distance(transform.position, Mothership.transform.position) < accessRange) {

                    if (MothershipInventory.activeSelf) {
                        MothershipInventory.SetActive(false);
                        movement.canMove = true;
                    } else {
                        MothershipInventory.SetActive(true);
                        movement.canMove = false;
                    }
                }
            }
        } 
        if (Mothership != null) {
            if (Vector3.Distance(transform.position, Mothership.transform.position) < accessRange) {
                SpriteRenderer sprite = outline.GetComponent<SpriteRenderer>();
                Color tmp = sprite.color;
                tmp.a = 1f;
                sprite.color = tmp;
            } else {
                SpriteRenderer sprite = outline.GetComponent<SpriteRenderer>();
                Color tmp = sprite.color;
                tmp.a = 0f;
                sprite.color = tmp;
            }
        }
    }
}

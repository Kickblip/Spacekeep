using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    Spaceship_Movement movement;

    // Update is called once per frame
    void Start() {
        movement = GetComponent<Spaceship_Movement>();
    }
    void Update()
    {
        Debug.Log(movement.canMove);
        if (Input.GetKeyDown("i")) {
            if (inventory.activeSelf) {
                inventory.SetActive(false);
                movement.canMove = true;
            } else {
                inventory.SetActive(true);
                movement.canMove = false;
            }
        }
    }
}

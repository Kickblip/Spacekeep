using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] GameObject inventory;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i")) {
            if (inventory.activeSelf) {
                inventory.SetActive(false);
            } else {
                inventory.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    public void Click() {
        //NOTE: This script should be modified so that we can enter in resources in a serialized array that are required to craft
        //that way we don't need a unique script for every recipe

        //but this works as more of a proof of concept

        GameObject[] grids = GameObject.FindGameObjectsWithTag("Inventory");

        foreach (GameObject grid in grids) { //would run through this x amount of times for each required crafting material
            GridSystem scr = grid.GetComponent<GridSystem>();
            foreach (GameObject item in scr.items) {
                InventoryItem iscr = item.GetComponent<InventoryItem>();
                if (iscr.identifier == "Rockium") {
                    Debug.Log("DELETED");
                    scr.items.Remove(item);
                    Destroy(item);
                    return;
                }
            }
        }
    }
}

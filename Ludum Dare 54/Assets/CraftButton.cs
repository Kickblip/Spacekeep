using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipe : MonoBehaviour
{
    [Header("Crafting Resources")]
    [SerializeField] public string[] resources;
    [SerializeField] public int[] amounts;

    [Header("Crafting Result")]
    [SerializeField] GameObject createdObject;
    [SerializeField] Vector2 coords;
    public void Click() {
        //Debug.Log(hasEnough());
        //Debug.Log(counts()[0]);
        if (hasEnough()) {
            GameObject[] grids = GameObject.FindGameObjectsWithTag("Inventory");
            for (int i = 0; i < resources.Length; i++) {
                for (int j = 0; j < amounts[i]; j++) {
                    bool itemFound = false;
                    foreach (GameObject grid in grids) { //would run through this x amount of times for each required crafting material
                        GridSystem scr = grid.GetComponent<GridSystem>();
                        foreach (GameObject item in scr.items) {
                            InventoryItem iscr = item.GetComponent<InventoryItem>();
                            if (iscr.identifier == resources[i]) {
                                Debug.Log("Consumed: " + item);
                                scr.items.Remove(item);
                                Destroy(item);
                                itemFound = true;
                                break;
                            }
                        }
                        if (itemFound) {
                            break;
                        }
                    }
                }
            }
            Instantiate(createdObject, new Vector3(transform.position.x+coords.x,transform.position.y+coords.y, -5), Quaternion.identity);
        }
    }

    public bool hasEnough() {
        bool enough = true;
        GameObject[] grids = GameObject.FindGameObjectsWithTag("Inventory");

        for (int i = 0; i < resources.Length; i++) {
            int count = 0;
            foreach (GameObject grid in grids) { //would run through this x amount of times for each required crafting material
                GridSystem scr = grid.GetComponent<GridSystem>();
                foreach (GameObject item in scr.items) {
                    InventoryItem iscr = item.GetComponent<InventoryItem>();
                    if (iscr.identifier == resources[i]) {
                        count++;
                    }
                }
            }
            if (count < amounts[i]) {
                enough = false;
                break;
            }
        }
        return enough;
    }

    public int[] counts() {
        int[] counts = new int[resources.Length];
        GameObject[] grids = GameObject.FindGameObjectsWithTag("Inventory");

        for (int i = 0; i < resources.Length; i++) {
            int count = 0;
            foreach (GameObject grid in grids) { //would run through this x amount of times for each required crafting material
                GridSystem scr = grid.GetComponent<GridSystem>();
                foreach (GameObject item in scr.items) {
                    InventoryItem iscr = item.GetComponent<InventoryItem>();
                    if (iscr.identifier == resources[i]) {
                        count++;
                    }
                }
            }
            counts[i] = count;
        }
        return counts;
    }
}
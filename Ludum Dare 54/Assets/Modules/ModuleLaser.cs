using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleLaser : InventoryItem
{
    public override void onPlaced() {
        Debug.Log("laseradded");
    }

    public override void onRemoved() {
        Debug.Log("laseradded");
    }
}

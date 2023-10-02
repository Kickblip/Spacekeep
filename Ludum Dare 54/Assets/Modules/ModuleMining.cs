using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMining : InventoryItem
{
    public override void onPlaced() {
        GameObject player = GameObject.Find("Ship");
        PlayerAttributes atr = player.GetComponent<PlayerAttributes>();

        atr.damageMultiplier += 0.5f;
        Debug.Log("Mining removed: " + atr.damageMultiplier);
    }

    public override void onRemoved() {
        GameObject player = GameObject.Find("Ship");
        PlayerAttributes atr = player.GetComponent<PlayerAttributes>();
        
        atr.damageMultiplier += 0.5f;
        Debug.Log("Mining removed: " + atr.damageMultiplier);
    }
}

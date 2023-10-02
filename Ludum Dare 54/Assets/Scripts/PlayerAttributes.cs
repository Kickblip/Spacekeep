using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public float baseDamage = 10;
    public float damageMultiplier = 1;
    public float damage = 10; //final damage player does to enemies
    public float speed = 10;

    void Update() {
        damage = baseDamage*damageMultiplier;
    }
}

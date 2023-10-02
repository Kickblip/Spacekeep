using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    float enemyCount = 1; //how many enemies are spawned each cycle (will increase over time)

    float cycleTime = 30; //cycle time at start, will slowly decrease
    float curCycleTime = 0;
    [SerializeField] GameObject enemy;

    void Update()
    {
        if (enemyCount < 3) {
            enemyCount += 0.1f*Time.deltaTime;
        }

        if (cycleTime > 5) {
            cycleTime -= 0.1f*Time.deltaTime;
        }

        //Debug.Log(enemyCount + " | " + cycleTime);

        if (curCycleTime > 0) {
            curCycleTime -= 1*Time.deltaTime;
        } else {//spawn enemies
            curCycleTime = cycleTime;
            Debug.Log("cyclespawn");
            for (int i = 0; i < (int)enemyCount; i++) {
                Instantiate(enemy, new Vector3(transform.position.x,transform.position.y, 0), Quaternion.identity);
            }
        }
    }
}

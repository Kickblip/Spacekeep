using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    LineRenderer LineRend;
    float timer = 0;
    float damage = 20;
    float cycleTime = 1; //time between shots
    float laserTimer = 0;
    float range = 30;
    [SerializeField] AudioSource shoot;
    void Start()
    {
        LineRend = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag != "InInventory") {
            if (timer <= 0) { //time to shoot
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");

                GameObject close = GetClosestEnemy(enemies);

                if (close != null) {
                    if (Vector3.Distance(close.transform.position,transform.position) < range) {
                        LineRend.enabled = true;
                        Vector3 sp = transform.position;
                        Vector3 ep = close.transform.position;
                        LineRend.SetVertexCount(2);
                        LineRend.SetPosition(0, sp);
                        LineRend.SetPosition(1, ep);
                        close.GetComponent<EnemyController>().hp -= damage;
                        timer = cycleTime;
                        laserTimer = 0.5f;
                        shoot.Play();
                    }
                }
            } else {
                timer -= 1 * Time.deltaTime;
                laserTimer -= 1 * Time.deltaTime;
                if (laserTimer <= 0) {
                    LineRend.enabled = false;
                }
            }
        }
    }

    GameObject GetClosestEnemy(GameObject[] enemies) {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform Target;
    public Vector2 position;
    public Vector2 velocity;
    public Vector2 acceleration;
    public GameObject thisEnemy;

    float minDistance = 100f; // You can change this value as needed

    List<EnemyController> enemyNeighbors = new List<EnemyController>();

    private void Awake()
    {
        thisEnemy = this.gameObject;
        position = transform.position;
        velocity = new Vector2(transform.right.x, transform.right.y);
    }

    private void Update()
    {
        //get Neighbours
        Collider2D[] enemyCols = Physics2D.OverlapCircleAll(this.transform.position, EnemySpawner._awarenessRadius);       
        enemyNeighbors.Clear();
        for (int i = 0; i < enemyCols.Length; i++)
        {
            if (enemyCols[i] != this.GetComponent<Collider2D>())
                enemyNeighbors.Add(enemyCols[i].gameObject.GetComponent<EnemyController>());
        }

        //check rules
        AvoidEdges();

        Vector2 alignment = Align();
        Vector2 cohesion = Cohesian();
        Vector2 seperate = Seperation();
        Vector2 seekForce = Seek();
        Vector2 avoidForce = Avoid();

        acceleration = new Vector2(0, 0);
        acceleration += alignment * EnemySpawner._alignment * EnemySpawner._maxMoveSpeed;
        acceleration += cohesion * EnemySpawner._cohesian * EnemySpawner._maxMoveSpeed;
        acceleration += seperate * EnemySpawner._seperation * EnemySpawner._maxMoveSpeed;
        acceleration += seekForce * EnemySpawner._maxMoveSpeed;
        acceleration += avoidForce * EnemySpawner._maxMoveSpeed;

        

        //apply to vars
        position += velocity * Time.deltaTime;
        velocity += acceleration * Time.deltaTime;
        velocity = Vector2.ClampMagnitude(velocity, EnemySpawner._maxMoveSpeed);

        //apply to transform
        GetComponent<Transform>().position = position;
        transform.right = velocity;
    }

    Vector2 Avoid()
    {
        Vector2 avg = Vector2.zero;

        if (enemyNeighbors.Count > 0)
        {
            foreach (EnemyController enemy in enemyNeighbors)
            {
                if (enemy != null)
                {
                    Vector2 closestPoint = enemy.GetComponent<Collider2D>().ClosestPoint(position);

                    //calculate distance
                    float d = Vector2.Distance(closestPoint, position);
                    if (d < EnemySpawner._awarenessRadius * EnemySpawner._avoidanceRadius)
                    {
                        Vector2 difference = position - closestPoint;
                        difference /= d;
                        avg += difference;
                    }
                }
            }
            avg /= enemyNeighbors.Count;
            avg = avg.normalized * EnemySpawner._maxMoveSpeed;
            avg -= velocity;
            avg = Vector2.ClampMagnitude(avg, EnemySpawner._maxTurnEffect);
        }

        return avg;
    }


    void AvoidEdges() 
    {
        if (position.x < -EnemySpawner._boundSize.x)
        {
            velocity.x = Mathf.Abs(velocity.x);  // make it move right
        }
        else if (position.x > EnemySpawner._boundSize.x)
        {
            velocity.x = -Mathf.Abs(velocity.x);  // make it move left
        }

        if (position.y > EnemySpawner._boundSize.y)
        {
            velocity.y = -Mathf.Abs(velocity.y);  // make it move down
        }
        else if (position.y < -EnemySpawner._boundSize.y)
        {
            velocity.y = Mathf.Abs(velocity.y);  // make it move up
        }
    }


    Vector2 Align()
    {
        Vector2 avg = Vector2.zero;

        if (enemyNeighbors.Count > 0)
        {
            foreach (EnemyController enemy in enemyNeighbors)
            {
                if (enemy != null)
                    avg += enemy.velocity;
            }
            avg /= enemyNeighbors.Count;
            avg = avg.normalized * EnemySpawner._maxMoveSpeed;
            avg -= velocity;
            avg = Vector2.ClampMagnitude(avg, EnemySpawner._maxTurnEffect);
        }

        return avg;
    }

    Vector2 Cohesian()
    {
        Vector2 avg = Vector2.zero;

        if (enemyNeighbors.Count > 0)
        {
            foreach (EnemyController enemy in enemyNeighbors)
            {
                if (enemy != null)
                    avg += enemy.position;
            }
            avg /= enemyNeighbors.Count;
            avg -= position;
            avg = avg.normalized * EnemySpawner._maxMoveSpeed;
            avg -= velocity;
            avg = Vector2.ClampMagnitude(avg, EnemySpawner._maxTurnEffect);
        }

        return avg;
    }

    Vector2 Seperation()
    {
        Vector2 avg = Vector2.zero;

        if (enemyNeighbors.Count > 0)
        {
            foreach (EnemyController enemy in enemyNeighbors)
            {
                if (enemy != null)
                {
                    //calculate distance
                    float d = Vector2.Distance(enemy.position, position);
                    if (d < EnemySpawner._awarenessRadius * EnemySpawner._avoidanceRadius)
                    {
                        Vector2 difference = position - enemy.position;
                        difference /= d;
                        avg += difference;
                    }
                }
            }
            avg /= enemyNeighbors.Count;
            avg = avg.normalized * EnemySpawner._maxMoveSpeed;
            avg -= velocity;
            avg = Vector2.ClampMagnitude(avg, EnemySpawner._maxTurnEffect);
        }

        return avg;
    }
    
    Vector2 Seek()
    {
        if (Target == null) return Vector2.zero; // No target, so don't apply any force

        Vector2 offset = Target.position - transform.position;
        float distance = offset.magnitude;

        if (distance > minDistance)
        {
            Vector2 desired = offset.normalized * EnemySpawner._maxMoveSpeed;
            Vector2 steer = desired - velocity;
            steer = Vector2.ClampMagnitude(steer, EnemySpawner._maxTurnEffect);
            return steer;
        }
        return Vector2.zero;
    }



}
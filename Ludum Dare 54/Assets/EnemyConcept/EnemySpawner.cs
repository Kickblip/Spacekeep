using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Vector2 boundSize;

    [Range(0,1)]
    public float alignment = 1f;
    [Range(0, 1)]
    public float seperation = 1f;
    [Range(0, 1)]
    public float cohesian = 1f;

    public float awarenessRadius = 5f;
    [Range(0, 1)]
    public float avoidanceRadius = 0.5f;
    public float maxMoveSpeed = 2f;
    public float maxTurnEffect = 1f;

    public static Vector2 _boundSize;
    public static float _alignment;
    public static float _seperation;
    public static float _cohesian;
    public static float _maxTurnEffect;
    public static float _awarenessRadius;
    public static float _avoidanceRadius;
    public static float _maxMoveSpeed;
    public int numEnemies = 10;
    public GameObject enemyPrefab;

    private List<EnemyController> enemies = new List<EnemyController>();


    private void Start()
    {
        _boundSize = boundSize;

        _alignment = alignment;
        _seperation = seperation;
        _cohesian = cohesian;

        _avoidanceRadius = avoidanceRadius;
        _awarenessRadius = awarenessRadius;
        _maxMoveSpeed = maxMoveSpeed;
        _maxTurnEffect = maxTurnEffect;

        SpawnEnemies();
    }

    private void Update()
    {
        _boundSize = boundSize;

        _alignment = alignment;
        _seperation = seperation;
        _cohesian = cohesian;

        _avoidanceRadius = avoidanceRadius;
        _awarenessRadius = awarenessRadius;
        _maxMoveSpeed = maxMoveSpeed;
        _maxTurnEffect = maxTurnEffect;
    }

    void SpawnEnemies() 
    {
        //spawn all the boids
        for (int i = 0; i < numEnemies; i++)
        {
            Vector2 enemyPos = new Vector2(
                Random.Range(-_boundSize.x, _boundSize.x), 
                Random.Range(-_boundSize.y, _boundSize.y));
            Instantiate(enemyPrefab, enemyPos, Quaternion.Euler(transform.forward * Random.Range(0, 360)), this.transform);
        }
    }
}
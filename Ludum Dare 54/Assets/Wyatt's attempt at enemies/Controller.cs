using UnityEngine;

public class BoidController : MonoBehaviour
{
    public GameObject boidPrefab;
    public int spawnCount = 10;
    public float spawnRadius = 4.0f;

    [Range(0.1f, 20.0f)]
    public float velocity = 6.0f;

    [Range(0.0f, 0.9f)]
    public float velocityVariation = 0.5f;

    [Range(0.1f, 20.0f)]
    public float rotationCoeff = 4.0f;

    [Range(0.1f, 10.0f)]
    public float neighborDist = 2.0f;

    public LayerMask searchLayer;

    void Start()
    {
        Invoke("SpawnBoids", 0.5f);
    }

    void SpawnBoids()
    {
        for (var i = 0; i < spawnCount; i++)
        {
            float angle = i * Mathf.PI * 2 / spawnCount;
            Vector2 position = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnRadius;
            position += (Vector2)transform.position;
            Spawn(position);
        }
    }

    public GameObject Spawn(Vector2 position)
    {
        var angle = Mathf.Atan2(position.y - transform.position.y, position.x - transform.position.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.Euler(0, 0, angle);
        var boid = Instantiate(boidPrefab, position, rotation) as GameObject;
        boid.GetComponent<BoidBehaviour>().controller = this;
        return boid;
    }
}

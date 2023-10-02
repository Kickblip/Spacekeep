using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public GameObject target;
    public float speed = 5f;
    public float radius = 10f;

    private Vector3 initialPosition;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned.");
            return;
        }

        // Calculate a random angle between 0 to 360 degrees
        float angle = Random.Range(0, 360);

        // Calculate the initial position on the circle around the target
        initialPosition.x = target.transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        initialPosition.y = target.transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        initialPosition.z = target.transform.position.z;

        // Set the initial position of the mover
        transform.position = initialPosition;
    }

    void Update()
    {
        // Move towards the target
        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}

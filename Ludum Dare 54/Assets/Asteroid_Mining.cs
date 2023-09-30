using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int hp = 100;  // Asteroid HP
    public float explosionForce = 10f;
    public float explosionRadius = 5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hp -= 10;
            Debug.Log("Asteroid hit by player. HP: " + hp);  // log the current hp

            if (hp <= 0)
            {
                Debug.Log("Asteroid destroyed");
                Destroy(gameObject);
            }
            else
            {
                Vector2 explosionCenter = collision.contacts[0].point;
                Vector2 direction = (rb.position - explosionCenter).normalized;
                float distance = Vector2.Distance(rb.position, explosionCenter);
                float force = explosionForce / (1 + distance / explosionRadius);
                rb.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
    }
}

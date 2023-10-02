using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float hp = 100;  // Asteroid HP
    public float explosionForce = 10f;
    public float explosionRadius = 5f;
    public OreType[] possibleOreTypes;  // Possible ore types to spawn
    public int max_allowed_ore_types;
    public GameObject oreChunkPrefab; // Ore chunk prefab
    public int numberOfChunks = 3;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hp -= collision.gameObject.GetComponent<PlayerAttributes>().damage;
            Debug.Log("Asteroid hit by player. HP: " + hp);  // log the current hp

            if (hp <= 0)
            {
                Debug.Log("Asteroid destroyed");
                SpawnOreChunks();
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

    private void SpawnOreChunks()
    {
        for (int i = 0; i < numberOfChunks; i++)
        {
            // Add an ore chunk!
            GameObject chunk = Instantiate(oreChunkPrefab, transform.position, Quaternion.identity);
            
            // Assign one of the possible ore types to this chunk
            OreChunk oreChunk = chunk.GetComponent<OreChunk>();
            if (oreChunk != null && possibleOreTypes.Length > 0)
            {
                int randomIndex = Random.Range(0, max_allowed_ore_types);
                oreChunk.oreType = possibleOreTypes[randomIndex];
            }

            // Add some force to make the chunks explode outwards
            Rigidbody2D chunkRb = chunk.GetComponent<Rigidbody2D>();
            if (chunkRb != null)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                float randomForce = Random.Range(explosionForce / 2f, explosionForce);
                chunkRb.AddForce(randomDirection * randomForce, ForceMode2D.Impulse);
            }
        }
    }
}

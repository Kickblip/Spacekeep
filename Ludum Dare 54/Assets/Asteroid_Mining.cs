using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int hp = 100;  // Asteroid HP

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
        }
    }
}

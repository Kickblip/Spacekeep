using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Mothership;  //Drag your mother ship here in the inspector
    private Rigidbody2D rb;
    //ENEMY ATTRIBUTES-------------------------------
    //can modify these on spawn to increase difficulty dynamically
    public float hp = 100;  //Enemy HP
    public float acceleration = 0.4f; //how fast it can add speed
    private float maxSpeed = 40; //will stop accelerating once hit
    private float damage = 10; //how much damage the enemy does
    private float pushPower = 50; //how far enemy is pushed when attacking mothership or taking damage from player

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Mothership = GameObject.Find("Mothership");
    }

    private void Update()
    { 
        if (hp <= 0) {
            Debug.Log("Enemy destroyed");
            Destroy(gameObject);
        }

        if (Mothership != null)
        {
            Vector2 direction = (Mothership.transform.position - transform.position).normalized;
            if (rb.velocity.magnitude < maxSpeed) { //accelerating toward target
                rb.velocity += direction * acceleration;
            }   

            //angle enemy toward mothership
            Vector3 targetPos = Vector3.zero - transform.position;
            float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Mothership")) {
            Vector2 pushDir = -(collision.gameObject.transform.position - transform.position).normalized;
            rb.AddForce(pushDir * pushPower, ForceMode2D.Impulse);

            if (collision.gameObject.CompareTag("Player")) {
                PlayerAttributes atr = collision.gameObject.GetComponent<PlayerAttributes>();
                hp -= atr.damage; //take damage defined for player
                Debug.Log("Enemy hit by player. HP: " + hp);  //log the current hp
            } else { //do damage to mothership
                MothershipController ctrl = collision.gameObject.GetComponent<MothershipController>();
                ctrl.hp -= damage;
                Debug.Log("Mothership HP: " + ctrl.hp);
            }
        }
    }
}

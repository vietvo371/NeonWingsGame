using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 1;
    private Vector2 moveDir;

    public void Init(Vector2 direction)
    {
        moveDir = direction.normalized;
    }

    void Update()
    {
        transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
        Destroy(gameObject, 4f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

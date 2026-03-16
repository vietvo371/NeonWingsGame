using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public float speed = 5f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
       // Lệnh này bắt buộc phải có để tàu bay vào màn hình
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Tự hủy khi bay thoát khỏi màn hình dưới để nhẹ máy
        if (transform.position.y < -10f) 
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // TODO: hiệu ứng nổ, cộng điểm...
        Destroy(gameObject);
    }
}

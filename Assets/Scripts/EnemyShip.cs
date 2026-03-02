using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyShip : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveRange = 2.5f;
    public float verticalSpeed = 0.5f;

    public GameObject enemyBulletPrefab;
    public float fireInterval = 1.5f;

    private Vector3 startPos;
    private float nextFireTime;

    void Start()
    {
        startPos = transform.position;
        nextFireTime = Time.time + fireInterval;
    }

    void Update()
    {
        // Move
        float offsetX = Mathf.Sin(Time.time * moveSpeed) * moveRange;
        Vector3 pos = startPos;
        pos.x += offsetX;
        pos.y -= verticalSpeed * Time.deltaTime;
        transform.position = pos;

        // Fire
        if (Time.time >= nextFireTime)
        {
            FireAtPlayer();
            nextFireTime = Time.time + fireInterval;
        }
    }

    void FireAtPlayer()
    {
        if (enemyBulletPrefab == null) return;

        // Tìm player - dùng API mới để tránh cảnh báo obsolete
        Player player = Object.FindFirstObjectByType<Player>();
        if (player == null) return;

        // Hướng bắn từ enemy tới player
        Vector3 dir = (player.transform.position - transform.position).normalized;

        // Tạo bullet
        GameObject bulletObj = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);

        EnemyBullet bullet = bulletObj.GetComponent<EnemyBullet>();
        if (bullet != null)
        {
            bullet.Init(dir);
        }
    }
}

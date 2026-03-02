using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1.5f; // mỗi 1.5s sinh 1 địch
    public float xPadding = 0.5f;      // chừa lề 2 bên để enemy không xuất hiện nửa ngoài màn hình

    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        if (Camera.main == null || enemyPrefab == null) return;

        // lấy biên trái/phải phía trên màn hình
        float zDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 leftTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, zDistance));
        Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, zDistance));

        float minX = leftTop.x + xPadding;
        float maxX = rightTop.x - xPadding;

        // CHỈNH DÒNG NÀY: spawn ngay mép trên hoặc hơi trong 1 chút
        float y = leftTop.y + 0.1f ;        // hoặc leftTop.y + 0.1f cho an toàn

        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(randomX, y, transform.position.z);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}

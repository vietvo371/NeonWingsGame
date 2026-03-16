using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1.5f; // mỗi 1.5s sinh 1 địch
    public float xPadding = 0.5f;      // chừa lề 2 bên để enemy không xuất hiện nửa ngoài màn hình
    public float topScreenPercentage = 0.3f; // Tỷ lệ phần trên màn hình để spawn enemy

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

        // Lấy biên trái/phải phía trên màn hình
        float zDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 leftTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, zDistance));
        Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, zDistance));

        float minX = leftTop.x + xPadding;
        float maxX = rightTop.x - xPadding;

        // Lấy chiều cao của prefab
        float prefabHeight = enemyPrefab.GetComponent<Renderer>().bounds.size.y;

        // Giới hạn vị trí y để chỉ spawn ở phần trên của màn hình
        float maxY = leftTop.y - (prefabHeight / 2);
        float minY = leftTop.y - (Camera.main.orthographicSize * topScreenPercentage);

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(randomX, randomY, transform.position.z);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}

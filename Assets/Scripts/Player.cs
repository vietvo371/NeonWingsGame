using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Import namespace cho Image
using TMPro; // Import TextMeshPro namespace

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint; // gán ship2-flame vào đây
    public float fireRate = 0.2f;

    // Máu của player
    public int maxHealth = 5;
    private int currentHealth;

    public TextMeshProUGUI hpText; // Tham chiếu đến TextMeshPro UI (hp_points)
    public Image healthFillImage; // Tham chiếu đến Image của thanh Fill

    private float nextFireTime = 0f; // thời điểm được phép bắn tiếp theo
    public AudioClip shootSfx;   // ở phần biến public
    private AudioSource audioSource; // ở phần biến private
    private FirePointEffect firePointEffect;

    void Start()
    {
        // Khởi tạo máu player
        currentHealth = maxHealth;
        UpdateHPUI(); // Cập nhật hiển thị máu

        Debug.Log("Player Start() - Application.isPlaying: " + Application.isPlaying);
        audioSource = GetComponent<AudioSource>();
        if (firePoint != null)
        {
            firePointEffect = firePoint.GetComponent<FirePointEffect>();
        }
    }

    void Update()
    {
        try
        {
            SimpleMovement();
            Debug.Log("SimpleMovement() completed successfully");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error in SimpleMovement: " + e.Message);
            Debug.LogError("Stack trace: " + e.StackTrace);
        }

        // Bắn liên tục tự động theo fireRate
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }

        Debug.Log("UPDATE END - Frame: " + Time.frameCount);
    }

    void SimpleMovement()
    {
        Debug.Log("SimpleMovement() START");

        if (Camera.main == null)
        {
            Debug.LogError("Camera.main is null in SimpleMovement!");
            return;
        }

        // Lấy vị trí chuột bằng Input System mới
        if (Mouse.current == null)
        {
            Debug.LogError("Mouse.current is null - kiểm tra Input System");
            return;
        }

        Vector2 mouseScreenPos2D = Mouse.current.position.ReadValue();

        // Chuyển sang Vector3 để dùng với ScreenToWorldPoint
        Vector3 mouseScreenPos = new Vector3(mouseScreenPos2D.x, mouseScreenPos2D.y, 0f);

        // Vì là game 2D, camera thường nhìn vào trục Z, nên ta set z theo khoảng cách từ camera tới player
        float zDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        mouseScreenPos.z = zDistance;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        // Đảm bảo cùng z với player
        mouseWorldPos.z = transform.position.z;

        Vector3 currentPos = transform.position;
        float distance = Vector3.Distance(currentPos, mouseWorldPos);

        transform.position = Vector3.MoveTowards(currentPos, mouseWorldPos, moveSpeed * Time.deltaTime);

        // Sau khi tính currentPos, mouseWorldPos và set transform.position,
        // ta giới hạn vị trí player trong khung hình camera.
        ClampToScreenBounds();

        Debug.Log("SimpleMovement() END");
    }

    void ClampToScreenBounds()
    {
        if (Camera.main == null) return;

        // Lấy biên camera theo world units
        Camera cam = Camera.main;
        float z = transform.position.z;

        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, Mathf.Abs(cam.transform.position.z - z)));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, Mathf.Abs(cam.transform.position.z - z)));

        float minX = bottomLeft.x;
        float maxX = topRight.x;
        float minY = bottomLeft.y;
        float maxY = topRight.y;

        // Nếu muốn chừa biên cho kích thước tàu (ví dụ nửa chiều rộng/chiều cao), có thể trừ/ cộng một offset nhỏ ở đây.

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    void Fire()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("BulletPrefab hoặc FirePoint chưa được gán trên Player");
            return;
        }

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // chơi tiếng bắn
        if (audioSource != null && shootSfx != null)
        {
            audioSource.PlayOneShot(shootSfx);
        }

        if (firePointEffect != null)
        {
            firePointEffect.Flash(0.05f); // nháy lửa 0.05 giây mỗi viên
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage: " + damage + " | HP: " + currentHealth + "/" + maxHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        UpdateHPUI(); // Cập nhật hiển thị máu sau khi nhận sát thương
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        // Đảm bảo máu không vượt quá maxHealth
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHPUI(); // Cập nhật hiển thị máu sau khi hồi máu
    }

    private void UpdateHPUI()
    {
        // Cập nhật Text hiển thị máu
        if (hpText != null)
        {
            hpText.text = $"{currentHealth}/{maxHealth}";
        }

        // Cập nhật thanh Fill
        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        // TODO: hiệu ứng nổ, game over UI...
        // Tạm thời: disable player
        gameObject.SetActive(false);
    }
}

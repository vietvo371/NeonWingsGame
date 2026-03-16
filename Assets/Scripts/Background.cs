using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // Tốc độ cuộn
    private MeshRenderer meshRenderer;
    private Vector2 offset;

    void Start()
    {
        // Chuyển sang dùng MeshRenderer để thay đổi Offset của vật liệu
        meshRenderer = GetComponent<MeshRenderer>();
        FitToCamera();
    }

    void Update()
    {
        // Tính toán offset theo thời gian
        offset.y += scrollSpeed * Time.deltaTime;
        // Áp dụng vào vật liệu của hình nền
        meshRenderer.material.mainTextureOffset = offset;
    }

    void FitToCamera()
    {
        var cam = Camera.main;
        if (cam == null) return;

        // Tính toán kích thước để phủ kín camera
        float worldH = cam.orthographicSize * 2f;
        float worldW = worldH * cam.aspect;
        transform.localScale = new Vector3(worldW, worldH, 1f);

        // Đảm bảo luôn nằm ở giữa camera
        transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 10f);
    }
}
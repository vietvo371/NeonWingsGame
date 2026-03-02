using UnityEngine;

public class Background : MonoBehaviour
{
    void Start()
    {
        FitToCamera();
    }

    void FitToCamera()
    {
        var cam = Camera.main;
        var sr = GetComponent<SpriteRenderer>();
        if (cam == null || sr == null || sr.sprite == null) return;

        // reset scale
        transform.localScale = Vector3.one;

        // size sprite trong world
        float spriteW = sr.sprite.bounds.size.x;
        float spriteH = sr.sprite.bounds.size.y;

        // size màn hình trong world
        float worldH = cam.orthographicSize * 2f;
        float worldW = worldH * cam.aspect;

        float scaleX = worldW / spriteW;
        float scaleY = worldH / spriteH;
        float scale = Mathf.Max(scaleX, scaleY);   // không méo, phủ kín

        transform.localScale = new Vector3(scale, scale, 1f);

        // đặt đúng giữa camera
        var camPos = cam.transform.position;
        transform.position = new Vector3(camPos.x, camPos.y, transform.position.z);
    }
}

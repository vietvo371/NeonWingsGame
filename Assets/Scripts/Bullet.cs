using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    
    void Update()
    {
        // Luôn bay lên trên của màn hình (trục Y thế giới)
        transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
        Destroy(gameObject, 3f);
    }
}
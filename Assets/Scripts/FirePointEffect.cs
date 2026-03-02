using UnityEngine;
using System.Collections;

public class FirePointEffect : MonoBehaviour
{
    public enum EffectMode
    {
        EngineIdle,   // lửa động cơ cháy liên tục, nhấp nháy nhẹ
        MuzzleFlash   // chớp sáng khi bắn (như hiện tại)
    }

    public EffectMode mode = EffectMode.MuzzleFlash;
    public float idleMinIntensity = 0.6f;  // cho EngineIdle
    public float idleMaxIntensity = 1.0f;
    public float idleFlickerSpeed = 10f;

    private SpriteRenderer sr;
    private Coroutine currentRoutine;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        if (mode == EffectMode.EngineIdle)
        {
            // Động cơ: luôn bật, nhưng scale/alpha nhấp nháy nhẹ
            sr.enabled = true;
            currentRoutine = StartCoroutine(EngineIdleRoutine());
        }
        else
        {
            // Muzzle flash: tắt, chỉ bật khi bắn
            sr.enabled = false;
        }
    }

    // Dùng cho chế độ MuzzleFlash (gọi từ Player.Fire)
    public void Flash(float duration)
    {
        if (mode != EffectMode.MuzzleFlash || sr == null || !gameObject.activeInHierarchy)
            return;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(FlashRoutine(duration));
    }

    private IEnumerator FlashRoutine(float duration)
    {
        sr.enabled = true;
        yield return new WaitForSeconds(duration);
        sr.enabled = false;
    }

    // Động cơ nhấp nháy nhẹ (EngineIdle)
    private IEnumerator EngineIdleRoutine()
    {
        // Giữ lửa bật, nhưng thay đổi alpha/scale nhẹ theo thời gian
        Color baseColor = sr.color;
        Vector3 baseScale = transform.localScale;

        while (true)
        {
            float t = Mathf.PerlinNoise(Time.time * idleFlickerSpeed, 0f);
            float intensity = Mathf.Lerp(idleMinIntensity, idleMaxIntensity, t);

            // thay đổi alpha
            Color c = baseColor;
            c.a = intensity;
            sr.color = c;

            // có thể scale nhẹ theo intensity cho đẹp hơn
            float scaleMul = Mathf.Lerp(0.9f, 1.1f, intensity);
            transform.localScale = baseScale * scaleMul;

            yield return null;
        }
    }
}
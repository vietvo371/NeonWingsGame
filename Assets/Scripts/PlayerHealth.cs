using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI hpText; // Tham chiếu đến TextMeshPro UI (hp_points)
    public int maxHP = 5; // Máu tối đa
    private int currentHP; // Máu hiện tại

    void Start()
    {
        // Khởi tạo máu ban đầu
        currentHP = maxHP;
        UpdateHPText();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        // Đảm bảo máu không giảm xuống dưới 0
        if (currentHP < 0)
        {
            currentHP = 0;
            GameOver();
        }

        UpdateHPText();
    }

    public void Heal(int amount)
    {
        currentHP += amount;

        // Đảm bảo máu không vượt quá maxHP
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        UpdateHPText();
    }

    private void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = $"{currentHP}/{maxHP}";
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Thêm logic kết thúc game tại đây (ví dụ: hiển thị màn hình Game Over, dừng game, v.v.)
    }
}

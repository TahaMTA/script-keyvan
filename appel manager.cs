using UnityEngine;
using TMPro;

public class AppleManager : MonoBehaviour
{
    public static AppleManager Instance;

    [Header("Apple")]
    public int appleCount = 0;

    public int applesForHealth = 10;

    [Header("UI")]
    public TMP_Text appleText;

    [Header("Player")]
    public player Player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void CollectApple()
    {
        appleCount++;

        // هر 10 سیب = یک واحد سلامتی
        if (appleCount >= applesForHealth)
        {
            if (Player.Health < 3)
            {
                appleCount -= applesForHealth;
                Player.Health += 1;
            }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        appleText.text = appleCount.ToString();
    }
}
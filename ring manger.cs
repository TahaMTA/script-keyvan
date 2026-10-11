using UnityEngine;
using TMPro;

public class RingManager : MonoBehaviour
{
    public static RingManager Instance;

    [Header("Rings")]
    public int ringCount = 0;
    public int shieldCost = 25;

    [Header("UI")]
    public TMP_Text ringText;

    [Header("Shield")]
    public GameObject shieldObject;
    public float shieldDuration = 10f;

    [Header("Player")]
    public player Player;

    private bool shieldActive = false;
    public AudioSource EFX;
    public AudioClip ShildSound;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();

        if (shieldObject != null)
            shieldObject.SetActive(false);
    }

    private void Update()
    {
        // فعال کردن سپر با Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivateShield();
        }
    }

    public void CollectRing()
    {
        ringCount++;
        UpdateUI();
    }

    public void ActivateShield()
    {
        
        // اگر سپر از قبل فعال است
        if (shieldActive)
            return;

        // اگر رینگ کافی نداریم
        if (ringCount < shieldCost)
            return;

        // مصرف 25 رینگ
        ringCount -= shieldCost;
        UpdateUI();

        // فعال کردن سپر
        shieldActive = true;
        Player.HasShield = true;

        if (shieldObject != null)
            shieldObject.SetActive(true);

        Invoke(nameof(DeactivateShield), shieldDuration);
        EFX.PlayOneShot(ShildSound);
    }

    private void DeactivateShield()
    {
        shieldActive = false;
        Player.HasShield = false;

        if (shieldObject != null)
            shieldObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if (ringText != null)
            ringText.text = ringCount.ToString();
    }
}
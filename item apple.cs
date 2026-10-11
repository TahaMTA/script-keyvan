using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject EffectPrefab;

    [Header("Sound")]
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<player>() != null)
        {
            // اضافه کردن سیب به شمارنده
            AppleManager.Instance.CollectApple();

            // افکت برداشتن آیتم
            Instantiate(EffectPrefab, transform.position, Quaternion.identity);

            // پخش صدا
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // حذف آیتم
            Destroy(gameObject);
        }
    }
}
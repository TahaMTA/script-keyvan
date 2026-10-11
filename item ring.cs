using UnityEngine;

public class Ring : MonoBehaviour
{
    public GameObject EffectPrefab;

    [Header("Sound")]
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<player>() != null)
        {
            RingManager.Instance.CollectRing();

            Instantiate(EffectPrefab, transform.position, Quaternion.identity);

            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject);
        }
    }
}
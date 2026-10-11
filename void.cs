using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player player = collision.GetComponent<player>();

            if (player != null)
            {
                player.Health = 0;
            }
        }
    }
}
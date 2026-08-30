using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip dieSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null && dieSound != null)
                audioSource.PlayOneShot(dieSound);

            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();

            if (respawn != null)
                respawn.Respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (audioSource != null && dieSound != null)
                audioSource.PlayOneShot(dieSound);

            PlayerRespawn respawn = collision.gameObject.GetComponent<PlayerRespawn>();

            if (respawn != null)
                respawn.Respawn();
        }
    }
}
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Game Sounds")]
    public AudioClip jumpSound;
    public AudioClip dieSound;
    public AudioClip buttonClickSound;
    public AudioClip levelClearedSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PlayJump()
    {
        if (audioSource != null && jumpSound != null)
            audioSource.PlayOneShot(jumpSound);
    }

    public void PlayDie()
    {
        if (audioSource != null && dieSound != null)
            audioSource.PlayOneShot(dieSound);
    }

    public void PlayButtonClick()
    {
        if (audioSource != null && buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);
    }

    public void PlayLevelCleared()
    {
        if (audioSource != null && levelClearedSound != null)
            audioSource.PlayOneShot(levelClearedSound);
    }
}
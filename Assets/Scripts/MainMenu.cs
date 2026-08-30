using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [Header("Camera")]
    public Camera mainCamera;

    [Header("Room 3 Camera Position")]
    public float room3X;
    public float room3Y;

    [Header("Camera Movement")]
    public float moveDuration = 2f;

    [Header("Menu UI")]
    public GameObject menuUI;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    private bool moving = false;

    public void Play()
    {
        if (moving)
            return;

        if (audioSource != null && buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);

        if (menuUI != null)
            menuUI.SetActive(false);

        StartCoroutine(MoveCamera());
    }

    public void Quit()
    {
        if (audioSource != null && buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);

        Application.Quit();
    }

    private IEnumerator MoveCamera()
    {
        moving = true;

        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera == null)
            yield break;

        Vector3 startPosition = mainCamera.transform.position;

        Vector3 targetPosition = new Vector3(
            room3X,
            room3Y,
            startPosition.z
        );

        float timer = 0f;

        while (timer < moveDuration)
        {
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / moveDuration);

            t = Mathf.SmoothStep(0f, 1f, t);

            mainCamera.transform.position = Vector3.Lerp(
                startPosition,
                targetPosition,
                t
            );

            yield return null;
        }

        mainCamera.transform.position = targetPosition;

        moving = false;
    }
}
using UnityEngine;
using System.Collections;

public class ExitCameraDoor : MonoBehaviour
{
    [Header("Camera")]
    public Camera mainCamera;

    [Header("Camera Target Position")]
    public float cameraX;
    public float cameraY;

    [Header("Cinematic Settings")]
    public float moveDuration = 2f;

    [Header("Player")]
    public GameObject player;

    [Header("Player Spawn Position")]
    public float spawnX;
    public float spawnY;

    private bool triggered = false;
    private bool moving = false;

    private Vector3 startCameraPosition;
    private Vector3 targetCameraPosition;
    private float moveTimer;

    private Rigidbody2D playerRb;

    private void Start()
    {
        if (player != null)
            playerRb = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        triggered = true;

        StartCoroutine(TeleportPlayer());
        StartCameraCinematic();
    }

    private IEnumerator TeleportPlayer()
    {
        if (player == null)
            yield break;

        if (playerRb == null)
            playerRb = player.GetComponent<Rigidbody2D>();

        Vector3 exactSpawnPosition = new Vector3(
            spawnX,
            spawnY,
            player.transform.position.z
        );

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
            playerRb.angularVelocity = 0f;

            playerRb.simulated = false;

            player.transform.position = exactSpawnPosition;

            yield return null;

            player.transform.position = exactSpawnPosition;

            playerRb.position = new Vector2(spawnX, spawnY);

            Physics2D.SyncTransforms();

            playerRb.simulated = true;
            playerRb.linearVelocity = Vector2.zero;
        }
        else
        {
            player.transform.position = exactSpawnPosition;
        }
    }

    private void StartCameraCinematic()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera == null)
            return;

        startCameraPosition = mainCamera.transform.position;

        targetCameraPosition = new Vector3(
            cameraX,
            cameraY,
            mainCamera.transform.position.z
        );

        moveTimer = 0f;
        moving = true;
    }

    private void Update()
    {
        if (!moving)
            return;

        moveTimer += Time.deltaTime;

        float progress = Mathf.Clamp01(moveTimer / moveDuration);

        progress = Mathf.SmoothStep(0f, 1f, progress);

        mainCamera.transform.position = Vector3.Lerp(
            startCameraPosition,
            targetCameraPosition,
            progress
        );

        if (progress >= 1f)
        {
            mainCamera.transform.position = targetCameraPosition;
            moving = false;
        }
    }
}
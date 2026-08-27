using UnityEngine;

public class Room3Button : MonoBehaviour
{
    [Header("Button Visual")]
    public GameObject beforePressPrefab;
    public GameObject afterPressPrefab;

    [Header("Room 3")]
    public GameObject wall;
    public SpikeFall spike;
    public GameObject disappearingPlatform;

    [Header("Door")]
    public GameObject closedDoor;
    public GameObject openDoor;

    [Header("Who Can Press")]
    public bool playerCanPress = true;
    public bool boxCanPress = true;
    public bool spikesCanPress = true;

    private bool isPressed = false;

    private void Start()
    {
        if (beforePressPrefab != null)
            beforePressPrefab.SetActive(true);

        if (afterPressPrefab != null)
            afterPressPrefab.SetActive(false);

        if (closedDoor != null)
            closedDoor.SetActive(true);

        if (openDoor != null)
            openDoor.SetActive(false);

        if (disappearingPlatform != null)
            disappearingPlatform.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPressed)
            return;

        if (other.CompareTag("Player") && playerCanPress)
        {
            ActivateButton();
        }
        else if (other.CompareTag("Box") && boxCanPress)
        {
            ActivateButton();
        }
        else if (other.CompareTag("Spikes") && spikesCanPress)
        {
            ActivateButton();
        }
    }

    private void ActivateButton()
    {
        if (isPressed)
            return;

        isPressed = true;

        if (beforePressPrefab != null)
            beforePressPrefab.SetActive(false);

        if (afterPressPrefab != null)
            afterPressPrefab.SetActive(true);

        if (wall != null)
            wall.SetActive(true);

        if (spike != null)
            spike.Fall();

        if (disappearingPlatform != null)
            disappearingPlatform.SetActive(false);

        if (closedDoor != null)
            closedDoor.SetActive(false);

        if (openDoor != null)
            openDoor.SetActive(true);
    }
}
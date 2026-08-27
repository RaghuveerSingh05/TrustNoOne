using UnityEngine;

public class Room1RGB : MonoBehaviour
{
    public enum ButtonColor
    {
        Red,
        Green,
        Blue
    }

    [Header("Button")]
    public ButtonColor buttonColor;
    public GameObject normalButton;
    public GameObject pressedButton;

    [Header("Room 1")]
    public GameObject closedDoor1;
    public GameObject closedDoor2;
    public GameObject exit;

    [Header("Uncle Red")]
    public GameObject uncleRed;

    private static int currentStep = 0;
    private static bool completed = false;

    private bool pressed = false;

    private void Start()
    {
        if (normalButton != null)
            normalButton.SetActive(true);

        if (pressedButton != null)
            pressedButton.SetActive(false);

        if (closedDoor1 != null)
            closedDoor1.SetActive(true);

        if (closedDoor2 != null)
            closedDoor2.SetActive(true);

        if (exit != null)
            exit.SetActive(false);

        if (uncleRed != null)
        {
            Rigidbody2D uncleRb = uncleRed.GetComponent<Rigidbody2D>();

            if (uncleRb != null)
                uncleRb.bodyType = RigidbodyType2D.Static;

            BoxCollider2D boxCollider = uncleRed.GetComponent<BoxCollider2D>();

            if (boxCollider != null)
                boxCollider.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (pressed || completed)
            return;

        CheckButton();
    }

    private void CheckButton()
    {
        if (buttonColor == ButtonColor.Red && currentStep == 0)
        {
            PressButton();
            currentStep = 1;
        }
        else if (buttonColor == ButtonColor.Green && currentStep == 1)
        {
            PressButton();
            currentStep = 2;
        }
        else if (buttonColor == ButtonColor.Blue && currentStep == 2)
        {
            PressButton();
            currentStep = 3;
            CompleteRoom();
        }
        else
        {
            ResetSequence();
        }
    }

    private void PressButton()
    {
        pressed = true;

        if (normalButton != null)
            normalButton.SetActive(false);

        if (pressedButton != null)
            pressedButton.SetActive(true);
    }

    private void ResetSequence()
    {
        currentStep = 0;

        Room1RGB[] buttons = FindObjectsOfType<Room1RGB>();

        foreach (Room1RGB button in buttons)
        {
            button.pressed = false;

            if (button.normalButton != null)
                button.normalButton.SetActive(true);

            if (button.pressedButton != null)
                button.pressedButton.SetActive(false);
        }
    }

    private void CompleteRoom()
    {
        completed = true;

        if (closedDoor1 != null)
            closedDoor1.SetActive(false);

        if (closedDoor2 != null)
            closedDoor2.SetActive(false);

        if (exit != null)
            exit.SetActive(true);

        if (uncleRed != null)
        {
            Rigidbody2D uncleRb = uncleRed.GetComponent<Rigidbody2D>();

            if (uncleRb != null)
                uncleRb.bodyType = RigidbodyType2D.Dynamic;

            BoxCollider2D boxCollider = uncleRed.GetComponent<BoxCollider2D>();

            if (boxCollider != null)
                boxCollider.isTrigger = false;
        }
    }
}
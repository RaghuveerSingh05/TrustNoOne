using UnityEngine;

public class SpikeFall : MonoBehaviour
{
    [Header("Bottom Position")]
    public float bottomX;
    public float bottomY;

    [Header("Fall Settings")]
    public float fallSpeed = 8f;

    private bool falling = false;

    private Vector3 bottomPosition;

    private void Start()
    {
        bottomPosition = new Vector3(
            bottomX,
            bottomY,
            transform.position.z
        );
    }

    private void Update()
    {
        if (!falling)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            bottomPosition,
            fallSpeed * Time.deltaTime
        );

        // Destroy when the spike reaches the bottom
        if (Vector3.Distance(transform.position, bottomPosition) < 0.01f)
        {
            Destroy(gameObject);
        }
    }

    public void Fall()
    {
        if (!falling)
        {
            falling = true;
        }
    }
}
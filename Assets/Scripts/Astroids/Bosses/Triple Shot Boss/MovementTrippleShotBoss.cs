using UnityEngine;

public class MovementTrippleShotBoss : MonoBehaviour
{
    public Rigidbody2D boss;
    public float movementSpeed;
    public float smoothing;
    public float changeDirectionInterval;
    private Vector2 movementDirection;
    private System.Random rnd = new System.Random();
    private float nextChangeTime;
    private float leftBoundary;
    private float rightBoundary;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
        CalculateCameraBounds();
        nextChangeTime = Time.time + changeDirectionInterval;
        ChangeDirection();
    }

    void Update()
    {
        if (Time.time >= nextChangeTime)
        {
            ChangeDirection();
            nextChangeTime = Time.time + changeDirectionInterval;
        }
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = movementDirection * movementSpeed;
        boss.velocity = Vector2.Lerp(boss.velocity, targetVelocity, smoothing);

        // Clamp the boss position to stay within camera bounds
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftBoundary, rightBoundary);
        transform.position = clampedPosition;
    }

    private void ChangeDirection()
    {
        float movementX = rnd.Next(-1, 2); // Randomly choose -1, 0, or 1 for movement direction
        movementDirection = new Vector2(movementX, 0).normalized;
    }

    private void CalculateCameraBounds()
    {
        Vector3 leftBoundaryWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 rightBoundaryWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));
        leftBoundary = leftBoundaryWorldPosition.x;
        rightBoundary = rightBoundaryWorldPosition.x;
    }
}
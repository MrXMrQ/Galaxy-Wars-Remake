using System.Collections;
using UnityEngine;

public class MovementTripleShotBoss : MonoBehaviour
{
    public Rigidbody2D boss;
    public GameObject projectilePrefab;
    public float movementSpeed;
    public float smoothing;
    public float changeDirectionInterval;
    public float shootingCooldown;
    private float bossWidth;
    private bool isShooting;
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

        bossWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    void Update()
    {
        if (Time.time >= nextChangeTime)
        {
            ChangeDirection();
            nextChangeTime = Time.time + changeDirectionInterval;
        }

        if (!isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = movementDirection * movementSpeed;
        boss.velocity = Vector2.Lerp(boss.velocity, targetVelocity, smoothing);

        // Clamp the boss position to stay within camera bounds
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftBoundary + bossWidth, rightBoundary - bossWidth);
        transform.position = clampedPosition;
    }

    private void ChangeDirection()
    {
        // Check if the boss is at the left or right boundary and force the movement direction if necessary
        if (transform.position.x <= leftBoundary + bossWidth)
        {
            // Move right
            movementDirection = Vector2.right;
        }
        else if (transform.position.x >= rightBoundary - bossWidth)
        {
            // Move left
            movementDirection = Vector2.left;
        }
        else
        {
            // Randomly choose -1 or 1 for movement direction
            float movementX = GenerateRandomNumber();
            movementDirection = new Vector2(movementX, 0).normalized;
        }
    }

    private int GenerateRandomNumber()
    {
        int movementX = rnd.Next(-1, 2);
        return movementX == 0 ? -1 : movementX;
    }

    private void CalculateCameraBounds()
    {
        Vector3 leftBoundaryWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 rightBoundaryWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));
        leftBoundary = leftBoundaryWorldPosition.x;
        rightBoundary = rightBoundaryWorldPosition.x;
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        float x = transform.position.x;
        float y = transform.position.y + 0.5f;
        Vector2 pos = new Vector2(x, y);

        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), pos, -3);
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), pos, 0);
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), pos, +3);

        yield return new WaitForSeconds(shootingCooldown);
        isShooting = false;
    }

    private void MakeInstance(GameObject projectile, Vector2 pos, float xOffset)
    {
        Vector2 moveDirection = new Vector2(PlayerController.player.transform.position.x, PlayerController.player.transform.position.y) - pos;
        Debug.Log("boom");
        BossProjectile projScript = projectile.GetComponent<BossProjectile>();

        if (projScript != null)
        {
            projScript.SetDirection(new Vector2(moveDirection.x - xOffset, moveDirection.y));
        }
    }
}
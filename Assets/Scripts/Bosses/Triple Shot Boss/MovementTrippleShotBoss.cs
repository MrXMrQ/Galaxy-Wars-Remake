using System.Collections;
using UnityEngine;

public class MovementTripleShotBoss : MonoBehaviour
{
    public Rigidbody2D boss;
    public GameObject projectilePrefab;
    public GameObject bombPrefab;
    public float movementSpeed;
    public float smoothing;
    public float changeDirectionInterval;
    public float shootingCooldown;
    public float nextChangeTimePhase;
    private float lastPhaseChangeTime;

    private enum BossPhase { Phase1, Phase2 }
    private BossPhase currentPhase;

    private float bossWidth;
    private bool isShooting;
    private Vector2 movementDirection;
    private Vector2 attackDirection;
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
        ChangePhase();
        bossWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    void Update()
    {
        if (Time.time - lastPhaseChangeTime >= nextChangeTimePhase)
        {
            ChangePhase();
        }

        if (Time.time >= nextChangeTime)
        {
            ChangeDirection();
            nextChangeTime = Time.time + changeDirectionInterval;
        }

        switch (currentPhase)
        {
            case BossPhase.Phase1:
                Phase1Behavior();
                break;
            case BossPhase.Phase2:
                Phase2Behavior();
                break;
        }

        attackDirection = PlayerController.player.transform.position - transform.position;
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

    private void ChangePhase()
    {
        currentPhase = (BossPhase)rnd.Next(0, 2);
        lastPhaseChangeTime = Time.time;
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

    private void Phase1Behavior()
    {
        if (!isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    private void Phase2Behavior()
    {
        if (!isShooting)
        {
            StartCoroutine(Bomb());
        }
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
        float y = transform.position.y - 2.5f; //?
        Vector2 pos = new Vector2(x, y);

        MakeInstanceProjectile(Instantiate(projectilePrefab, new Vector2(x - 3, y), Quaternion.identity), pos, -3);
        MakeInstanceProjectile(Instantiate(projectilePrefab, new Vector2(x, y), Quaternion.identity), pos, 0);
        MakeInstanceProjectile(Instantiate(projectilePrefab, new Vector2(x + 3, y), Quaternion.identity), pos, +3);

        yield return new WaitForSeconds(shootingCooldown);
        isShooting = false;
    }

    private IEnumerator Bomb()
    {
        isShooting = true;
        float x = transform.position.x;
        float y = transform.position.y - 2.5f; //?
        Vector2 pos = new Vector2(x, y);

        MakeInstanceBomb(Instantiate(bombPrefab, new Vector2(x - 3, y), Quaternion.identity), pos, -3);
        MakeInstanceBomb(Instantiate(bombPrefab, new Vector2(x, y), Quaternion.identity), pos, 0);
        MakeInstanceBomb(Instantiate(bombPrefab, new Vector2(x + 3, y), Quaternion.identity), pos, +3);

        yield return new WaitForSeconds(shootingCooldown);
        isShooting = false;
    }

    private void MakeInstanceProjectile(GameObject projectile, Vector2 pos, float xOffset)
    {
        Vector2 moveDirection = new Vector2(PlayerController.player.transform.position.x, PlayerController.player.transform.position.y) - pos;
        BossProjectile projScript = projectile.GetComponent<BossProjectile>();

        if (projScript != null)
        {
            projScript.SetDirection(new Vector2(moveDirection.x - xOffset, moveDirection.y));
        }
    }

    private void MakeInstanceBomb(GameObject bomb, Vector2 pos, float xOffset)
    {
        Vector2 moveDirection = new Vector2(PlayerController.player.transform.position.x, PlayerController.player.transform.position.y) - pos;
        Bomb bombScript = bomb.GetComponent<Bomb>();

        if (bombScript != null)
        {
            bombScript.SetDirection(new Vector2(moveDirection.x - xOffset, moveDirection.y));
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.knockBack.CallKnockBack(attackDirection, Vector2.zero, new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical")));
        }
    }
}
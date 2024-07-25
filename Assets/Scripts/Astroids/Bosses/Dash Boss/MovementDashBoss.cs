using UnityEngine;

public class MovementDashBoss : MonoBehaviour
{
    public Rigidbody2D boss;

    [Header("Movement")]
    public float changeDirectionInterval;
    public float dashSpeed;
    private float nextChangeTime;
    private Vector2 targetPosition;
    private Vector2 attackDirection;
    private bool isDashing;

    [Header("Camera")]
    private Camera mainCamera;
    private float leftBoundary;
    private float rightBoundary;
    private float bossWidth;

    void Start()
    {
        mainCamera = Camera.main;
        CalculateCameraBounds();
        Dash();
        nextChangeTime = Time.time + changeDirectionInterval;
        bossWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    void Update()
    {
        if (Time.time >= nextChangeTime && !isDashing)
        {
            Dash();
            nextChangeTime = Time.time + changeDirectionInterval;
        }

        if (isDashing)
        {
            Debug.Log("if");
            float distance = Vector2.Distance(boss.position, targetPosition);
            if (distance < 1f)
            {
                StopDash();
            }
        }

        attackDirection = PlayerController.player.transform.position - transform.position;
    }

    private void CalculateCameraBounds()
    {
        Vector3 leftBoundaryWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 rightBoundaryWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));
        leftBoundary = leftBoundaryWorldPosition.x;
        rightBoundary = rightBoundaryWorldPosition.x;
    }

    private void Dash()
    {
        targetPosition = PlayerController.player.transform.position;
        isDashing = true;
        Vector2 movementDirection = (targetPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        boss.velocity = new Vector2(movementDirection.x * dashSpeed, movementDirection.y * dashSpeed);
    }

    private void StopDash()
    {
        boss.velocity = Vector2.zero;
        isDashing = false;
        Debug.Log("Stopped at position: " + transform.position);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.knockBack.CallKnockBack(attackDirection, Vector2.zero, new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical")));
        }
    }
}
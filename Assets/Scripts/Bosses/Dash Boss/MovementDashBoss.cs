using System.Collections;
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

    [Header("Shot")]
    public GameObject projectilePrefab;

    void Start()
    {
        //Dash();
        nextChangeTime = Time.time + changeDirectionInterval;
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
            float distance = Vector2.Distance(boss.position, targetPosition);
            if (distance < 1f)
            {
                Shoot();
                StopDash();
            }
        }

        attackDirection = PlayerController.player.transform.position - transform.position;
    }

    private void Dash()
    {
        targetPosition = PlayerController.player.transform.position;
        isDashing = true;
        Vector2 movementDirection = (targetPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        boss.velocity = new Vector2(movementDirection.x * dashSpeed, movementDirection.y * dashSpeed);
    }

    private void Shoot()
    {
        float x = transform.position.x;
        float y = transform.position.y + 0.5f; //?
        Vector2 pos = new Vector2(x, y);

        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), Vector2.up);
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), Vector2.down);
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), Vector2.left);
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), Vector2.right);
    }

    private void MakeInstance(GameObject projectile, Vector2 pos)
    {
        Vector2 moveDirection = pos;
        BossProjectile projScript = projectile.GetComponent<BossProjectile>();

        if (projScript != null)
        {
            projScript.SetDirection(new Vector2(moveDirection.x, moveDirection.y));
        }
    }

    private void StopDash()
    {
        boss.velocity = Vector2.zero;
        isDashing = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.knockBack.CallKnockBack(attackDirection, Vector2.zero, new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical")));
        }
    }
}
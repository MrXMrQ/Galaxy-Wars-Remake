using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D player;
    public float movementSpeed;
    private Vector2 movementDirection;

    [Header("Dashing")]
    public float dashSpeed;
    public float dashDistance;
    public float dashDuration;
    public float dashCooldown;
    private bool isDashing;
    private bool canDash = true;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public float shootingCooldown;
    private bool isShooting;

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(Shooting());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        movementDirection = new Vector2(movementX, movementY).normalized;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        player.velocity = new Vector2(movementDirection.x * movementSpeed, movementDirection.y * movementSpeed);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        player.velocity = new Vector2(movementDirection.x * dashSpeed, movementDirection.y * dashSpeed) * dashDistance;

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator Shooting()
    {
        isShooting = true;
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(shootingCooldown);
        isShooting = false;
    }
}

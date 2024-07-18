using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    public ParticleSystem dashParticles;
    private bool isDashing;
    private bool canDash = true;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public float shootingCooldown;
    public ParticleSystem shootParticles;
    private bool isShooting;

    [Header("Health")]
    public int maxHealth;
    public Healthbar healthbar;
    public static int currentHealth;

    [Header("Score")]
    public static int currentScore;
    public Score score;

    void Start()
    {
        currentScore = 0;
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }


    void Update()
    {
        updateHealthbar();
        updateScore();

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

        SpawnDashParticles();

        player.velocity = new Vector2(movementDirection.x * dashSpeed, movementDirection.y * dashSpeed) * dashDistance;

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void SpawnDashParticles()
    {
        Instantiate(dashParticles, transform.position, transform.rotation);
    }

    private IEnumerator Shooting()
    {
        isShooting = true;

        float x = transform.position.x;
        float y = transform.position.y + 0.5f;
        Vector2 pos = new Vector2(x, y);

        Instantiate(projectilePrefab, pos, Quaternion.identity);
        Instantiate(shootParticles, transform.position, transform.rotation);
        yield return new WaitForSeconds(shootingCooldown);
        isShooting = false;
    }

    public void updateHealthbar()
    {
        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(5);
        }
    }

    public void updateScore()
    {
        score.SetScore(currentScore);
    }

    public void ResetDashingCooldown()
    {
        dashCooldown = 1;
    }

    public void SetDashingCooldown(float newDashCooldown)
    {
        dashCooldown = newDashCooldown;
    }

    public void ResetShootingCooldown()
    {
        shootingCooldown = 0.5f;
    }

    public void SetShootingCooldown(float newShootingCooldown)
    {
        shootingCooldown = newShootingCooldown;
    }

    public void SetHealth(int health)
    {
        currentHealth += health;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        updateHealthbar();
    }

    public void immortality()
    {
        currentHealth = maxHealth;
        updateHealthbar();
    }
}
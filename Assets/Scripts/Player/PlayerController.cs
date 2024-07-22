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
    public float dashCooldownDefaultValue;
    public float dashCooldown;
    public ParticleSystem dashParticles;
    private bool isDashing;
    private bool canDash = true;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public ParticleSystem shootParticles;
    public float shootingCooldownDefaultValue;
    public float shootingCooldown;
    private bool isShooting;

    [Header("Health")]
    public Healthbar healthbar;
    public static int currentHealthpoints;
    private int maxHealthpoints;
    public int healing;

    [Header("Score")]
    public Score score;
    public static int currentScore;
    private int multiplier;
    private int totalScore;
    private int level;

    [Header("Upgrades")]
    private float dashCooldownUpgrade;
    private int healingUpgrade;
    private float shootingCooldownUpgrade;
    private int maxHealthpointsCost;
    private int dashCooldownCost;
    private int healingCost;
    private int shootingCooldownCost;
    private int multiplierCost;

    [Header("Cooldowns")]
    public CooldownHandler cooldownHandler;

    void Start()
    {
        Load();

        currentScore = 0;
        currentHealthpoints = maxHealthpoints;

        dashCooldown = dashCooldownDefaultValue;
        shootingCooldown = shootingCooldownDefaultValue;

        healthbar.SetMaxHealth(maxHealthpoints);
    }

    void Update()
    {
        Debug.Log(dashCooldown);
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
            cooldownHandler.lastShot = Time.time;
            StartCoroutine(Shooting());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            cooldownHandler.lastDash = Time.time;
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

    private void OnDestroy()
    {
        totalScore += (currentScore * multiplier);
        SaveSystem.Save(new GameData(maxHealthpoints, totalScore, level, dashCooldownUpgrade, healingUpgrade, shootingCooldownUpgrade, multiplier, maxHealthpointsCost, dashCooldownCost, healingCost, shootingCooldownCost, multiplierCost));
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

    public void Load()
    {
        GameData gameData = SaveSystem.Load();

        maxHealthpoints = gameData.maxHealthpoints;
        totalScore = gameData.totalScore;
        level = gameData.level;

        dashCooldownUpgrade = gameData.dashCooldown;
        healingUpgrade = gameData.healing;
        shootingCooldownUpgrade = gameData.shootingCooldown;
        multiplier = gameData.multiplier;

        maxHealthpointsCost = gameData.maxHealthpointsCost;
        dashCooldownCost = gameData.dashCooldownCost;
        healingCost = gameData.healingCost;
        shootingCooldownCost = gameData.shootingCooldownCost;
        multiplierCost = gameData.multiplierCost;
    }

    public void updateHealthbar()
    {
        healthbar.SetHealth(currentHealthpoints);

        if (currentHealthpoints <= 0)
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
        dashCooldown = dashCooldownDefaultValue;
    }

    public void SetDashingCooldown(float newDashCooldown)
    {
        dashCooldown = newDashCooldown;
    }

    public void ResetShootingCooldown()
    {
        shootingCooldown = shootingCooldownDefaultValue;
    }

    public void SetShootingCooldown(float newShootingCooldown)
    {
        shootingCooldown = newShootingCooldown;
    }

    public void SetHealth(int heal)
    {
        currentHealthpoints += heal;

        if (currentHealthpoints > maxHealthpoints)
        {
            currentHealthpoints = maxHealthpoints;
        }
        updateHealthbar();
    }

    public void immortality()
    {
        currentHealthpoints = maxHealthpoints;
        updateHealthbar();
    }
}
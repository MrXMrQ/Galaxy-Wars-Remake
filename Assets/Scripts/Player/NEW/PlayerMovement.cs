using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    [Header("MOVEMENT")]
    [SerializeField] Rigidbody2D player;
    [SerializeField] float movementSpeed;
    [SerializeField] float smoothing;
    Vector2 _movementDirection;
    [HideInInspector] public Vector2 currentPosition;

    [Header("DASH")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDistance;
    [SerializeField] float dashDuration;
    [SerializeField] public float dashCooldownDefaultValue;
    [HideInInspector] public float dashCooldown;
    [SerializeField] ParticleSystem dashParticles;
    bool _isDashing;
    bool _canDash = true;

    [Header("SHOOT")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] ParticleSystem shotParticles;
    [SerializeField] public float shotCooldownDefaultValue;
    [HideInInspector] public float shotCooldown;
    bool _isShooting;

    [Header("OTHER")]
    [SerializeField] public PlayerHealth health;
    [SerializeField] public KnockBack knockBack;
    [SerializeField] public Score score;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    void Update()
    {
        if (_isDashing || knockBack.isBeingKnockBack)
        {
            return;
        }

        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_isShooting)
        {
            StartCoroutine(Shoot());
        }

        _movementDirection = new Vector2(movementX, movementY).normalized;
        currentPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (_isDashing || knockBack.isBeingKnockBack)
        {
            return;
        }

        Vector2 targetVelocity = _movementDirection * movementSpeed;
        player.velocity = Vector2.Lerp(player.velocity, targetVelocity, smoothing);
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;

        SpawnParticles(dashParticles);

        player.velocity = new Vector2(_movementDirection.x * dashSpeed, _movementDirection.y * dashSpeed) * dashDistance;

        yield return new WaitForSeconds(dashDuration);
        _isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
    }

    private IEnumerator Shoot()
    {
        _isShooting = true;
        float x = transform.position.x;
        float y = transform.position.y + 0.5f; //The shot is spawned a little higher so that it is not immediately deleted if the player sits on the lower border
        Vector2 pos = new Vector2(x, y);

        Instantiate(projectilePrefab, pos, Quaternion.identity);
        SpawnParticles(shotParticles);

        yield return new WaitForSeconds(shotCooldown);
        _isShooting = false;
    }

    private void SpawnParticles(ParticleSystem particle)
    {
        Instantiate(particle, transform.position, Quaternion.identity);
    }
}
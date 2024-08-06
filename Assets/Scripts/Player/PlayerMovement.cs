using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    [Header("MOVEMENT")]
    [SerializeField] Rigidbody2D player;
    [SerializeField] float MOVEMENT_SPEED;
    [SerializeField] float SMOOTHING;
    Vector2 _movement_direction;
    [HideInInspector] public Vector2 current_position;

    [Header("DASH")]
    [SerializeField] float DASH_SPEED;
    [SerializeField] float DASH_DISTANCE;
    [SerializeField] float DASH_DURATION;
    [SerializeField] public float DASH_COOLDOWN_DEFAULT_VALUE;
    [HideInInspector] public float dash_cooldown;
    [SerializeField] ParticleSystem dash_particles;
    bool _is_dashing;
    bool _can_dash = true;

    [Header("SHOOT")]
    [SerializeField] GameObject player_projectile_prefab;
    [SerializeField] ParticleSystem shot_particles;
    [SerializeField] public float SHOT_COOLDOWN_DEFAULT_VALUE;
    [HideInInspector] public float shot_cooldown;
    bool _isShooting;

    [Header("OTHER")]
    [SerializeField] public int ITEM_DROP_CHANCE;
    [SerializeField] public int COIN_DROP_CHANCE;
    [SerializeField] public PlayerHealth health;
    [SerializeField] public KnockBack knock_back;
    [SerializeField] public Score score;
    [SerializeField] public AbilityCooldownLogic ability_cooldown_logic;

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
        dash_cooldown = DASH_COOLDOWN_DEFAULT_VALUE;
        shot_cooldown = SHOT_COOLDOWN_DEFAULT_VALUE;
    }

    void Update()
    {
        if (_is_dashing || knock_back.is_being_knock_backed)
        {
            return;
        }

        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Dash") && _can_dash)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetButton("Fire1") && !_isShooting)
        {
            StartCoroutine(Shoot());
        }

        _movement_direction = new Vector2(movementX, movementY).normalized;
        current_position = transform.position;
    }

    void FixedUpdate()
    {
        if (_is_dashing || knock_back.is_being_knock_backed)
        {
            return;
        }

        Vector2 targetVelocity = _movement_direction * MOVEMENT_SPEED;
        player.velocity = Vector2.Lerp(player.velocity, targetVelocity, SMOOTHING);
    }

    private IEnumerator Dash()
    {
        _can_dash = false;
        _is_dashing = true;
        ability_cooldown_logic.last_dash = Time.time;

        SpawnParticles(dash_particles);

        player.velocity = new Vector2(_movement_direction.x * DASH_SPEED, _movement_direction.y * DASH_SPEED) * DASH_DISTANCE;

        yield return new WaitForSeconds(DASH_DURATION);
        _is_dashing = false;

        yield return new WaitForSeconds(dash_cooldown);
        _can_dash = true;
    }

    private IEnumerator Shoot()
    {
        _isShooting = true;
        ability_cooldown_logic.last_shot = Time.time;

        float x = transform.position.x;
        float y = transform.position.y + 0.5f; //! The shot is spawned a little higher so that it is not immediately deleted if the player sits on the lower border
        Vector2 pos = new Vector2(x, y);

        Instantiate(player_projectile_prefab, pos, Quaternion.identity);
        SpawnParticles(shot_particles);

        yield return new WaitForSeconds(shot_cooldown);
        _isShooting = false;
    }

    private void SpawnParticles(ParticleSystem particle)
    {
        Instantiate(particle, transform.position, Quaternion.identity);
    }
}
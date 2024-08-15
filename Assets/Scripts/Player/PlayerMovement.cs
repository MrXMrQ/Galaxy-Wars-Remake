using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    [Header("MOVEMENT")]
    [SerializeField] Rigidbody2D player;
    [SerializeField] float MOVEMENT_SPEED;
    [SerializeField] float SMOOTHING;
    [HideInInspector] public Vector2 movement_direction;
    [HideInInspector] public Vector2 current_position;

    [Header("SHOOT")]
    [SerializeField] ParticleSystem shot_particles;
    [SerializeField] public float SHOT_COOLDOWN_DEFAULT_VALUE;
    [HideInInspector] public float shot_cooldown;
    public int DAMAGE;
    GameObject _player_projectile_prefab;
    bool _isShooting;

    [Header("OTHER")]
    [SerializeField] public int ITEM_DROP_CHANCE;
    [SerializeField] public int COIN_DROP_CHANCE;
    [SerializeField] public PlayerHealth health;
    [SerializeField] public KnockBack knock_back;
    [SerializeField] public Score score;
    [SerializeField] public AbilityCooldownLogic ability_cooldown_logic;
    [SerializeField] public AbilityHolder ability_holder;

    void Start()
    {
        Load();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        shot_cooldown = SHOT_COOLDOWN_DEFAULT_VALUE;
    }

    void Update()
    {
        if (knock_back.is_being_knock_backed)
        {
            return;
        }

        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Fire1") && !_isShooting)
        {
            StartCoroutine(Shoot());
        }

        movement_direction = new Vector2(movementX, movementY).normalized;
        current_position = transform.position;
    }

    void FixedUpdate()
    {
        if (knock_back.is_being_knock_backed)
        {
            return;
        }

        Vector2 targetVelocity = movement_direction * MOVEMENT_SPEED;
        player.velocity = Vector2.Lerp(player.velocity, targetVelocity, SMOOTHING);
    }

    private IEnumerator Shoot()
    {
        _isShooting = true;
        ability_cooldown_logic.last_shot = Time.time;

        float x = transform.position.x;
        float y = transform.position.y + 0.5f; //! The shot is spawned a little higher so that it is not immediately deleted if the player sits on the lower border
        Vector2 pos = new Vector2(x, y);

        Instantiate(_player_projectile_prefab, pos, Quaternion.identity);
        SpawnParticles(shot_particles);

        yield return new WaitForSeconds(shot_cooldown);
        _isShooting = false;
    }

    private void SpawnParticles(ParticleSystem particle)
    {
        Instantiate(particle, transform.position, Quaternion.identity);
    }

    private void Load()
    {
        GameData game_data = SaveSystem.Load();
        string path = game_data.weapon_prefab_path;
        _player_projectile_prefab = Resources.Load(path) as GameObject;

        if (_player_projectile_prefab == null)
        {
            Debug.LogError("Failed to load weapon prefab from path: " + path);
        }

        DAMAGE = game_data.damge;
    }
}
using System.Collections;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    [Header("MOVEMENT")]
    [SerializeField] Rigidbody2D player;
    float MOVEMENT_SPEED;
    float SMOOTHING;
    [HideInInspector] public Vector2 movement_direction;
    [HideInInspector] public Vector2 current_position;

    [Header("SHOOT")]
    [SerializeField] ParticleSystem shot_particles;
    [HideInInspector] public float shot_cooldown;
    GameObject _player_projectile_prefab;
    bool _isShooting;

    void Start()
    {
        MOVEMENT_SPEED = PlayerMovement.Instance.MOVEMENT_SPEED;
        SMOOTHING = PlayerMovement.Instance.SMOOTHING;
        shot_particles = PlayerMovement.Instance.shot_particles;
        Load();

        shot_cooldown = PlayerMovement.Instance.SHOT_COOLDOWN_DEFAULT_VALUE;
    }

    void Update()
    {
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
        Vector2 targetVelocity = movement_direction * MOVEMENT_SPEED;
        player.velocity = Vector2.Lerp(player.velocity, targetVelocity, SMOOTHING);
    }

    private IEnumerator Shoot()
    {
        _isShooting = true;

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
    }
}
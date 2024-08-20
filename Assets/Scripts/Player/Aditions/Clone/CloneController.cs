using System.Collections;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    [Header("MOVEMENT")]
    [SerializeField] SpriteRenderer sprite_renderer;
    [HideInInspector] public float _max_distance;
    Rigidbody2D _clone;
    float _MOVEMENT_SPEED;
    float _SMOOTHING;
    Vector2 _movement_direction;

    [Header("SHOOT")]
    ParticleSystem _shot_particles;
    [HideInInspector] public float _shot_cooldown;
    GameObject _player_projectile_prefab;
    bool _isShooting;

    [Header("LIFE TIME")]
    [SerializeField] float MAX_LIFE_TIME;
    [SerializeField] float MIN_LIFE_TIME;
    float _life_time;
    float _instance_time;

    void Start()
    {
        sprite_renderer.sprite = PlayerMovement.Instance.GetComponent<SpriteRenderer>().sprite;

        _life_time = Random.Range(MAX_LIFE_TIME, MIN_LIFE_TIME);

        _clone = GetComponent<Rigidbody2D>();
        _MOVEMENT_SPEED = PlayerMovement.Instance.MOVEMENT_SPEED;
        _SMOOTHING = PlayerMovement.Instance.SMOOTHING;
        _shot_particles = PlayerMovement.Instance.shot_particles;
        _shot_cooldown = PlayerMovement.Instance.SHOT_COOLDOWN_DEFAULT_VALUE;

        Load();

        _instance_time = Time.time;
    }

    void Update()
    {
        if (Time.time - _instance_time >= _life_time)
        {
            PlayerMovement.Instance.ability_holder.ability._clone_is_alive = false;
            Destroy(gameObject);
        }

        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Fire1") && !_isShooting)
        {
            StartCoroutine(Shoot());
        }

        _movement_direction = new Vector2(movementX, movementY).normalized;
        _shot_cooldown = PlayerMovement.Instance.shot_cooldown;
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = _movement_direction * _MOVEMENT_SPEED;
        _clone.velocity = Vector2.Lerp(_clone.velocity, targetVelocity, _SMOOTHING);
    }

    private IEnumerator Shoot()
    {
        _isShooting = true;

        float x = transform.position.x;
        float y = transform.position.y + 0.5f; //! The shot is spawned a little higher so that it is not immediately deleted if the player sits on the lower border
        Vector2 pos = new Vector2(x, y);

        Instantiate(_player_projectile_prefab, pos, Quaternion.identity);
        SpawnParticles(_shot_particles);

        yield return new WaitForSeconds(_shot_cooldown);
        _isShooting = false;
    }

    private void SpawnParticles(ParticleSystem particle)
    {
        Instantiate(particle, transform.position, Quaternion.identity);
    }

    private void Load()
    {
        GameData game_data = SaveSystem.Load();
        string path = game_data.weapon_path;
        _player_projectile_prefab = Resources.Load(path) as GameObject;

        if (_player_projectile_prefab == null)
        {
            Debug.LogError("Failed to load weapon prefab from path: " + path);
        }
    }
}
using System.Collections;
using UnityEngine;

public class MovesetTrippleShotBoss : MonoBehaviour
{
    [Header("PREFABS")]
    [SerializeField] Rigidbody2D boss;
    [SerializeField] GameObject boss_projectile_prefab;
    [SerializeField] GameObject boss_grenade_prefab;

    [Header("VALUES")]
    [SerializeField] float MOVEMENT_SPEED;
    [SerializeField] float SMOOTHING;
    [SerializeField] float CHANGE_DIRECTION_INTERVAL;
    [SerializeField] float SHOT_COOLDOWN;
    [SerializeField] float NEXT_PHASE_TIME;
    [SerializeField] ParticleSystem next_phase_particles;

    enum _BOSS_PHASE { Phase1, Phase2 }
    _BOSS_PHASE _current_phase;
    float _last_phase_time;
    float _boss_width;
    bool is_shooting;
    Vector2 _movement_direction;
    float _next_change_direction_time;
    bool _is_changing_phase;
    int _last_boss_phase;

    [Header("CAMERA")]
    Camera _main_camera;
    float _left_boundary;
    float _right_boundary;

    void Start()
    {
        _main_camera = Camera.main;
        CalculateCameraBounds();

        _boss_width = GetComponent<SpriteRenderer>().bounds.extents.x;
        _next_change_direction_time = Time.time + CHANGE_DIRECTION_INTERVAL;

        _current_phase = (_BOSS_PHASE)Random.Range(0, 2);
        ChangeDirection();
    }

    void Update()
    {
        if (Time.time - _last_phase_time >= NEXT_PHASE_TIME && !_is_changing_phase)
        {
            StartCoroutine(ChangePhase());
        }

        if (Time.time >= _next_change_direction_time)
        {
            ChangeDirection();
            _next_change_direction_time = Time.time + CHANGE_DIRECTION_INTERVAL;
        }

        switch (_current_phase)
        {
            case _BOSS_PHASE.Phase1:
                Phase1Behavior();
                break;
            case _BOSS_PHASE.Phase2:
                Phase2Behavior();
                break;
        }
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = _movement_direction * MOVEMENT_SPEED;
        boss.velocity = Vector2.Lerp(boss.velocity, targetVelocity, SMOOTHING);

        /*
        It is recommended that the position of the boss character be fixed in place within the camera view, 
        in order to prevent the boss character from wandering out of the camera frame.
        */
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, _left_boundary + _boss_width, _right_boundary - _boss_width);
        transform.position = clampedPosition;
    }

    private IEnumerator ChangePhase()
    {
        _is_changing_phase = true;

        ParticleSystem particles = Instantiate(next_phase_particles, transform.position, Quaternion.identity);
        particles.transform.SetParent(transform);
        yield return new WaitForSeconds(particles.main.duration);

        int index;

        do
        {
            index = Random.Range(0, 2);
        } while (index == _last_boss_phase);

        _last_boss_phase = index;

        _current_phase = (_BOSS_PHASE)index;
        _last_phase_time = Time.time;
        _is_changing_phase = false;
    }

    private void ChangeDirection()
    {
        if (transform.position.x <= _left_boundary + _boss_width)
        {
            _movement_direction = Vector2.right;
        }
        else if (transform.position.x >= _right_boundary - _boss_width)
        {
            _movement_direction = Vector2.left;
        }
        else
        {
            float movement_x = GenerateRandomNumber();
            _movement_direction = new Vector2(movement_x, 0).normalized;
        }
    }

    private int GenerateRandomNumber()
    {
        int movement_x = Random.Range(-1, 2);
        return movement_x == 0 ? -1 : movement_x;
    }

    private void Phase1Behavior()
    {
        if (!is_shooting)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        is_shooting = true;
        float x = transform.position.x;
        float y = transform.position.y - 2.5f; //* The projectiles are spawned below the surface due to the configuration of the sprite.
        Vector2 pos = new Vector2(x, y);

        MakeInstanceProjectile(Instantiate(boss_projectile_prefab, new Vector2(x - 3, y), Quaternion.identity), pos, -3);
        MakeInstanceProjectile(Instantiate(boss_projectile_prefab, new Vector2(x, y), Quaternion.identity), pos, 0);
        MakeInstanceProjectile(Instantiate(boss_projectile_prefab, new Vector2(x + 3, y), Quaternion.identity), pos, +3);

        yield return new WaitForSeconds(SHOT_COOLDOWN);
        is_shooting = false;
    }

    private void Phase2Behavior()
    {
        if (!is_shooting)
        {
            StartCoroutine(Grenade());
        }
    }

    private IEnumerator Grenade()
    {
        is_shooting = true;
        float x = transform.position.x;
        float y = transform.position.y - 2.5f; //* The projectiles are spawned below the surface due to the configuration of the sprite.
        Vector2 pos = new Vector2(x, y);

        MakeInstanceGrenade(Instantiate(boss_grenade_prefab, new Vector2(x - 3, y), Quaternion.identity), pos, -3);
        MakeInstanceGrenade(Instantiate(boss_grenade_prefab, new Vector2(x, y), Quaternion.identity), pos, 0);
        MakeInstanceGrenade(Instantiate(boss_grenade_prefab, new Vector2(x + 3, y), Quaternion.identity), pos, +3);

        yield return new WaitForSeconds(SHOT_COOLDOWN);
        is_shooting = false;
    }

    private void CalculateCameraBounds()
    {
        Vector3 left_boundary_world_position = _main_camera.ViewportToWorldPoint(new Vector3(0, 0, _main_camera.nearClipPlane));
        Vector3 right_boundary_world_position = _main_camera.ViewportToWorldPoint(new Vector3(1, 0, _main_camera.nearClipPlane));
        _left_boundary = left_boundary_world_position.x;
        _right_boundary = right_boundary_world_position.x;
    }

    private void MakeInstanceProjectile(GameObject projectile, Vector2 pos, float xOffset)
    {
        Vector2 move_direction = PlayerMovement.Instance.current_position - pos;
        BossProjectile1 boss_projectile = projectile.GetComponent<BossProjectile1>();

        if (boss_projectile != null)
        {
            boss_projectile.SetDirection(new Vector2(move_direction.x - xOffset, move_direction.y));
        }
    }

    private void MakeInstanceGrenade(GameObject boss_grenade, Vector2 pos, float xOffset)
    {
        Vector2 move_direction = PlayerMovement.Instance.current_position - pos;
        BossProjectile3 boss_grenade_script = boss_grenade.GetComponent<BossProjectile3>();

        if (boss_grenade_script != null)
        {
            boss_grenade_script.SetDirection(new Vector2(move_direction.x - xOffset, move_direction.y));
        }
    }
}
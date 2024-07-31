using System.Collections;
using UnityEngine;

public class MovesetRapidFireBoss : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject boss_projectile_prefab;
    [SerializeField] GameObject boss_missile_prefab;
    [SerializeField] GameObject boss_mine_prefab;

    [Header("VALUES")]
    [SerializeField] float SHOT_COOLDOWN;
    [SerializeField] float MINE_COOLDOWN;
    [SerializeField] float MISSILE_X_OFF_SET;
    [SerializeField] float ROTATION_SPEED;
    [SerializeField] float NEXT_PHASE_TIME;

    enum _BOSS_PHASE { Phase1, Phase2, Phase3 }
    _BOSS_PHASE _current_phase;
    Vector2 _left_missile_position;
    Vector2 _right_missile_position;
    bool _is_shooting;
    bool _is_missile_spawned;
    bool _is_mine_spawned;

    float _MIN_X, _MIN_Y, _MAX_X, _MAX_Y;
    float _last_phase_time;
    Camera _main_camera;

    void Start()
    {
        _main_camera = Camera.main;
        CalculateCameraBounds();
        ChangePhase();
    }

    void Update()
    {
        if (Time.time - _last_phase_time >= NEXT_PHASE_TIME)
        {
            ChangePhase();
        }

        switch (_current_phase)
        {
            case _BOSS_PHASE.Phase1:
                Phase1Behavior();
                break;
            case _BOSS_PHASE.Phase2:
                if (!_is_missile_spawned)
                {
                    Phase2Behavior();
                }
                break;
            case _BOSS_PHASE.Phase3:
                Phase3Behavior();
                break;
        }
    }

    void FixedUpdate()
    {
        if (_current_phase == _BOSS_PHASE.Phase1 && Time.time - _last_phase_time <= NEXT_PHASE_TIME / 2)
        {
            transform.Rotate(0, 0, -ROTATION_SPEED * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, ROTATION_SPEED * Time.deltaTime);
        }
    }

    private void CalculateCameraBounds()
    {
        float distance = _main_camera.nearClipPlane;

        Vector3 bottomLeft = _main_camera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 topLeft = _main_camera.ViewportToWorldPoint(new Vector3(0, 1, distance));
        Vector3 bottomRight = _main_camera.ViewportToWorldPoint(new Vector3(1, 0, distance));

        _MIN_X = bottomLeft.x;
        _MAX_X = bottomRight.x;
        _MIN_Y = bottomLeft.y;
        _MAX_Y = topLeft.y;
    }

    private void ChangePhase()
    {
        _is_missile_spawned = false;
        _current_phase = (_BOSS_PHASE)Random.Range(0, 3);
        _last_phase_time = Time.time;
    }

    private void Phase1Behavior()
    {
        if (!_is_shooting)
        {
            StartCoroutine(ShootPhase1());
        }
    }

    private IEnumerator ShootPhase1()
    {
        _is_shooting = true;
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + 0.5f);

        InstantiateProjectile(pos, Vector2.up);
        InstantiateProjectile(pos, Vector2.down);
        InstantiateProjectile(pos, Vector2.left);
        InstantiateProjectile(pos, Vector2.right);

        yield return new WaitForSeconds(SHOT_COOLDOWN);
        _is_shooting = false;
    }

    private void Phase2Behavior()
    {
        _is_missile_spawned = true;
        _left_missile_position = new Vector2(transform.position.x - MISSILE_X_OFF_SET, transform.position.y);
        _right_missile_position = new Vector2(transform.position.x + MISSILE_X_OFF_SET, transform.position.y);

        Instantiate(boss_missile_prefab, _left_missile_position, Quaternion.identity);
        Instantiate(boss_missile_prefab, _right_missile_position, Quaternion.identity);
        ChangePhase();
    }

    private void Phase3Behavior()
    {
        if (!_is_mine_spawned)
        {
            StartCoroutine(SpawnMine());
        }

        if (!_is_shooting)
        {
            StartCoroutine(ShootPhase3());
        }
    }
    private IEnumerator SpawnMine()
    {
        Debug.Log("mine");
        _is_mine_spawned = true;
        Instantiate(boss_mine_prefab, GenerateRandomPos(), Quaternion.identity);
        yield return new WaitForSeconds(MINE_COOLDOWN);
        _is_mine_spawned = false;
    }

    private Vector2 GenerateRandomPos()
    {
        float randomX = Random.Range(_MIN_X, _MAX_X);
        float randomY = Random.Range(_MIN_Y, _MAX_Y);
        return new Vector2(randomX, randomY);
    }

    private IEnumerator ShootPhase3()
    {
        _is_shooting = true;
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        InstantiateProjectile(pos, Vector2.up);

        yield return new WaitForSeconds(SHOT_COOLDOWN);
        _is_shooting = false;
    }

    private void InstantiateProjectile(Vector2 position, Vector2 direction)
    {
        GameObject boss_projectile = Instantiate(boss_projectile_prefab, position, transform.rotation);
        BossProjectile1 boss_projectile_script = boss_projectile.GetComponent<BossProjectile1>();
        if (boss_projectile_script != null)
        {
            boss_projectile_script.SetDirection(direction);
        }
    }
}
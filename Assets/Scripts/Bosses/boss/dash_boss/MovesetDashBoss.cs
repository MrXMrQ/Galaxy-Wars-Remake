using UnityEngine;

public class MovesetDashBoss : MonoBehaviour
{
    [Header("MOVEMENT")]
    [SerializeField] Rigidbody2D boss;
    [SerializeField] float CHANGE_DIRECTION_INTERVAL;
    [SerializeField] float DASH_SPEED;
    [SerializeField] float STOP_AREA;

    [Header("SHOT")]
    public GameObject boss_projectile_prefab;

    float _next_change_time;
    float _distance;
    Vector2 _target_position;
    bool _is_dashing;

    void Start()
    {
        _next_change_time = Time.time + CHANGE_DIRECTION_INTERVAL;
    }

    void Update()
    {
        if (Time.time >= _next_change_time && !_is_dashing)
        {
            Dash();
            _next_change_time = Time.time + CHANGE_DIRECTION_INTERVAL;
        }

        if (_is_dashing)
        {
            _distance = Vector2.Distance(boss.position, _target_position);

            if (_distance < STOP_AREA)
            {
                Shoot();
                StopDash();
            }
        }
    }

    private void Dash()
    {
        _is_dashing = true;
        _target_position = PlayerMovement.Instance.current_position;

        Vector2 movement_direction = (_target_position - (Vector2)transform.position).normalized;
        boss.velocity = new Vector2(movement_direction.x * DASH_SPEED, movement_direction.y * DASH_SPEED);
    }

    private void Shoot()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Vector2 pos = new Vector2(x, y);

        MakeInstance(Instantiate(boss_projectile_prefab, pos, Quaternion.identity), Vector2.up);
        MakeInstance(Instantiate(boss_projectile_prefab, pos, Quaternion.identity), Vector2.down);
        MakeInstance(Instantiate(boss_projectile_prefab, pos, Quaternion.identity), Vector2.left);
        MakeInstance(Instantiate(boss_projectile_prefab, pos, Quaternion.identity), Vector2.right);
    }

    private void MakeInstance(GameObject projectile, Vector2 pos)
    {
        Vector2 move_direction = pos;
        BossProjectile1 boss_projectile = projectile.GetComponent<BossProjectile1>();

        if (boss_projectile != null)
        {
            boss_projectile.SetDirection(new Vector2(move_direction.x, move_direction.y));
        }
    }

    private void StopDash()
    {
        boss.velocity = Vector2.zero;
        _is_dashing = false;
    }
}
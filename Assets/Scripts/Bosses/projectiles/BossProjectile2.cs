using UnityEngine;

public class BossProjectile2 : MonoBehaviour
{
    [SerializeField] GameObject boss_projectile_prefab;
    [SerializeField] int DAMAGE;
    [SerializeField] float MOVEMENT_SPEED;
    [SerializeField] float MAX_LIFE_TIME;
    [SerializeField] float MIN_LIFE_TIME;

    Vector2 _movement_direction;
    float _life_time;
    float _instanciate_time;
    bool _is_detonated;

    void Start()
    {
        _life_time = Random.Range(MAX_LIFE_TIME, MIN_LIFE_TIME);
        _instanciate_time = Time.time;
        Debug.Log("Spawn");
    }

    void Update()
    {

        _movement_direction = (PlayerMovement.Instance.current_position - (Vector2)transform.position).normalized;
        transform.Translate(_movement_direction * MOVEMENT_SPEED * Time.deltaTime);

        if (Time.time - _instanciate_time >= _life_time && !_is_detonated)
        {
            _is_detonated = true;
            Detonate();
        }
    }

    public void Detonate()
    {
        Destroy(gameObject);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Detonate();
            PlayerMovement.Instance.knock_back.CallKnockBack(_movement_direction, Vector2.zero, new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical")));
            PlayerMovement.Instance.health.current_healthpoints -= DAMAGE;
        }
    }
}

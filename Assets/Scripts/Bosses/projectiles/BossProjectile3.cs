using UnityEngine;

public class BossProjectile3 : MonoBehaviour
{
    [SerializeField] GameObject boss_projectil_prefab;
    [SerializeField] float MOVEMENT_SPEED;
    [SerializeField] float MAX_LIFE_TIME;
    [SerializeField] float MIN_LIFE_TIME;

    float _life_time;
    Vector2 _movement_direction;
    Vector2 attackDirection;
    float instanceTime;
    bool isDetonated;

    void Start()
    {
        _life_time = Random.Range(MAX_LIFE_TIME, MIN_LIFE_TIME);
        instanceTime = Time.time;
    }

    void Update()
    {
        if (Time.time - instanceTime >= _life_time && !isDetonated)
        {
            isDetonated = true;
            Detonate();
        }

        transform.Translate(_movement_direction * MOVEMENT_SPEED * Time.deltaTime);

        attackDirection = (PlayerMovement.Instance.transform.position - transform.position).normalized;
    }

    private void Detonate()
    {
        Destroy(gameObject);
        float x = transform.position.x;
        float y = transform.position.y;
        Vector2 pos = new Vector2(x, y);

        MakeInstance(Instantiate(boss_projectil_prefab, pos, Quaternion.identity), Vector2.up);
        MakeInstance(Instantiate(boss_projectil_prefab, pos, Quaternion.identity), Vector2.down);
        MakeInstance(Instantiate(boss_projectil_prefab, pos, Quaternion.identity), Vector2.left);
        MakeInstance(Instantiate(boss_projectil_prefab, pos, Quaternion.identity), Vector2.right);
        MakeInstance(Instantiate(boss_projectil_prefab, pos, Quaternion.identity), new Vector2(1, 1));
        MakeInstance(Instantiate(boss_projectil_prefab, pos, Quaternion.identity), new Vector2(1, -1));
        MakeInstance(Instantiate(boss_projectil_prefab, pos, Quaternion.identity), new Vector2(-1, -1));
        MakeInstance(Instantiate(boss_projectil_prefab, pos, Quaternion.identity), new Vector2(-1, 1));
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
            PlayerMovement.Instance.knock_back.CallKnockBack(attackDirection, Vector2.zero, new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical")));
            PlayerMovement.Instance.health.current_healthpoints--;
        }
    }

    public void SetDirection(Vector2 _movement_direction)
    {
        this._movement_direction = _movement_direction.normalized;
    }
}

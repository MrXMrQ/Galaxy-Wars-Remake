using UnityEngine;

public class ProjctileDefaultLogic : MonoBehaviour
{
    [SerializeField] float PROJECTILE_SPEED;
    [SerializeField] Vector2 MOVE_DIRECTION;
    [SerializeField] bool is_destructible;
    [SerializeField] ParticleSystem hit_particles;
    int _DAMAGE;

    void Start()
    {
        _DAMAGE = PlayerMovement.Instance.DAMAGE;
    }

    void Update()
    {
        transform.Translate(MOVE_DIRECTION * PROJECTILE_SPEED * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            if (is_destructible)
            {
                Destroy(gameObject);
            }

            Destroy(other.gameObject);

            if (Random.Range(0, 100) >= 100 - PlayerMovement.Instance.ITEM_DROP_CHANCE)
            {
                ItemLogic.Instance.SpawnItem(other.transform.position);
            }

            if (Random.Range(0, 100) >= 100 - PlayerMovement.Instance.COIN_DROP_CHANCE)
            {
                ItemLogic.Instance.SpawnCoin(other.transform.position);
            }

            AsteroidLogic asteroidLogic = other.GetComponent<AsteroidLogic>();
            PlayerMovement.Instance.score.UpdateScorePoints(asteroidLogic.SCORE_VALUE);
            SpawnParticles(other.transform.position);
            asteroidLogic.AdjustDifficulty();
        }

        if (other.CompareTag("Boss"))
        {
            Destroy(gameObject);
            BossLogic bossLogic = other.GetComponent<BossLogic>();
            bossLogic.current_healthpoints -= _DAMAGE;
            SpawnParticles(transform.position);
        }

        if (other.CompareTag("BossProjectile2"))
        {
            Destroy(gameObject);
            BossProjectile2 boss_missile = other.GetComponent<BossProjectile2>();
            boss_missile.Detonate();
            SpawnParticles(other.transform.position);
        }
    }

    private void SpawnParticles(Vector2 positon)
    {
        Instantiate(hit_particles, positon, Quaternion.identity);
    }

    public void SetMoveDirection(Vector2 MOVE_DIRECTION)
    {
        this.MOVE_DIRECTION = MOVE_DIRECTION;
    }
}
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] public float PROJECTILE_SPEED;
    [SerializeField] Vector2 move_direction;
    [SerializeField] ParticleSystem hit_particles;

    void Update()
    {
        transform.Translate(move_direction * PROJECTILE_SPEED * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
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
            bossLogic.current_healthpoints--;
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
        this.move_direction = MOVE_DIRECTION;
    }
}
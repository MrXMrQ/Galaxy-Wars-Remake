using UnityEngine;

public class ProjctileLogic : MonoBehaviour
{
    [SerializeField] float PROJECTILE_SPEED;
    [SerializeField] float ITEM_DROP_CHANCE;
    [SerializeField] float COIN_DROP_CHANCE;
    [SerializeField] int DAMAGE;
    [SerializeField] ParticleSystem hit_particles;

    void Update()
    {
        transform.Translate(Vector2.up * PROJECTILE_SPEED * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);

            if (Random.Range(0, 100) >= 100 - ITEM_DROP_CHANCE)
            {
                ItemLogic.Instance.SpawnItem(other.transform.position);
            }

            if (Random.Range(0, 100) >= 100 - COIN_DROP_CHANCE)
            {
                ItemLogic.Instance.SpawnCoin(other.transform.position);
            }

            AsteroidLogic asteroidLogic = other.GetComponent<AsteroidLogic>();
            PlayerMovement.Instance.score.UpdateScorePoints(asteroidLogic.SCORE_VALUE);
            //asteroidLogic.SpawnParticles();
            SpawnParticles(other.transform.position);
            asteroidLogic.AdjustDifficulty();
        }

        if (other.CompareTag("Boss"))
        {
            Destroy(gameObject);
            BossLogic bossLogic = other.GetComponent<BossLogic>();
            bossLogic.current_healthpoints -= DAMAGE;
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
}
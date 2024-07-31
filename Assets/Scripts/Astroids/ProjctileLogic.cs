using UnityEngine;

public class ProjctileLogic : MonoBehaviour
{
    [SerializeField] float PROJECTILE_SPEED;
    [SerializeField] float _ITEM_DROP_CHANCE;
    [SerializeField] int DAMAGE;

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

            if (Random.Range(0, 100) >= 100 - _ITEM_DROP_CHANCE)
            {
                ItemLogic.Instance.SpawnItem(other.transform.position);
            }

            AsteroidLogic asteroidLogic = other.GetComponent<AsteroidLogic>();
            PlayerMovement.Instance.score.UpdateScorePoints(asteroidLogic.SCORE_VALUE);
            asteroidLogic.SpawnParticles();
            asteroidLogic.AdjustDifficulty();
        }

        if (other.CompareTag("Boss"))
        {
            Destroy(gameObject);
            BossLogic bossLogic = other.GetComponent<BossLogic>();
            bossLogic.current_healthpoints -= DAMAGE;
        }

        if (other.CompareTag("BossProjectile2"))
        {
            Destroy(gameObject);
            BossProjectile2 boss_missile = other.GetComponent<BossProjectile2>();
            boss_missile.Detonate();
        }
    }
}
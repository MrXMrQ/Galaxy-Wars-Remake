using UnityEngine;

public class BossProjectile1 : MonoBehaviour
{
    [SerializeField] float PROJECTILE_SPEED;
    [SerializeField] ParticleSystem hit_particles;
    [SerializeField] int DAMAGE;
    Vector2 _move_direction;

    void Update()
    {
        transform.Translate(_move_direction * PROJECTILE_SPEED * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerMovement.Instance.health.current_healthpoints -= DAMAGE;
            SpawnParticles(hit_particles, transform.position);
        }

        if (other.CompareTag("Clone"))
        {
            Destroy(other.gameObject);
            PlayerMovement.Instance.ability_holder.ability._clone_is_alive = false;
            SpawnParticles(hit_particles, other.transform.position);
        }
    }

    public void SetDirection(Vector2 _move_direction)
    {
        this._move_direction = _move_direction.normalized;
    }

    public void SpawnParticles(ParticleSystem particle, Vector2 pos)
    {
        Instantiate(particle, pos, Quaternion.identity);
    }
}

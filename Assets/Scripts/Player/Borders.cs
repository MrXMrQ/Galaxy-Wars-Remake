using UnityEngine;

public class Borders : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] AbilityCooldownLogic ability_cooldown_logic;
    [SerializeField] public float TELEPORT_COOLDOWN;
    [SerializeField] ParticleSystem teleport_particle;

    float _last_teleport_time;
    bool _can_teleport = true;

    void Update()
    {
        if (Time.time - _last_teleport_time > TELEPORT_COOLDOWN)
        {
            _can_teleport = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_can_teleport && other.gameObject.tag == "Player")
        {
            Vector2 new_position = player.position;

            if (other.contacts[0].normal == Vector2.left || other.contacts[0].normal == Vector2.right)
            {
                ability_cooldown_logic.last_teleport = Time.time;

                new_position.x = -player.position.x;
                Instantiate(teleport_particle, player.position, Quaternion.identity);

                player.position = new_position;
                _last_teleport_time = Time.time;
                _can_teleport = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile") || other.CompareTag("BossProjectile2"))
        {
            Destroy(other.gameObject);
        }
    }
}

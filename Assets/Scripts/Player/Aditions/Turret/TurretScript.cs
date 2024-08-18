using System.Collections;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] ParticleSystem hit_particle;
    [SerializeField] float max_life_time;
    [SerializeField] float min_life_time;
    [SerializeField] string tag_to_detect;
    [SerializeField] float detect_radius;
    [SerializeField] GameObject projectile;
    [SerializeField] float shot_cooldown;

    bool _is_shooting;
    float life_time;
    float _instance_time;


    void Awake()
    {
        life_time = Random.Range(max_life_time, min_life_time);
    }

    void Start()
    {
        _instance_time = Time.time;
    }

    void Update()
    {
        if (Time.time - _instance_time >= life_time)
        {
            Destroy(gameObject);
        }

        DetectAsteroids();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Destroy(other.gameObject);
            Instantiate(hit_particle, other.transform.position, Quaternion.identity);
            AsteroidLogic asteroid_script = other.gameObject.GetComponent<AsteroidLogic>();

            if (asteroid_script != null)
            {
                PlayerMovement.Instance.score.UpdateScorePoints(asteroid_script.SCORE_VALUE);
            }
            else
            {
                Debug.LogWarning("NULL");
            }
        }
    }

    private void DetectAsteroids()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detect_radius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Asteroid"))
            {
                if (!_is_shooting)
                {
                    AsteroidLogic asteroid_logic_script = hitCollider.GetComponent<AsteroidLogic>();
                    TurretProjectile projectile_script = projectile.GetComponent<TurretProjectile>();

                    Vector2 asteroid_pos = hitCollider.transform.position;
                    Vector2 asteroid_velocity = asteroid_logic_script.GetComponentInParent<Rigidbody2D>().velocity;
                    Vector2 turret_position = transform.position;
                    float projectile_speed = projectile_script.PROJECTILE_SPEED;

                    Vector2 intercept_position = CalculateInterceptPosition(
                        turret_position, projectile_speed, asteroid_pos, asteroid_velocity
                    );

                    StartCoroutine(InstanceProjectile(projectile, (intercept_position - turret_position).normalized));
                }

                Debug.Log("Erkanntes Objekt: " + hitCollider.gameObject.name + " mit Tag: " + hitCollider.tag);
            }
        }
    }

    private Vector2 CalculateInterceptPosition(Vector2 turret_position, float projectile_speed, Vector2 asteroid_position, Vector2 asteroid_velocity)
    {
        Vector2 direction_to_asteroid = asteroid_position - turret_position;
        float distance_to_asteroid = direction_to_asteroid.magnitude;
        float time_to_impact = distance_to_asteroid / projectile_speed;

        Vector2 future_asteroid_position = asteroid_position + asteroid_velocity * time_to_impact;
        return future_asteroid_position;
    }

    private IEnumerator InstanceProjectile(GameObject projectile, Vector2 move_direction)
    {
        _is_shooting = true;
        TurretProjectile turret_projectile_script = projectile.GetComponent<TurretProjectile>();

        if (turret_projectile_script != null)
        {


            turret_projectile_script.SetMoveDirection(move_direction);
            Instantiate(projectile, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(shot_cooldown);
        _is_shooting = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, detect_radius);
    }
}
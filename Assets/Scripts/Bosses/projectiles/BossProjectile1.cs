using UnityEngine;

public class BossProjectile1 : MonoBehaviour
{
    [SerializeField] float PROJECTILE_SPEED;
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
        }
    }

    public void SetDirection(Vector2 _move_direction)
    {
        this._move_direction = _move_direction.normalized;
    }
}

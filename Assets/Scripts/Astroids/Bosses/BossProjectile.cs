using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float projectileSpeed;
    public Vector2 moveDirection;
    void Update()
    {
        transform.Translate(moveDirection * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerController.currentHealthpoints--;
        }

        if (other.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection.normalized;
    }
}

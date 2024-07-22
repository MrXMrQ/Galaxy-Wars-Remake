using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    void Update()
    {
        transform.Translate(Vector2.up * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    void Update()
    {
        transform.Translate(Vector2.up * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Border")
        {
            Destroy(gameObject);
        }
    }
}

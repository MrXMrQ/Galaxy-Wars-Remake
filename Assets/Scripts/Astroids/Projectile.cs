using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public static int damage = 1;
    void Update()
    {
        transform.Translate(Vector2.up * projectileSpeed * Time.deltaTime);
    }
}
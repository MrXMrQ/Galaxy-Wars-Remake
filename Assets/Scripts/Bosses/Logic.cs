using UnityEngine;

public class Logic : MonoBehaviour
{
    public string bossName;
    public int maxHealthpoints;
    public int collisionDamage;
    public int score;
    public static int currentHealthpoints;
    public Vector2 spawnPoint;

    void Start()
    {
        currentHealthpoints = maxHealthpoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealthpoints <= 0)
        {
            Destroy(gameObject);
            Spawner.Instance.isBossAlive = false;
            PlayerController.currentScore += score;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.currentHealthpoints -= collisionDamage;
        }

        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            currentHealthpoints -= Projectile.damage;
        }
    }

    public int GetHealthpoints()
    {
        return currentHealthpoints;
    }
}
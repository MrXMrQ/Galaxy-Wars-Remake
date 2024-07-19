using UnityEngine;

public class AsteroidsMovement : MonoBehaviour
{
    public float movementSpeed;
    public int damage;
    public int score;
    public ParticleSystem dstroyParticles;
    private float multiplier = 0;
    private int increaseValue = 100;
    private float deadZone = -15;
    private System.Random rnd = new System.Random();

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.down * (movementSpeed + multiplier)) * Time.deltaTime;

        MultiplierIncrease();

        if (transform.position.y < deadZone)
        {
            Destroy(gameObject);
            PlayerController.currentScore++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);

            if (rnd.Next(0, 100) >= 75)
            {
                ItemHandler.Instance.SpawnItem(transform.position);
            }

            PlayerController.currentScore += score;
            SpawnParticles();
        }
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerController.currentHealth -= damage;
            SpawnParticles();
        }
    }

    private void MultiplierIncrease()
    {
        if (PlayerController.currentScore >= increaseValue)
        {
            multiplier += 1;
            increaseValue += increaseValue;
        }
    }

    public void SpawnParticles()
    {
        Instantiate(dstroyParticles, transform.position, Quaternion.identity);
    }
}

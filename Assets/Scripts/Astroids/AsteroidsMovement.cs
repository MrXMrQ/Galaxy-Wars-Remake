using System.Data.Common;
using UnityEngine;

public class AsteroidsMovement : MonoBehaviour
{
    public float movementSpeed;
    public int damage;
    public int score;
    public ParticleSystem dstroyParticles;
    private float multiplier = 0;
    private bool incrase;
    private static int increaseValue = 100;
    private float deadZone = -15;
    private System.Random rnd = new System.Random();

    // Update is called once per frame
    void Update()
    {
        MultiplierIncrease();

        transform.position = transform.position + (Vector3.down * (movementSpeed + multiplier)) * Time.deltaTime;

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

            if (rnd.Next(0, 100) >= 50)
            {
                ItemHandler.Instance.SpawnItem(transform.position);
            }

            PlayerController.currentScore += score;
            SpawnParticles();
        }
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerController.currentHealthpoints -= damage;
            SpawnParticles();
        }
    }

    private void MultiplierIncrease()
    {
        // Debug current score and increase value
        Debug.Log($"Current Score: {PlayerController.currentScore}, Increase Value: {increaseValue}");

        // Check if the current score has reached or surpassed the increase value
        if (PlayerController.currentScore >= increaseValue)
        {
            Debug.Log("Condition met: currentScore >= increaseValue");

            // Ensure that 'incrase' is true only if it's not already set
            if (!incrase)
            {
                incrase = true;
            }
        }

        // Check if the 'incrase' flag is set
        if (incrase)
        {
            Debug.Log("Increasing speed and updating values");

            // Perform your speed increase logic
            if (Spawner.Instance.spawnrate > 0.09f)
            {
                Spawner.Instance.spawnrate -= 0.01f;
            }

            multiplier += 5;

            // Update the increaseValue for the next check
            increaseValue += 100;

            // Reset the incrase flag to prevent continuous increase
            incrase = false;
        }
    }

    public void SpawnParticles()
    {
        Instantiate(dstroyParticles, transform.position, Quaternion.identity);
    }
}

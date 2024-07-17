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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            PlayerController.currentScore += score;
            SpawnParticles();

        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            PlayerController.currentHealth -= damage;
            SpawnParticles();
        }
        SpawnParticles();

    }

    private void MultiplierIncrease()
    {
        if (PlayerController.currentScore >= increaseValue)
        {
            multiplier += 1;
            increaseValue += increaseValue;
            Debug.Log(multiplier);
        }
    }

    public void SpawnParticles()
    {
        Instantiate(dstroyParticles, transform.position, Quaternion.identity);
    }
}

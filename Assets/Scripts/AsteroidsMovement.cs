using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsMovement : MonoBehaviour
{
    public float movementSpeed;
    public int damage;
    public int score;
    private float deadZone = -15;
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.down * movementSpeed) * Time.deltaTime;

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
            PlayerController.currentScore += score;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            PlayerController.currentHealth -= damage;
        }
    }
}

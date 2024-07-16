using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsMovement : MonoBehaviour
{
    public float movementSpeed;
    private float deadZone = -15;
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.down * movementSpeed) * Time.deltaTime;

        if (transform.position.y < deadZone)
        {
            Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Game Over");
            Destroy(collision.gameObject);
        }
    }
}

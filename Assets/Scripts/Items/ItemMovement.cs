using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public float movementSpeed = 2f;
    public int index;
    public float itemDuration;
    public ParticleSystem particles;
    private float deadZone = -15f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * movementSpeed * Time.deltaTime;

        if (transform.position.y < deadZone)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(particles, transform.position, Quaternion.identity);
            ItemHandler.Instance.UpdateSprite(GetComponent<SpriteRenderer>().sprite, index);
            ItemHandler.Instance.collectedItem[index - 1] = true;
            Destroy(gameObject);
        }
    }
}
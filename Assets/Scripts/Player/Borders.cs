using UnityEngine;

public class Borders : MonoBehaviour
{
    public Rigidbody2D player;
    public CooldownHandler cooldownHandler;
    public float teleportCooldown;
    public ParticleSystem teleportParticle;
    private float lastTeleportTime;
    private bool canTeleport = true;

    void Update()
    {
        if (Time.time - lastTeleportTime > teleportCooldown)
        {
            canTeleport = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (canTeleport && other.gameObject.tag == "Player")
        {
            Vector2 newPosition = player.position;

            if (other.contacts[0].normal == Vector2.left || other.contacts[0].normal == Vector2.right)
            {
                cooldownHandler.lastTeleport = Time.time;

                newPosition.x = -player.position.x;
                Instantiate(teleportParticle, player.position, Quaternion.identity);

                player.position = newPosition;
                lastTeleportTime = Time.time;
                canTeleport = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }
    }
}

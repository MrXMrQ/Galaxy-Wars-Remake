using Unity.VisualScripting;
using UnityEngine;

public class Borders : MonoBehaviour
{
    public Rigidbody2D player;
    public float teleportCooldown = 3f;
    public ParticleSystem teleportParticle;
    private float lastTeleportTime = 0f;
    private bool canTeleport = true;

    void Start()
    {
        // Initial setup if needed
    }

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
                newPosition.x = -player.position.x;
                Instantiate(teleportParticle, player.position, Quaternion.identity);
            }

            player.position = newPosition;
            lastTeleportTime = Time.time;
            canTeleport = false;
        }
    }
}

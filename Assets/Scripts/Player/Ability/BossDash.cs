using UnityEngine;

[CreateAssetMenu(fileName = "_ability", menuName = "Ability/Boss Dash")]
public class BossDash : Ability
{
    [SerializeField] ParticleSystem dash_particles;
    [SerializeField] float DASH_SPEED;
    [SerializeField] float DASH_DISTANCE;

    public override void Activate(GameObject parent)
    {
        PlayerMovement playerMovement = parent.GetComponent<PlayerMovement>();
        Rigidbody2D rigidbody = parent.GetComponent<Rigidbody2D>();

        SpawnParticles(dash_particles, rigidbody.position);
        rigidbody.velocity = playerMovement.movement_direction * DASH_SPEED * DASH_DISTANCE;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "_ability", menuName = "Ability/Boss Dash")]
public class BossDash : Ability
{
    [SerializeField] ParticleSystem dash_particles;
    [SerializeField] float DASH_SPEED;
    [SerializeField] float DASH_DISTANCE;
    [SerializeField] GameObject player_default_projectile;

    public override void Activate(GameObject parent)
    {
        PlayerMovement playerMovement = parent.GetComponent<PlayerMovement>();
        Rigidbody2D rigidbody = parent.GetComponent<Rigidbody2D>();

        SpawnParticles(dash_particles, rigidbody.position);
        rigidbody.velocity = playerMovement.movement_direction * DASH_SPEED * DASH_DISTANCE;

        Shot(rigidbody);
    }

    private void Shot(Rigidbody2D rigidbody)
    {
        InstanceProjectile(player_default_projectile, Vector2.left, rigidbody.position);
        InstanceProjectile(player_default_projectile, Vector2.right, rigidbody.position);
        InstanceProjectile(player_default_projectile, Vector2.down, rigidbody.position);
        InstanceProjectile(player_default_projectile, Vector2.up, rigidbody.position);
    }

    private void InstanceProjectile(GameObject projectile, Vector2 move_direction, Vector2 pos)
    {
        ProjctileDefaultLogic projectile_script = projectile.GetComponent<ProjctileDefaultLogic>();

        if (projectile_script != null)
        {
            projectile_script.SetMoveDirection(move_direction);
            Instantiate(projectile, pos, Quaternion.identity);
        }
    }
}

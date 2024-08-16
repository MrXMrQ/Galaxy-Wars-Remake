using UnityEngine;

public class ProjectileRoundShot : MonoBehaviour
{
    [SerializeField] GameObject default_projectile;

    void Start()
    {
        RoundShot();
    }

    private void RoundShot()
    {
        InstanceProjectile(default_projectile, new Vector2(1, 1));
        InstanceProjectile(default_projectile, new Vector2(1, -1));
        InstanceProjectile(default_projectile, new Vector2(-1, 1));
        InstanceProjectile(default_projectile, new Vector2(-1, -1));
        InstanceProjectile(default_projectile, Vector2.right);
        InstanceProjectile(default_projectile, Vector2.left);
        InstanceProjectile(default_projectile, Vector2.down);
        InstanceProjectile(default_projectile, Vector2.up);
    }

    private void InstanceProjectile(GameObject projectile, Vector2 move_direction)
    {
        ProjctileDefaultLogic projectile_script = projectile.GetComponent<ProjctileDefaultLogic>();

        if (projectile_script != null)
        {
            projectile_script.SetMoveDirection(move_direction);
            Instantiate(projectile, transform.position, Quaternion.identity);
        }
    }
}

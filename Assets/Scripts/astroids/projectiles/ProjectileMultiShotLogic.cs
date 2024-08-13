using UnityEngine;

public class ProjectileMultiShotLogic : MonoBehaviour
{
    [SerializeField] private ShotType shotType;
    [SerializeField] private GameObject defaultProjectile;

    private ProjctileDefaultLogic projectileLogic;

    private enum ShotType
    {
        Dual,
        Triple
    }

    void Start()
    {
        projectileLogic = defaultProjectile.GetComponent<ProjctileDefaultLogic>();

        if (projectileLogic == null)
        {
            return;
        }

        switch (shotType)
        {
            case ShotType.Triple:
                TripleShot();
                break;
            case ShotType.Dual:
                DualShot();
                break;
        }
    }

    private void TripleShot()
    {
        Destroy(gameObject);
        Vector2 position = transform.position;

        CreateProjectile(new Vector2(-0.5f, 1), position);
        CreateProjectile(new Vector2(0.5f, 1), position);
        CreateProjectile(Vector2.up, position);
    }

    private void DualShot()
    {
        Destroy(gameObject);
        Vector2 position = transform.position;
        CreateProjectile(Vector2.up, position + Vector2.left);
        CreateProjectile(Vector2.up, position + Vector2.right);
    }

    private void CreateProjectile(Vector2 moveDirection, Vector2 position)
    {
        projectileLogic.SetMoveDirection(moveDirection);
        Instantiate(defaultProjectile, position, Quaternion.identity);
    }
}
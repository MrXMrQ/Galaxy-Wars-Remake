using System.Collections;
using UnityEngine;

public class MovementRapidFireBoss : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject missilePrefab;
    public float shootingCooldownPhase1;
    public float shootingCooldownPhase3;
    public float rotationSpeed;
    public float nextChangeTimePhase;

    private enum BossPhase { Phase1, Phase2, Phase3 }
    private BossPhase currentPhase;
    private bool isSpawned;

    private bool isShooting;
    private Vector2 attackDirection;
    private System.Random rnd = new System.Random();
    private float lastPhaseChangeTime;

    void Start()
    {
        ChangePhase();
    }

    void Update()
    {
        if (Time.time - lastPhaseChangeTime >= nextChangeTimePhase)
        {
            ChangePhase();
        }

        switch (currentPhase)
        {
            case BossPhase.Phase1:
                Phase1Behavior();
                break;
            case BossPhase.Phase2:
                if (!isSpawned)
                {
                    isSpawned = true;
                    Vector2 posLeft = new Vector2(transform.position.x - 5, transform.position.y);
                    Vector2 posRight = new Vector2(transform.position.x + 5, transform.position.y);
                    Phase2Behavior(posLeft, posRight);
                }
                break;
            case BossPhase.Phase3:
                Phase3Behavior();
                break;
        }

        attackDirection = PlayerController.player.transform.position - transform.position;
    }

    void FixedUpdate()
    {
        if (currentPhase == BossPhase.Phase1 && Time.time - lastPhaseChangeTime <= nextChangeTimePhase / 2)
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    private void ChangePhase()
    {
        isSpawned = false;
        currentPhase = (BossPhase)rnd.Next(0, 3);
        lastPhaseChangeTime = Time.time;
    }

    private void Phase1Behavior()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootPhase1());
        }
    }

    private IEnumerator ShootPhase1()
    {
        isShooting = true;
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + 0.5f);

        InstantiateProjectile(pos, Vector2.up);
        InstantiateProjectile(pos, Vector2.down);
        InstantiateProjectile(pos, Vector2.left);
        InstantiateProjectile(pos, Vector2.right);

        yield return new WaitForSeconds(shootingCooldownPhase1);
        isShooting = false;
    }

    private void Phase2Behavior(Vector2 posLeft, Vector2 posRight)
    {
        Instantiate(missilePrefab, posLeft, Quaternion.identity);
        Instantiate(missilePrefab, posRight, Quaternion.identity);
    }

    private void Phase3Behavior()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootPhase3());
        }
    }

    private IEnumerator ShootPhase3()
    {
        isShooting = true;
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + 0.5f);

        InstantiateProjectile(pos, Vector2.up);

        yield return new WaitForSeconds(shootingCooldownPhase3);
        isShooting = false;
    }

    private void InstantiateProjectile(Vector2 position, Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, transform.rotation);
        BossProjectile projScript = projectile.GetComponent<BossProjectile>();
        if (projScript != null)
        {
            projScript.SetDirection(direction);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.knockBack.CallKnockBack(attackDirection, Vector2.zero, new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical")));
        }
    }
}
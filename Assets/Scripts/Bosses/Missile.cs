using UnityEngine;

public class Missile : MonoBehaviour
{
    public Rigidbody2D missile;
    public GameObject projectilePrefab;
    public float movementSpeed;
    public float smoothing;
    public float nextPhaseTime;
    private Vector2 movementDirection;
    private float lifeTime;
    private float instanceTime;
    private bool isDetonated;
    private System.Random rnd = new System.Random();
    void Start()
    {
        lifeTime = (float)(rnd.NextDouble() * (nextPhaseTime - 1) + 1);
        instanceTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = (PlayerController.player.transform.position - transform.position).normalized;

        if (Time.time - instanceTime >= lifeTime && !isDetonated)
        {
            isDetonated = true;
            Detonate();
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = movementDirection * movementSpeed;
        missile.velocity = Vector2.Lerp(missile.velocity, targetVelocity, smoothing);
    }

    private void Detonate()
    {
        Destroy(gameObject);
        float x = transform.position.x;
        float y = transform.position.y + 0.5f; //?
        Vector2 pos = new Vector2(x, y);

        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), Vector2.up);
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), Vector2.down);
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), Vector2.left);
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), Vector2.right);
    }

    private void MakeInstance(GameObject projectile, Vector2 pos)
    {
        Vector2 moveDirection = pos;
        BossProjectile projScript = projectile.GetComponent<BossProjectile>();

        if (projScript != null)
        {
            projScript.SetDirection(new Vector2(moveDirection.x, moveDirection.y));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Detonate();
            Debug.Log("Hit");
            PlayerController.knockBack.CallKnockBack(movementDirection, Vector2.zero, new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical")));
            PlayerController.currentHealthpoints--;
        }

        if (other.CompareTag("Border"))
        {
            Detonate();
            Destroy(gameObject);
        }
    }
}

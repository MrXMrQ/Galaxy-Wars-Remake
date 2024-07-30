using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Rigidbody2D bomb;
    public GameObject projectilePrefab;
    public float movementSpeed;
    public float smoothing;
    public float mineLifeTime;
    public float maxLifeTime;

    private Vector2 moveDirection;
    private Vector2 attackDirection;
    private float lifeTime;
    private float instanceTime;
    private bool isDetonated;
    private System.Random rnd = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = (float)(rnd.NextDouble() * maxLifeTime + mineLifeTime);
        instanceTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - instanceTime >= lifeTime && !isDetonated)
        {
            isDetonated = true;
            Detonate();
        }

        transform.Translate(moveDirection * movementSpeed * Time.deltaTime);

        attackDirection = (PlayerController.player.transform.position - transform.position).normalized;
    }

    private void Detonate()
    {
        Destroy(gameObject);
        float x = transform.position.x;
        float y = transform.position.y;
        Vector2 pos = new Vector2(x, y);

        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), Vector2.up);
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), Vector2.down);
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), Vector2.left);
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), Vector2.right);
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), new Vector2(1, 1));
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), new Vector2(1, -1));
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), new Vector2(-1, -1));
        MakeInstance(Instantiate(projectilePrefab, pos, Quaternion.identity), new Vector2(-1, 1));
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
            PlayerController.knockBack.CallKnockBack(attackDirection, Vector2.zero, new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical")));
            if (!ItemHandler.isImmortal)
            {
                PlayerController.currentHealthpoints--;
            }
        }

        if (other.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection.normalized;
    }
}

using UnityEngine;

public class BossLogic : MonoBehaviour
{
    [SerializeField] public string boss_name;
    [SerializeField] public Sprite sprite;
    [SerializeField] public int MAX_HEALTHPOINTS;
    [SerializeField] int COLLISION_DAMAGE;
    [SerializeField] int SCORE;
    [SerializeField] public ParticleSystem spawn_particles;
    static int _currentHealthpoints { get; set; }
    Vector2 _attack_direction;
    public Vector2 spawn_point;

    [HideInInspector]
    public int current_healthpoints
    {
        get
        {
            return _currentHealthpoints;
        }
        set
        {
            _currentHealthpoints = value;

            if (current_healthpoints <= 0)
            {
                Destroy(gameObject);
                FabricatorLogic.Instance.is_boss_alive = false;
                PlayerMovement.Instance.score.UpdateScorePoints(SCORE);
            }
        }
    }

    void Start()
    {
        current_healthpoints = MAX_HEALTHPOINTS;
    }

    void Update()
    {
        _attack_direction = PlayerMovement.Instance.current_position - (Vector2)transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.Instance.health.current_healthpoints -= COLLISION_DAMAGE;
            PlayerMovement.Instance.knock_back.CallKnockBack(_attack_direction, Vector2.zero, new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        }
    }

    public void SpawnParticles()
    {
        Instantiate(spawn_particles, spawn_point, Quaternion.identity);
    }
}
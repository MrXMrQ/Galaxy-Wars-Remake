using UnityEngine;

public class AsteroidLogic : MonoBehaviour
{
    [SerializeField] float MOVEMENT_SPEED;
    [SerializeField] int DAMAGE;
    [SerializeField] public int SCORE_VALUE;
    [SerializeField] ParticleSystem hit_particles;
    static float _score_For_Difficulty_Increase = 100;
    int _DIFFICULTY_INCREMENT_VALUE = 100;
    int _player_score;
    float _difficultyMultiplier = 0;
    float _DIFFICULTY_MULTIPLIER_INCREMENT_VALUE = 5;
    float _MIN_SPAWN_RATE = 0.09f;
    float _SPAWN_RATE_INCREASE_AMOUNT = 0.01f;
    float _OFF_SCREEN_Y_THRESHOLD;
    Camera _main_camera;

    void Start()
    {
        _main_camera = Camera.main;
        CalculateCameraBounds();
    }
    void Update()
    {
        if (transform.position.y < _OFF_SCREEN_Y_THRESHOLD)
        {
            Destroy(gameObject);
            PlayerMovement.Instance.score.UpdateScorePoints(1);
            _player_score = PlayerMovement.Instance.score.score;
            AdjustDifficulty();
        }

        transform.Translate(Vector2.down * (MOVEMENT_SPEED + _difficultyMultiplier) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(hit_particles, transform.position, Quaternion.identity);
            PlayerMovement.Instance.health.current_healthpoints -= DAMAGE;
        }

        if (other.CompareTag("Clone"))
        {
            PlayerMovement.Instance.ability_holder.ability._clone_is_alive = false;

            Destroy(gameObject);
            Destroy(other.gameObject);
            Instantiate(hit_particles, other.gameObject.transform.position, Quaternion.identity);
        }
    }
    private void CalculateCameraBounds()
    {
        Vector3 bottom_right = _main_camera.ViewportToWorldPoint(new Vector3(-1, -1, _main_camera.nearClipPlane));
        _OFF_SCREEN_Y_THRESHOLD = bottom_right.y;
    }

    public void AdjustDifficulty()
    {
        if (_player_score >= _score_For_Difficulty_Increase)
        {
            if (FabricatorLogic.Instance.spawn_rate > _MIN_SPAWN_RATE)
            {
                FabricatorLogic.Instance.spawn_rate -= _SPAWN_RATE_INCREASE_AMOUNT;
            }

            _difficultyMultiplier += _DIFFICULTY_MULTIPLIER_INCREMENT_VALUE;
            _score_For_Difficulty_Increase += _DIFFICULTY_INCREMENT_VALUE;
        }
    }
}
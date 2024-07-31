using UnityEngine;

public class AsteroidLogic : MonoBehaviour
{
    [SerializeField] float MOVEMENT_SPEED;
    [SerializeField] int DAMAGE;
    [SerializeField] public int SCORE_VALUE;
    [SerializeField] ParticleSystem EXPLOSION_EFFECT;
    static float _score_For_Difficulty_Increase = 100;
    int _DIFFICULTY_INCREMENT_VALUE = 100;
    int _player_score;
    float _difficultyMultiplier = 0;
    float _DIFFICULTY_MULTIPLIER_INCREMENT_VALUE = 5;
    float _MIN_SPAWN_RATE = 0.09f;
    float _SPAWN_RATE_INCREASE_AMOUNT = 0.01f;
    float _OFF_SCREEN_Y_THRESHOLD = -15;

    void Update()
    {
        if (transform.position.y < _OFF_SCREEN_Y_THRESHOLD)
        {
            Destroy(gameObject);
            PlayerMovement.Instance.score.UpdateScorePoints(1);
            _player_score = PlayerMovement.Instance.score._score;
            AdjustDifficulty();
        }

        transform.Translate(Vector2.down * (MOVEMENT_SPEED + _difficultyMultiplier) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);

            PlayerMovement.Instance.health.currentHealthpoints -= DAMAGE;

            SpawnParticles();
        }
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

    public void SpawnParticles()
    {
        Instantiate(EXPLOSION_EFFECT, transform.position, Quaternion.identity);
    }
}